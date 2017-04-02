using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Robot1que.MailViewer.Models;

namespace Robot1que.MailViewer.ViewModels
{
    public class FolderTreeViewModel: INotifyPropertyChanged
    {
        private readonly IAuthenticationService _authenticationService;

        private ImmutableArray<MailFolder> _mailFolders = ImmutableArray<MailFolder>.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        public ImmutableArray<MailFolder> MailFolders
        {
            get
            {
                return this._mailFolders;
            }
            private set
            {
                if (this._mailFolders != value)
                {
                    this._mailFolders = value;
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MailFolders)));
                }
            }
        }

        public FolderTreeViewModel(IAuthenticationService authenticationService)
        {
            this._authenticationService = 
                authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        public async Task Initialize()
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

            this.MailFolders = dataItems.Select(item => MailFolder.FromData(item)).ToImmutableArray();
        }
    }
}
