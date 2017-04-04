using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Robot1que.MailViewer.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime? dateTime = null;

            if (value is DateTimeOffset dateTimeOffsetValue)
            {
                dateTime = dateTimeOffsetValue.ToLocalTime().DateTime;
            }
            else if (value is DateTime dateTimeValue)
            {
                dateTime = dateTimeValue.ToLocalTime();
            }

            var result = value;

            if (dateTime.HasValue)
            {
                var now = TimeService.Current.Now;
                var timeSpan = now - dateTime.Value;

                if (timeSpan < TimeSpan.FromMinutes(1))
                {
                    result = "a moment ago";
                }
                else if (now.Date == dateTime.Value.Date)
                {
                    result = dateTime.Value.ToString("HH:mm");
                }
                else
                {
                    result = dateTime.Value.ToString("yyyy-MM-dd");
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
