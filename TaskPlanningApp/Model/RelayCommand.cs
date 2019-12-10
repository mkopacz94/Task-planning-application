using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskPlanningApp.Model
{
    public class RelayCommand<T> : ICommand
    {
        //Properties

        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand()
        {
            _execute = null;
            _canExecute = null;
        }
        public RelayCommand(Action<T> execute) : this(execute, null)
        { }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }

    public class RelayCommand : ICommand
    {
        //Properties

        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand()
        {
            _execute = null;
            _canExecute = null;
        }
        public RelayCommand(Action<object> execute) : this(execute, null)
        { }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute((object)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((object)parameter);
        }
    }
}
