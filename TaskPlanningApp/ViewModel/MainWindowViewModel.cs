using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskPlanningApp.ViewModel
{
    public class MainWindowViewModel
    {
        private IClosable window;

        //public RelayCommand<IClosable> CloseCommand {get; set;}
        public RelayCommand<IClosable> CloseCommand { get; set; }
        private static Action<IClosable> _closeWindowAction = CloseWindow;
        private static Predicate<IClosable> _closeWindowCanExecute = CanCloseWindowExecute;
        public MainWindowViewModel()
        {
            //this.CloseCommand = new RelayCommand<IClosable>(this.CloseWindow);
            this.CloseCommand = new RelayCommand<IClosable>(_closeWindowAction, _closeWindowCanExecute);
        }

        private static void CloseWindow(IClosable window)
        {
            if (window != null)
                window.Close();
        }

        private static bool CanCloseWindowExecute(IClosable window)
        {
            return window != null;
        }
    }

    public class RelayCommand<T> : ICommand
    {
        //Properties

        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public event EventHandler CanExecuteChanged;

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
