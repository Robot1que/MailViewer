using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot1que.MailViewer.Models
{
    public static class ConvertExtensions
    {
        public static ContentType? Convert(this Microsoft.Graph.BodyType? value)
        {
            ContentType? result = null;

            if (value.HasValue)
            {
                result = 
                    value.Value == Microsoft.Graph.BodyType.Text ? 
                        ContentType.Text : 
                        ContentType.Html;
            }

            return result;
        }
    }
}
