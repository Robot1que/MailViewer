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
    using MailFolderItem = TreeViewItem<MailFolder>;

    public sealed partial class FolderTreeView : UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty;

        private readonly FolderTreeViewModel _viewModel;

        public ImmutableArray<MailFolderItem> ItemsSource
        {
            get => (ImmutableArray<MailFolderItem>)this.GetValue(FolderTreeView.ItemsSourceProperty);
            set => this.SetValue(FolderTreeView.ItemsSourceProperty, value);
        }

        static FolderTreeView()
        {
            FolderTreeView.ItemsSourceProperty =
                DependencyProperty.Register(
                    nameof(FolderTreeView.ItemsSource),
                    typeof(ImmutableArray<MailFolderItem>),
                    typeof(FolderTreeView),
                    new PropertyMetadata(ImmutableArray<MailFolderItem>.Empty)
                );
        }

        public FolderTreeView(FolderTreeViewModel viewModel)
        {
            this._viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

            this._viewModel.PropertyChanged += this.ViewModel_PropertyChanged;

            this.DataContext = viewModel;
            this.InitializeComponent();
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FolderTreeViewModel.MailFolders))
            {
                var viewModel = (FolderTreeViewModel)sender;
                this.ItemsSource = 
                    viewModel.MailFolders
                        .Select(item => new MailFolderItem(item, (x) => x.ChildFolders))
                        .SelectMany(item => new MailFolderItem[] { item }.Concat(item.Children))
                        .ToImmutableArray();
            }
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await this._viewModel.Initialize();
        }
    }
}
