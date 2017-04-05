using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml;

namespace Robot1que.MailViewer.Extensions
{
    public static class WebView
    {
        public static readonly DependencyProperty HtmlProperty;

        static WebView()
        {
            WebView.HtmlProperty =
                DependencyProperty.RegisterAttached(
                    "Html",
                    typeof(string),
                    typeof(WebView),
                    new PropertyMetadata(null, WebView.OnHtmlChanged)
                );
        }

        public static string GetHtml(DependencyObject obj)
        {
            return (string)obj.GetValue(WebView.HtmlProperty);
        }

        public static void SetHtml(DependencyObject obj, string value)
        {
            obj.SetValue(WebView.HtmlProperty, value);
        }

        private static void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Windows.UI.Xaml.Controls.WebView webView)
            {
                webView.NavigateToString((string)e.NewValue);
            }
        }
    }
}
