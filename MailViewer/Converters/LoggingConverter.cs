using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Robot1que.MailViewer.Converters
{
    public class LoggingConverter : IValueConverter
    {
        public string Tag { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            this.WriteTagIfAvailable();
            System.Diagnostics.Debug.WriteLine($"LoggingConverter.Convert(): {value}");

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            this.WriteTagIfAvailable();
            System.Diagnostics.Debug.WriteLine($"LoggingConverter.ConvertBack(): {value}");

            return value;
        }

        private void WriteTagIfAvailable()
        {
            if (!string.IsNullOrEmpty(this.Tag))
            {
                System.Diagnostics.Debug.Write($"{this.Tag} ");
            }
        }
    }
}
