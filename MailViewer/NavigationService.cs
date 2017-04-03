using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot1que.MailViewer
{
    public interface INavigationService
    {
        event EventHandler<FolderOpenRequestedEventArgs> FolderOpenRequested;

        void MailFolderOpen(string id);
    }

    public class NavigationService : INavigationService
    {
        public event EventHandler<FolderOpenRequestedEventArgs> FolderOpenRequested;

        public NavigationService()
        {
        }

        private void OnFolderOpenRequested(FolderOpenRequestedEventArgs args)
        {
            this.FolderOpenRequested?.Invoke(this, args);
        }

        public void MailFolderOpen(string id)
        {
            this.OnFolderOpenRequested(new FolderOpenRequestedEventArgs(id));
        }
    }

    public class FolderOpenRequestedEventArgs
    {
        public string MailFolderId { get; }

        public FolderOpenRequestedEventArgs(string mailFolderId)
        {
            this.MailFolderId = mailFolderId ?? throw new ArgumentNullException(nameof(mailFolderId));
        }
    }
}
