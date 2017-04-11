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

        public Func<object, bool> Filter { get; set; } = null;

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

        public void DataViewUpdate()
        {
            Func<object, Visibility> visibilityGetter;

            if (this.Filter != null)
            {
                visibilityGetter = 
                    (item) => this.Filter.Invoke(item) ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                visibilityGetter = (item) => Visibility.Visible;
            }
            
            foreach (var item in this.Items)
            {
                var element = (UIElement)this.ContainerFromItem(item);
                element.Visibility = visibilityGetter.Invoke(item);
            }
        }

        //protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        //{
        //    base.PrepareContainerForItemOverride(element, item);
        //}

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
