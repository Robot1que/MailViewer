using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml.Data;

namespace Robot1que.MailViewer.Converters
{
    public class NullToStringConverter : IValueConverter
    {
        public string NullString { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value != null ? value : this.NullString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
