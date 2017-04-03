using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Robot1que.MailViewer.ViewModels;
using Robot1que.MailViewer.Models;
using Robot1que.MailViewer.Controls;
using System.Collections.Immutable;
using System.ComponentModel;

namespace Robot1que.MailViewer.Views
{
    using TreeViewItemData = TreeViewItemData<MailFolder>;

    public sealed partial class FolderListView : UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty;

        private readonly FolderListViewModel _viewModel;

        public ImmutableArray<TreeViewItemData> ItemsSource
        {
            get => (ImmutableArray<TreeViewItemData>)this.GetValue(FolderListView.ItemsSourceProperty);
            set => this.SetValue(FolderListView.ItemsSourceProperty, value);
        }

        static FolderListView()
        {
            FolderListView.ItemsSourceProperty =
                DependencyProperty.Register(
                    nameof(FolderListView.ItemsSource),
                    typeof(ImmutableArray<TreeViewItemData>),
                    typeof(FolderListView),
                    new PropertyMetadata(ImmutableArray<TreeViewItemData>.Empty)
                );
        }

        public FolderListView(FolderListViewModel viewModel)
        {
            this._viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

            this._viewModel.PropertyChanged += this.ViewModel_PropertyChanged;

            this.DataContext = viewModel;
            this.InitializeComponent();
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FolderListViewModel.MailFolders))
            {
                var viewModel = (FolderListViewModel)sender;
                this.ItemsSource = 
                    viewModel.MailFolders
                        .Select(item => new TreeViewItemData(item, (x) => x.ChildFolders))
                        .SelectMany(item => new TreeViewItemData[] { item }.Concat(item.Children))
                        .ToImmutableArray();
            }
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await this._viewModel.Initialize();
        }

        private void MailFolderTree_SelectedItemChanged(object sender, EventArgs e)
        {
            var mailFolderTree = (MailFolderTree)sender;
            var treeViewItemData = (TreeViewItemData)mailFolderTree.SelectedItem.DataContext;
            this._viewModel.MailFolderSelect(treeViewItemData.Data.Id);
        }
    }
}
