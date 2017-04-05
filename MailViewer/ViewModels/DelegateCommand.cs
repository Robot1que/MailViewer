using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Robot1que.MailViewer.ViewModels
{
    public class DelegateCommand<T> : ICommand 
        where T : class
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute = null;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<T> execute)
        {
            this._execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            this._execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this._canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        bool ICommand.CanExecute(object parameter)
        {
            return this.CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            this.Execute((T)parameter);
        }

        public bool CanExecute(T parameter)
        {
            return this._canExecute != null ? this._canExecute.Invoke(parameter) : true;
        }

        public void Execute(T parameter)
        {
            this._execute.Invoke(parameter);
        }

        public void CanExecuteChangedRaise()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
