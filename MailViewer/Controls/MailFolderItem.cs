using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace Robot1que.MailViewer.Controls
{
    public sealed class MailFolderItem : Button
    {
        public static readonly DependencyProperty IsSelectedProperty;
        public static readonly DependencyProperty IsExpandedProperty;

        public bool IsSelected
        {
            get => (bool)this.GetValue(MailFolderItem.IsSelectedProperty);
            set => this.SetValue(MailFolderItem.IsSelectedProperty, value);
        }

        public bool IsExpanded
        {
            get => (bool)this.GetValue(MailFolderItem.IsExpandedProperty);
            set => this.SetValue(MailFolderItem.IsExpandedProperty, value);
        }

        public int NestingLevel { get; }

        static MailFolderItem()
        {
            MailFolderItem.IsExpandedProperty =
                DependencyProperty.Register(
                    nameof(MailFolderItem.IsExpanded), 
                    typeof(bool), 
                    typeof(MailFolderItem),
                    new PropertyMetadata(true)
                );

            MailFolderItem.IsSelectedProperty =
                DependencyProperty.Register(
                    nameof(MailFolderItem.IsSelected),
                    typeof(bool),
                    typeof(MailFolderItem),
                    new PropertyMetadata(false, MailFolderItem.IsSelected_Changed)
                );
        }

        public MailFolderItem()
        {
            this.DefaultStyleKey = typeof(MailFolderItem);
        }

        private static void IsSelected_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mailFolderItem = (MailFolderItem)d;
            mailFolderItem.VisualStateUpdate();
        }

        private void VisualStateUpdate()
        {
            if (this.IsSelected)
            {
                VisualStateManager.GoToState(this, "Selected", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "Unselected", true);
            }
        }
    }
}
