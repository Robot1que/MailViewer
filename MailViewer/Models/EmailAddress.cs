using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot1que.MailViewer.Models
{
    public class EmailAddress
    {
        public string Name { get; private set; }

        public string Address { get; private set; }

        private EmailAddress()
        {
        }

        public static EmailAddress FromData(Microsoft.Graph.EmailAddress data)
        {
            return
                new EmailAddress()
                {
                    Name = data.Name,
                    Address = data.Address
                };
        }
    }
}
