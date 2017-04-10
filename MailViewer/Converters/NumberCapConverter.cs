using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Robot1que.MailViewer.Converters
{
    public class NumberCapConverter : IValueConverter
    {
        private const string CapFormat = "{0}+";

        private int MaxValue { get; set; } = 99;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var result = value;

            if (value is int intValue)
            {
                result = 
                    intValue > this.MaxValue ? 
                        string.Format(NumberCapConverter.CapFormat, this.MaxValue) :
                        $"{intValue}";
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
