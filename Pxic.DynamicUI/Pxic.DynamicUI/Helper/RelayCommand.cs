using System.Windows.Input;

namespace Pxic.DynamicUI.Helper
{
    public class RelayCommand : ICommand
    {
        Action _TargetExecuteMethod;
        Func<bool> _TargetCanExecuteMethod;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class with the specified execute method.
        /// </summary>
        /// <param name="executeMethod">The method to execute when the command is invoked.</param>
        public RelayCommand(Action executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class with the specified execute and can-execute methods.
        /// </summary>
        /// <param name="executeMethod">The method to execute when the command is invoked.</param>
        /// <param name="canExecuteMethod">A function that determines whether the command can execute.</param>
        public RelayCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event to notify that the ability to execute the command may have changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        #region ICommand Members

        /// <summary>
        /// Determines whether the command can execute based on the can-execute method or the presence of an execute method.
        /// </summary>
        /// <param name="parameter">Unused parameter.</param>
        /// <returns><c>true</c> if the command can execute; otherwise, <c>false</c>.</returns>
        bool ICommand.CanExecute(object parameter)
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

        public event EventHandler CanExecuteChanged = delegate { };

        /// <summary>
        /// Executes the command by invoking the target execute method.
        /// </summary>
        /// <param name="parameter">Unused parameter.</param>
        void ICommand.Execute(object parameter)
        {
            if (_TargetExecuteMethod != null)
            {
                _TargetExecuteMethod();
            }
        }

        #endregion
    }

    public class RelayCommand<T> : ICommand
    {
        Action<T> _TargetExecuteMethod;
        Func<T, bool> _TargetCanExecuteMethod;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T}"/> class with the specified execute method.
        /// </summary>
        /// <param name="executeMethod">The method to execute when the command is invoked.</param>
        public RelayCommand(Action<T> executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T}"/> class with the specified execute and can-execute methods.
        /// </summary>
        /// <param name="executeMethod">The method to execute when the command is invoked.</param>
        /// <param name="canExecuteMethod">A function that determines whether the command can execute.</param>
        public RelayCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event to notify that the ability to execute the command may have changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        #region ICommand Members

        /// <summary>
        /// Determines whether the command can execute based on the can-execute method or the presence of an execute method.
        /// </summary>
        /// <param name="parameter">The parameter to evaluate for the can-execute method.</param>
        /// <returns><c>true</c> if the command can execute; otherwise, <c>false</c>.</returns>
        bool ICommand.CanExecute(object parameter)
        {
            if (_TargetCanExecuteMethod != null)
            {
                T tparm = (T)parameter;
                return _TargetCanExecuteMethod(tparm);
            }
            if (_TargetExecuteMethod != null)
            {
                return true;
            }
            return false;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        /// <summary>
        /// Executes the command by invoking the target execute method with the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter to pass to the execute method.</param>
        void ICommand.Execute(object parameter)
        {
            if (_TargetExecuteMethod != null)
            {
                _TargetExecuteMethod((T)parameter);
            }
        }

        #endregion
    }
}
