using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot1que.MailViewer
{
    public interface ITimeService
    {
        DateTime Now { get; }
    }

    internal class DefaultTimeService : ITimeService
    {
        public DateTime Now => DateTime.Now;
    }

    public static class TimeService
    {
        public static ITimeService Current { get; private set; }

        static TimeService()
        {
            TimeService.Reset();
        }

        public static void Set(ITimeService value)
        {
            TimeService.Current = value ?? throw new ArgumentNullException(nameof(value));
        }

        public static void Reset()
        {
            TimeService.Current = new DefaultTimeService();
        }
    }
}
