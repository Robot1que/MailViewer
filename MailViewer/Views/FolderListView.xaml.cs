using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using System.Collections.Immutable;
using System.ComponentModel;

using Robot1que.MailViewer.ViewModels;
using Robot1que.MailViewer.Models;
using Robot1que.MailViewer.Controls;

namespace Robot1que.MailViewer.Views
{
    public sealed partial class FolderListView : UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty;

        private readonly FolderListViewModel _viewModel;
        private readonly Dictionary<MailFolder, int> _mailFolderNestingInfo = 
            new Dictionary<MailFolder, int>();

        public ImmutableArray<MailFolder> ItemsSource
        {
            get => (ImmutableArray<MailFolder>)this.GetValue(FolderListView.ItemsSourceProperty);
            set => this.SetValue(FolderListView.ItemsSourceProperty, value);
        }
        
        static FolderListView()
        {
            FolderListView.ItemsSourceProperty =
                DependencyProperty.Register(
                    nameof(FolderListView.ItemsSource),
                    typeof(ImmutableArray<MailFolder>),
                    typeof(FolderListView),
                    new PropertyMetadata(ImmutableArray<MailFolder>.Empty)
                );
        }

        public FolderListView(FolderListViewModel viewModel, IDisplaySettings displaySettings)
        {
            if (displaySettings == null)
            {
                throw new ArgumentNullException(nameof(displaySettings));
            }

            this._viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

            this._viewModel.PropertyChanged += this.ViewModel_PropertyChanged;
            displaySettings.IsShowUnreadOnlyEnabledChanged += 
                this.DisplaySettings_IsShowUnreadOnlyEnabledChanged;

            this.DataContext = viewModel;
            this.InitializeComponent();

            this.MailFolderTree.ItemNestingLevelProvider = this.MailFolderNestingLevelGet;
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FolderListViewModel.MailFolders))
            {
                var viewModel = (FolderListViewModel)sender;

                this._mailFolderNestingInfo.Clear();
                this.MailFoldersFlatten(
                    viewModel.MailFolders.OrderBy(item => item.DisplayName, new FolderSortComparer()), 
                    this._mailFolderNestingInfo
                );

                this.ItemsSource = this._mailFolderNestingInfo.Keys.ToImmutableArray();
            }
        }

        private void MailFoldersFlatten(
            IEnumerable<MailFolder> items,
            Dictionary<MailFolder, int> itemNestingInfo,
            int nestingLevel = 0)
        {
            foreach (var item in items)
            {
                itemNestingInfo.Add(item, nestingLevel);
                this.MailFoldersFlatten(item.ChildFolders, itemNestingInfo, nestingLevel + 1);
            }
        }

        private int MailFolderNestingLevelGet(object item)
        {
            var mailFolder = (MailFolder)item;
            return this._mailFolderNestingInfo[mailFolder];
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await this._viewModel.Initialize();
        }

        private void MailFolderTree_SelectedItemChanged(object sender, EventArgs e)
        {
            var mailFolderTree = (MailFolderTree)sender;
            var mailFolder = (MailFolder)mailFolderTree.SelectedItem.DataContext;
            this._viewModel.MailFolderSelect(mailFolder.Id);
        }

        private void UnreadFilterEnable(bool isEnabled)
        {
            this.MailFolderTree.Filter = 
                isEnabled ? (Func<object, bool>)this.UnreadMailFolderFilter : null;

            this.MailFolderTree.DataViewUpdate();
        }

        private void DisplaySettings_IsShowUnreadOnlyEnabledChanged(object sender, EventArgs e)
        {
            var displaySettings = (IDisplaySettings)sender;
            this.UnreadFilterEnable(displaySettings.IsShowUnreadOnlyEnabled);
        }

        private bool UnreadMailFolderFilter(object item)
        {
            var mailFolder = (MailFolder)item;
            return
                (mailFolder.UnreadItemCount ?? 0) > 0 ||
                mailFolder.ChildFolders.Any(child => this.UnreadMailFolderFilter(child));
        }
    }

    public class FolderSortComparer : IComparer<string>
    {
        private static readonly string[] Preceding =
            { "Inbox", "Junk Email", "Drafts", "Sent Items", "Deleted Items" };

        private static readonly string[] Following = { "Archive" };

        public int Compare(string x, string y)
        {
            var xScore = this.KeyGet(x);
            var yScore = this.KeyGet(y);
            return xScore.CompareTo(yScore);
        }

        private int KeyGet(string value)
        {
            var key = Array.IndexOf(FolderSortComparer.Preceding, value);
            if (key == -1)
            {
                key = 
                    FolderSortComparer.Preceding.Length +
                    Array.IndexOf(FolderSortComparer.Following, value) + 1;
            }
            return key;
        }
    }
}
