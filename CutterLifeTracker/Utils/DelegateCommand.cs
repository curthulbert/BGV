using System;
using System.Windows.Input;

namespace CutterLifeTracker.Utils
{
    public class DelegateCommand : ICommand
    {
        //represents the method that defines a set of 
        //criteria and determines whether the specified
        //object meets those criteria
        private Predicate<object> _canExecute;

        //instead of having to explicitly define a delegate
        //that encapsulates a method with a single parameter
        //i.e. delegate void SomeMethod(string message);
        //we can use the Action<T> delegate to accomplish the same thing.
        //Action<T> requires that the encapsulated method take 1 parameter
        //and doesn't return a value        
        private Action<object> _method;
        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<object> method)
            : this(method, null)
        {
        }

        public DelegateCommand(Action<object> method, Predicate<object> canExecute)
        {
            _method = method;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _method.Invoke(parameter);
        }

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            var canExecuteChanged = CanExecuteChanged;

            if (canExecuteChanged != null)
                canExecuteChanged(this, e);
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged(EventArgs.Empty);
        }
    }
}
