using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Robot1que.MailViewer.Converters
{
    public class IndentationToPaddingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var result = value;

            if (
                value is int indentCount && 
                parameter is Thickness padding)
            {
                result = 
                    new Thickness(
                        padding.Left + padding.Left * indentCount, 
                        padding.Top, 
                        padding.Right, 
                        padding.Bottom
                    );
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
