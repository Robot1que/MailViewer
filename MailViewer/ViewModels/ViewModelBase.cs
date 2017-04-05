using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Robot1que.MailViewer.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            this.PropertyChanged?.Invoke(this, args);
        }

        protected bool PropertySet<T>(
            ref T storage, 
            T value, 
            [CallerMemberName] string propertyName = "")
        {
            var isChanged = false;

            if (!object.Equals(storage, value))
            {
                isChanged = true;
                storage = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }

            return isChanged;
        }
    }
}
