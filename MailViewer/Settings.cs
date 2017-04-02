using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Storage;

namespace Robot1que.MailViewer
{
    public interface ISettings
    {
        string AuthToken { get; set; }

        DateTimeOffset? AuthTokenExpiryTime { get; set; }
    }

    public class Settings : ISettings
    {
        private ApplicationDataContainer _localSettings;

        public string AuthToken
        {
            get
            {
                this._localSettings.Values.TryGetValue(nameof(AuthToken), out object value);
                return value as string;
            }
            set
            {
                this._localSettings.Values[nameof(AuthToken)] = value;
            }
        }

        public DateTimeOffset? AuthTokenExpiryTime
        {
            get
            {
                this._localSettings.Values.TryGetValue(nameof(AuthTokenExpiryTime), out object value);
                return value as DateTimeOffset?;
            }
            set
            {
                this._localSettings.Values[nameof(AuthTokenExpiryTime)] = value;
            }
        }

        public Settings()
        {
            this._localSettings = ApplicationData.Current.LocalSettings;
        }
    }
}
