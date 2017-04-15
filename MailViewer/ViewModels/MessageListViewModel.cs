using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Robot1que.MailViewer.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Robot1que.MailViewer.ViewModels
{
    public class MessageListViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;

        private Message _openedMessage;

        public ObservableCollection<Message> Messages { get; } = new ObservableCollection<Message>();

        public Message OpenedMessage
        {
            get => this._openedMessage;
            set => this.PropertySet(ref this._openedMessage, value);
        }

        public MessageListViewModel(
            IAuthenticationService authenticationService, 
            INavigationService navigationService)
        {
            this._authenticationService = 
                authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));

            this._navigationService =
                navigationService ?? throw new ArgumentNullException(nameof(navigationService));

            this._navigationService.MailFolderOpenRequested += this.NavigationService_FolderOpenRequested;

            this.PropertyChanged += this.MessageListViewModel_PropertyChanged;
        }

        private async void NavigationService_FolderOpenRequested(
            object sender, 
            MailFolderOpenRequestedEventArgs e)
        {
            this.Messages.Clear();

            var graphService = this._authenticationService.GraphServiceGet();
            var mailFolder = await
                graphService.Me.MailFolders[e.MailFolderId].Request()
                    .Expand("Messages")
                    .GetAsync();

            foreach (var data in mailFolder.Messages)
            {
                this.Messages.Add(Message.FromData(data));
            }
        }

        private void MessageListViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MessageListViewModel.OpenedMessage))
            {
                this.MessageOpen();
            }
        }

        private void MessageOpen()
        {
            if (this.OpenedMessage != null)
            {
                this._navigationService.MessageOpen(this.OpenedMessage.Id);
            }
        }
    }
}
