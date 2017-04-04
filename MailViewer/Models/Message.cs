using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot1que.MailViewer.Models
{
    public class Message
    {
        public string Id { get; private set; }

        public EmailAddress Sender { get; private set; }

        public DateTimeOffset? ReceivedDateTime { get; private set; }

        public string Subject { get; private set; }

        public string BodyPreview { get; private set; }

        public MessageBody Body { get; private set; }

        public bool? IsRead { get; private set; }

        private Message()
        {

        }

        public static Message FromData(Microsoft.Graph.Message data)
        {
            return
                new Message()
                {
                    Id = data.Id,
                    Sender = EmailAddress.FromData(data.Sender.EmailAddress),
                    Subject = data.Subject,
                    ReceivedDateTime = data.ReceivedDateTime,
                    BodyPreview = data.BodyPreview,
                    Body = MessageBody.FromData(data.Body),
                    IsRead = data.IsRead
                };
        }
    }
}
