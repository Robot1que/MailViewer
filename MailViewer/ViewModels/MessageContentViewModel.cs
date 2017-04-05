using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Robot1que.MailViewer.Models;

namespace Robot1que.MailViewer.ViewModels
{
    public class MessageContentViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;

        private MessageBody _messageBody;

        public MessageBody MessageBody
        {
            get
            {
                return this._messageBody;
            }
            private set
            {
                this.PropertySet(ref this._messageBody, value);
            }
        }

        public MessageContentViewModel(
            IAuthenticationService authenticationService,
            INavigationService navigationService)
        {
            this._authenticationService = 
                authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this._navigationService = 
                navigationService ?? throw new ArgumentNullException(nameof(navigationService));

            this._navigationService.MessageOpenRequested += this.NavigationService_MessageOpenRequested;
            this._navigationService.MailFolderOpenRequested += 
                this.NavigationService_MailFolderOpenRequested;
        }

        private void NavigationService_MailFolderOpenRequested(
            object sender, 
            MailFolderOpenRequestedEventArgs e)
        {
            this.MessageBody = null;
        }

        private async void NavigationService_MessageOpenRequested(
            object sender, 
            MessageOpenRequestedEventArgs e)
        {
            var graphService = this._authenticationService.GraphServiceGet();
            var data = await graphService.Me.Messages[e.MessageId].Request().GetAsync();
            this.MessageBody = Message.FromData(data).Body;
        }
    }
}
