using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskPlanningApp.Model;
using TaskPlanningApp.CustomControls;
using TaskPlanningApp.View;

namespace TaskPlanningApp.ViewModel
{
    public class MainWindowViewModel
    {
        //Minimize, Maximize and Close buttons

        public delegate void CloseMainWindow();
        public event CloseMainWindow OnClosingEvent;

        private Action<MainWindowViewModel> CloseWindowAction = vm =>
        {
            vm.OnClosingEvent?.Invoke();
        };
        private Predicate<MainWindowViewModel> CanCloseWindow = vm => true;
        public RelayCommand<MainWindowViewModel> CloseWindow { get; set; }

        private Predicate<Window> _canMinimizeExecute = window => (window.WindowState != WindowState.Minimized);
        private Action<Window> _minimizeAction = window => window.WindowState = WindowState.Minimized;
        public RelayCommand<Window> MinimizeButton { get; set; }

        private Predicate<Window> _canMaximizeExecute = window => true;
        private Action<Window> _maximizeAction = window =>
        {
            if (window.WindowState != WindowState.Maximized)
                window.WindowState = WindowState.Maximized;
            else window.WindowState = WindowState.Normal;
        };
        public RelayCommand<Window> MaximizeButton { get; set; }

        //Userbar section
        public delegate void OpenNewWindow();
        public event OpenNewWindow MyEvent;

        private Action<MainWindowViewModel> OpenLoginWindow = vm =>
        {
            vm.MyEvent?.Invoke();
        };
        private Predicate<MainWindowViewModel> CanOpenLoginWindow = vm => true;
        public RelayCommand<MainWindowViewModel> LoginButton { get; set; }

        //Task panel section
        private TaskPanelManager _tasksPanelManager = new TaskPanelManager();
        public StackPanel TasksPanel { get; set; }
        public RelayCommand<StackPanel> AddTaskButton { get; set; }
        public RelayCommand<StackPanel> DeleteTaskButton { get; set; }

        public User TestUser = new User("Mateusz", "Kopacz");

        //Constructor
        public MainWindowViewModel()
        {
            this.MinimizeButton = new RelayCommand<Window>(_minimizeAction, _canMinimizeExecute);
            this.MaximizeButton = new RelayCommand<Window>(_maximizeAction, _canMaximizeExecute);
            this.CloseWindow = new RelayCommand<MainWindowViewModel>(CloseWindowAction, CanCloseWindow);

            this.LoginButton = new RelayCommand<MainWindowViewModel>(OpenLoginWindow, CanOpenLoginWindow);

            this.AddTaskButton = new RelayCommand<StackPanel>(_tasksPanelManager.AddNewTask, _tasksPanelManager.CanAddNewTask);
            this.DeleteTaskButton = new RelayCommand<StackPanel>(_tasksPanelManager.DeleteLastTask, _tasksPanelManager.CanDeleteTask);
        }

    }  

}
