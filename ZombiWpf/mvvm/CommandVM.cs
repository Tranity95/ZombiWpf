using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ZombiWpf.mvvm
{
    public class CommandVM : ICommand
    {
        Action Action;
        public CommandVM(Action action)
        {
            this.Action = action;
        }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            Action();
        }

    }

    public class CommandVM<T> : ICommand where T : class
    {
        Action<T> action;
        private readonly Func<T, bool> canExecute;

        public CommandVM(Action<T> action, Func<T, bool> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return canExecute((T)parameter);
        }
        public void Execute(object parameter)
        {
            action((T)parameter);
        }
    }
}
