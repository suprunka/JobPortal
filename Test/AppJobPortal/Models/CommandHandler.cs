using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppJobPortal.Models
{
    public class CommandHandler : ICommand
{
    private Action action;
    private Func<bool> canExecute;
    public CommandHandler(Action action, Func<bool> canExecute)
    {
        this.action = action;
        this.canExecute = canExecute;
    }

        public CommandHandler(object v1, bool v2)
        {
        }

        public event EventHandler CanExecuteChanged;
    public bool CanExecute(object parameter)
    {
        return canExecute();
    }

    public void Execute(object parameter)
    {
        action();
    }
}
}
