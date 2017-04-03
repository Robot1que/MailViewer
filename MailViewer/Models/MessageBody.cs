using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot1que.MailViewer.Models
{
    public enum ContentType
    {
        Html = 1,
        Text
    }

    public class MessageBody
    {
        public string Content { get; private set; }

        public ContentType? ContentType { get; private set; }

        private MessageBody()
        {
        }

        public static MessageBody FromData(Microsoft.Graph.ItemBody data)
        {
            return
                new MessageBody()
                {
                    Content = data.Content,
                    ContentType = data.ContentType.Convert()
                };
        }


    }
}
