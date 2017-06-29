using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot1que.MailViewer.ViewModels
{
    public class FilterViewModel : ViewModelBase, IDisplaySettings
    {
        private bool _isShowUnreadOnlyEnabled = false;

        public bool IsShowUnreadOnlyEnabled
        {
            get => this._isShowUnreadOnlyEnabled;
            set
            {
                if (this.PropertySet(ref this._isShowUnreadOnlyEnabled, value))
                {
                    this.OnIsShowUnreadOnlyEnabledChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler IsShowUnreadOnlyEnabledChanged;

        public void OnIsShowUnreadOnlyEnabledChanged(EventArgs args)
        {
            this.IsShowUnreadOnlyEnabledChanged?.Invoke(this, args);
        }
    }
}
