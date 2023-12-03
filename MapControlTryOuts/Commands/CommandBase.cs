using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MapControlTryOuts.Commands
{
    public class CommandBase : ICommand
    {
        Action _TargetExecuteMethod;
        Func<bool> _TargetCanExecuteMethod;

        public CommandBase(Action targetExecuteMethod)
        {
            _TargetExecuteMethod = targetExecuteMethod;
        }

        public CommandBase(Action targetExecuteMethod, Func<bool> targetCanExecuteMethod) : this(targetExecuteMethod)
        {
            _TargetCanExecuteMethod = targetCanExecuteMethod;
        }

        public event EventHandler CanExecuteChanged = delegate {};

        public virtual bool CanExecute(object? parameter)
        {
            if (_TargetCanExecuteMethod != null)
            {
                return _TargetCanExecuteMethod();
            }

            if (_TargetExecuteMethod != null)
            {
                return true;
            }

            return false;
        }

        public virtual void Execute(object? parameter)
        {
            _TargetExecuteMethod?.Invoke();
        }

        public virtual void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
