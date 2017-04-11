using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Robot1que.MailViewer.Models;

namespace Robot1que.MailViewer.ViewModels
{
    public class FolderListViewModel: ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;

        private ImmutableArray<MailFolder> _mailFolders = ImmutableArray<MailFolder>.Empty;
        private bool _hasUnreadMessages;

        public ImmutableArray<MailFolder> MailFolders
        {
            get => this._mailFolders;
            private set => this.PropertySet(ref this._mailFolders, value);
        }

        public bool HasUnreadMessages
        {
            get => this._hasUnreadMessages;
            private set => this.PropertySet(ref this._hasUnreadMessages, value);
        }

        public FolderListViewModel(
            IAuthenticationService authenticationService, 
            INavigationService navigationService)
        {
            this._authenticationService = 
                authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));

            this._navigationService =
                navigationService ?? throw new ArgumentNullException(nameof(navigationService));


        }

        public async Task Initialize()
        {
            await this.MailFolderRefresh();
        }

        public void MailFolderSelect(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            this._navigationService.MailFolderOpen(id);
        }

        private async Task MailFolderRefresh()
        {
            var mailFolders = await this.MailFolderGet();

            this.MailFolders = mailFolders.ToImmutableArray();
            this.HasUnreadMessages = this.HasUnreadMessagesGet(mailFolders);
        }

        private bool HasUnreadMessagesGet(IEnumerable<MailFolder> items)
        {
            return 
                items.Any(
                    item => (item.UnreadItemCount ?? 0) > 0 || (
                        item.ChildFolders != null &&
                        this.HasUnreadMessagesGet(item.ChildFolders)
                    )
                );
        }

        private async Task<MailFolder[]> MailFolderGet()
        {
            var dataItems = new List<Microsoft.Graph.MailFolder>();
            var graphService = this._authenticationService.GraphServiceGet();

            var request = graphService.Me.MailFolders.Request().Expand("ChildFolders");
            while (request != null)
            {
                var folders = await request.GetAsync();
                dataItems.AddRange(folders);

                request = folders.NextPageRequest;
            }

            return dataItems.Select(item => MailFolder.FromData(item)).ToArray();
        }
    }
}