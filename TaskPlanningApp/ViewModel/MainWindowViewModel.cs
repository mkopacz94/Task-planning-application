using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TaskPlanningApp.ViewModel
{
    public class MainWindowViewModel
    {
        public RelayCommand<IClosable> CloseButton { get; set; }
        public RelayCommand<Window> MinimizeButton { get; set; }
        public RelayCommand<Window> MaximizeButton { get; set; }
        public MainWindowViewModel()
        {
            this.CloseButton = new RelayCommand<IClosable>(CloseCommandExecuter.ActionToExecute, CloseCommandExecuter.CanCommandExecute);
            this.MinimizeButton = new RelayCommand<Window>(MinimizeCommandExecuter.ActionToExecute, MinimizeCommandExecuter.CanCommandExecute);
            this.MaximizeButton = new RelayCommand<Window>(MaximizeCommandExecuter.ActionToExecute, MaximizeCommandExecuter.CanCommandExecute);
        }

    }

    public static class CloseCommandExecuter
    {
        public static Predicate<IClosable> CanCommandExecute = window =>
        {
            if (window != null) return true;
            else throw new ArgumentNullException("Main window argument is a null argument");
        };

        public static Action<IClosable> ActionToExecute = window => window.Close();
    }

    public static class MinimizeCommandExecuter
    {
        public static Predicate<Window> CanCommandExecute = window => (window.WindowState != WindowState.Minimized);

        public static Action<Window> ActionToExecute = window => window.WindowState = WindowState.Minimized;
    }

    public static class MaximizeCommandExecuter
    {
        public static Predicate<Window> CanCommandExecute = window => true;

        public static Action<Window> ActionToExecute = window =>
        {
            if (window.WindowState != WindowState.Maximized)
                window.WindowState = WindowState.Maximized;
            else window.WindowState = WindowState.Normal;
        };
    }
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

}
