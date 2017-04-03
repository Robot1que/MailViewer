using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Robot1que.MailViewer.Controls
{
    public sealed class MailFolderTree : ItemsControl
    {
        public MailFolderItem SelectedItem { get; private set; } = null;

        public event EventHandler SelectedItemChanged;

        public MailFolderTree()
        {
            this.DefaultStyleKey = typeof(MailFolderTree);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            var mailFolderItem = new MailFolderItem();
            mailFolderItem.Click += this.MailFolderItem_Click;
            return mailFolderItem;
        }

        private void OnSelectedItemChanged(EventArgs args)
        {
            this.SelectedItemChanged?.Invoke(this, args);
        }

        private void MailFolderItem_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectedItem != null)
            {
                this.SelectedItem.IsSelected = false;
            }

            this.SelectedItem = (MailFolderItem)sender;
            this.SelectedItem.IsSelected = true;

            this.OnSelectedItemChanged(EventArgs.Empty);
        }
    }
}
