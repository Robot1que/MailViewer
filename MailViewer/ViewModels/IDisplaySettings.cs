using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot1que.MailViewer.ViewModels
{
    public interface IDisplaySettings
    {
        bool IsShowUnreadOnlyEnabled { get; }

        event EventHandler IsShowUnreadOnlyEnabledChanged;
    }
}
