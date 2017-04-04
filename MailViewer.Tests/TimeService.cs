using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot1que.MailViewer.Tests
{
    public class TimeServiceStub : ITimeService
    {
        public DateTime Now { get; set; }
    }
}
