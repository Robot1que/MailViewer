using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ComponentModel;

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

namespace Robot1que.MailViewer.Views
{
    public sealed partial class MessageContentView : UserControl
    {
        private readonly MessageContentViewModel _viewModel;

        public MessageContentView(MessageContentViewModel viewModel)
        {
            this._viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

            this.DataContext = this._viewModel;
            this.InitializeComponent();

            this._viewModel.PropertyChanged += this.ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MessageContentViewModel.MessageBody))
            {
                var viewModel = (MessageContentViewModel)sender;

                VisualStateManager.GoToState(this, this.WithoutContentState.Name, true);
                if (viewModel.MessageBody != null)
                {
                    this.ConentLoad(viewModel.MessageBody);
                }
            }
        }

        private void ConentLoad(MessageBody messageBody)
        {
            if (messageBody.ContentType == ContentType.Text)
            {
                VisualStateManager.GoToState(this, this.WithContentState.Name, true);
            }
            else
            {
                this.WebView.NavigationCompleted += this.WebView_NavigationCompleted;
                this.WebView.NavigateToString(messageBody.Content);
            }
        }

        private void WebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            this.WebView.NavigationCompleted -= this.WebView_NavigationCompleted;
            VisualStateManager.GoToState(this, this.WithContentState.Name, true);
        }
    }
}
