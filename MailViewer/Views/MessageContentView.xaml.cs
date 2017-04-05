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

namespace Robot1que.MailViewer.Views
{
    public sealed partial class MessageContentView : UserControl
    {
        public MessageContentView(MessageContentViewModel viewModel)
        {
            this.DataContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

            this.InitializeComponent();
        }
    }

    public class MessageBodyTemplateSelector : DataTemplateSelector
    {
        public DataTemplate HtmlTemplate { get; set; }

        public DataTemplate TextTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return this.SelectTemplateCore(item);
        }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            var dataTemplate = new DataTemplate();

            if (item is Models.MessageBody messageBody)
            {
                dataTemplate =
                    messageBody.ContentType == Models.ContentType.Html ?
                        this.HtmlTemplate :
                        this.TextTemplate;
            }
            else
            {
                dataTemplate = base.SelectTemplateCore(item);
            }

            return dataTemplate;
        }
    }
}
