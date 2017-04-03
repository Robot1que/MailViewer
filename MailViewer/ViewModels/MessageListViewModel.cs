using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Robot1que.MailViewer.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Robot1que.MailViewer.ViewModels
{
    public class MessageListViewModel : INotifyPropertyChanged
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;

        public ObservableCollection<Message> Messages { get; } = new ObservableCollection<Message>();

        public event PropertyChangedEventHandler PropertyChanged;

        public MessageListViewModel(
            IAuthenticationService authenticationService, 
            INavigationService navigationService)
        {
            this._authenticationService = 
                authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));

            this._navigationService =
                navigationService ?? throw new ArgumentNullException(nameof(navigationService));

            this._navigationService.FolderOpenRequested += this.NavigationService_FolderOpenRequested;
        }

        private void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            this.PropertyChanged?.Invoke(this, args);
        }

        private async void NavigationService_FolderOpenRequested(
            object sender, 
            FolderOpenRequestedEventArgs e)
        {
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
    }
}
