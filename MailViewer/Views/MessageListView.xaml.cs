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

namespace Robot1que.MailViewer.Views
{
    public sealed partial class MessageListView : UserControl
    {
        public MessageListView(MessageListViewModel viewModel)
        {
            this.DataContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            this.InitializeComponent();
        }
    }
}
