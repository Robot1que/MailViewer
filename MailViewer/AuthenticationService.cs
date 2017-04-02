using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace Robot1que.MailViewer
{
    public interface IAuthenticationService
    {
        GraphServiceClient GraphServiceGet();

        void SignOut();
    }

    public class AuthenticationService : IAuthenticationService
    {
        private static string ClientId = "acb24a92-3cd9-4db9-b575-0c9ef431f37b";
        private static string[] Scopes = { "Mail.ReadWrite", "Mail.Send" };

        private readonly ISettings _settings;
        private readonly PublicClientApplication _identityClientApp;

        private GraphServiceClient _graphServiceClient = null;

        public AuthenticationService(ISettings settings)
        {
            this._settings = settings ?? throw new ArgumentNullException(nameof(settings));

            this._identityClientApp = new PublicClientApplication(AuthenticationService.ClientId);
        }

        public GraphServiceClient GraphServiceGet()
        {
            if (this._graphServiceClient == null)
            {
                this._graphServiceClient =
                    new GraphServiceClient(
                        "https://graph.microsoft.com/v1.0",
                        new DelegateAuthenticationProvider(
                            async (requestMessage) =>
                            {
                                var (token, expiryTime) = await
                                    this.GetTokenForUserAsync(
                                        this._settings.AuthToken,
                                        this._settings.AuthTokenExpiryTime
                                    );

                                this._settings.AuthToken = token;
                                this._settings.AuthTokenExpiryTime = expiryTime;

                                requestMessage.Headers.Authorization =
                                    new AuthenticationHeaderValue("bearer", token);
                            }
                        )
                    );
            }
            return this._graphServiceClient;
        }

        public void SignOut()
        {
            foreach (var user in this._identityClientApp.Users)
            {
                user.SignOut();
            }

            this._graphServiceClient = null;

            this._settings.AuthToken = null;
            this._settings.AuthTokenExpiryTime = null;
        }

        private async Task<(string token, DateTimeOffset? expiryTime)> GetTokenForUserAsync(
            string oldToken,
            DateTimeOffset? oldExpiresOn)
        {
            string token = oldToken;
            DateTimeOffset? expiresOn = oldExpiresOn;

            try
            {
                var authResult = await
                    this._identityClientApp.AcquireTokenSilentAsync(AuthenticationService.Scopes);

                token = authResult.Token;
            }
            catch (MsalSilentTokenAcquisitionException)
            {
                if (
                    oldToken == null ||
                    oldExpiresOn.HasValue == false ||
                    oldExpiresOn.Value <= DateTimeOffset.UtcNow.AddMinutes(5))
                {
                    var authResult = await 
                        this._identityClientApp.AcquireTokenAsync(AuthenticationService.Scopes);

                    token = authResult.Token;
                    expiresOn = authResult.ExpiresOn;
                }
            }
            return (token, expiresOn);
        }
    }
}