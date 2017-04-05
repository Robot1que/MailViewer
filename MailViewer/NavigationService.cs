using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot1que.MailViewer
{
    public interface INavigationService
    {
        event EventHandler<MailFolderOpenRequestedEventArgs> MailFolderOpenRequested;
        event EventHandler<MessageOpenRequestedEventArgs> MessageOpenRequested;

        void MailFolderOpen(string id);
        void MessageOpen(string id);
    }

    public class NavigationService : INavigationService
    {
        public event EventHandler<MailFolderOpenRequestedEventArgs> MailFolderOpenRequested;

        public event EventHandler<MessageOpenRequestedEventArgs> MessageOpenRequested;

        public NavigationService()
        {
        }

        private void OnFolderOpenRequested(MailFolderOpenRequestedEventArgs args)
        {
            this.MailFolderOpenRequested?.Invoke(this, args);
        }

        private void OnMessageOpenRequested(MessageOpenRequestedEventArgs args)
        {
            this.MessageOpenRequested?.Invoke(this, args);
        }

        public void MailFolderOpen(string id)
        {
            this.OnFolderOpenRequested(new MailFolderOpenRequestedEventArgs(id));
        }

        public void MessageOpen(string id)
        {
            this.OnMessageOpenRequested(new MessageOpenRequestedEventArgs(id));
        }
    }

    public class MailFolderOpenRequestedEventArgs
    {
        public string MailFolderId { get; }

        public MailFolderOpenRequestedEventArgs(string mailFolderId)
        {
            this.MailFolderId = mailFolderId ?? throw new ArgumentNullException(nameof(mailFolderId));
        }
    }

    public class MessageOpenRequestedEventArgs
    {
        public string MessageId { get; }

        public MessageOpenRequestedEventArgs(string messageId)
        {
            this.MessageId = messageId ?? throw new ArgumentNullException(nameof(messageId));
        }
    }
}
