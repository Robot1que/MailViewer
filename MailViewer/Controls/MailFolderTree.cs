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

        public Func<object, int> ItemNestingLevelProvider { get; set; } = null;

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

            var container = (Control)element;
            this.ContainerPaddingSet(container, item);
        }

        private void ContainerPaddingSet(Control container, object item)
        {
            var nestingLevel = 
                this.ItemNestingLevelProvider != null ? 
                    this.ItemNestingLevelProvider.Invoke(item) : 
                    0;

            container.Padding =
                new Thickness(
                    this.ItemPadding.Left + this.ItemPadding.Left * nestingLevel,
                    this.ItemPadding.Top,
                    this.ItemPadding.Right,
                    this.ItemPadding.Bottom
                );
        }

        private void ContainerPaddingUpdate()
        {
            foreach (var item in this.Items)
            {
                var container = (Control)this.ContainerFromItem(item);
                this.ContainerPaddingSet(container, item);
            }
        }

        private static void ItemPadding_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mailFolderTree = (MailFolderTree)d;
            mailFolderTree.ContainerPaddingUpdate();
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
