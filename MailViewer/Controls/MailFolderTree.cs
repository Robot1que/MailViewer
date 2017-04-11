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
        public static readonly DependencyProperty ItemPaddingProperty;

        public MailFolderItem SelectedItem { get; private set; } = null;

        public Func<object, bool> Filter { get; set; } = null;

        public Thickness ItemPadding
        {
            get => (Thickness)this.GetValue(MailFolderTree.ItemPaddingProperty);
            set => this.SetValue(MailFolderTree.ItemPaddingProperty, value);
        }

        static MailFolderTree()
        {
            MailFolderTree.ItemPaddingProperty =
                DependencyProperty.Register(
                    nameof(MailFolderTree.ItemPadding),
                    typeof(Thickness),
                    typeof(MailFolderTree),
                    new PropertyMetadata(new Thickness(), MailFolderTree.ItemPadding_Changed)
                );
        }

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

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            var container = (ContentControl)element;
            var mailFolderItemData = (ITreeViewItemData)item;

            container.Content = mailFolderItemData.Data;

            this.ItemPaddingSet(container, mailFolderItemData.NestingLevel);
        }

        private void ItemPaddingSet(Control control, int nestingLevel)
        {
            control.Padding =
                new Thickness(
                    this.ItemPadding.Left + this.ItemPadding.Left * nestingLevel,
                    this.ItemPadding.Top,
                    this.ItemPadding.Right,
                    this.ItemPadding.Bottom
                );
        }

        private void ItemPaddingUpdate()
        {
            foreach (var item in this.Items)
            {
                var container = (Control)this.ContainerFromItem(item);
                var treeViewItemData = (ITreeViewItemData)item;

                this.ItemPaddingSet(container, treeViewItemData.NestingLevel);
            }
        }

        private static void ItemPadding_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mailFolderTree = (MailFolderTree)d;
            mailFolderTree.ItemPaddingUpdate();
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
