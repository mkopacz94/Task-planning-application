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
using TaskPlanningApp.Database;
using System.Collections.ObjectModel;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using TaskPlanningApp.Model.Messages;
using System.Windows.Media;

namespace TaskPlanningApp.ViewModel
{
    public class MainWindowViewModel : WorkspaceViewModel
    {
        //Minimize, Maximize and Close buttons
        //private DataContext database;
        public ICommand CloseWindow { get; set; }
        public ICommand MinimizeButton { get; set; }
        public ICommand MaximizeButton { get; set; }
        public ICommand LoginButton { get; set; }
        public ICommand DeleteTaskButton { get; set; }
        public ICommand AddTaskButton { get; set; }

        private bool userLogged = false;

        public bool UserLogged
        {
            get { return userLogged; }
            set
            {
                userLogged = value;
                OnPropertyChanged();
            }
        }
        private ImageSource imageNo1;

        private object _describeSection;
        public object DescribeSection
        {
            get { return _describeSection; }
            set
            {
                _describeSection = value;
                OnPropertyChanged();
            }
        }
        public ImageSource ImageNo1
        {
            get { return imageNo1; }
            set
            {
                imageNo1 = value;
                OnPropertyChanged();
            }
        }

        private ImageSource imageNo2;

        public ImageSource ImageNo2
        {
            get { return imageNo2; }
            set
            {
                imageNo2 = value;
                OnPropertyChanged();
            }
        }

        private ImageSource imageNo3;

        public ImageSource ImageNo3
        {
            get { return imageNo3; }
            set
            {
                imageNo3 = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ToDoTask> TasksList { get; set; }

        private CollectionView _tasksCollectionView;

        public CollectionView TasksCollectionView
        {
            get { return _tasksCollectionView; }
            set
            {
                _tasksCollectionView = value;
                ActiveTasks = TasksList.Count;
                OnPropertyChanged();
            }
        }

        private string _currentTaskTitle;

        public string CurrentTaskTitle
        {
            get { return _currentTaskTitle; }
            set
            {
                _currentTaskTitle = value;
                OnPropertyChanged();
            }
        }

        private string _currentTaskDescription;
        public string CurrentTaskDescription
        {
            get { return _currentTaskDescription; }
            set
            {
                _currentTaskDescription = value;
                OnPropertyChanged();
            }
        }

        private string _currentTaskDetails;
        public string CurrentTaskDetails
        {
            get { return _currentTaskDetails; }
            set
            {
                _currentTaskDetails = value;
                OnPropertyChanged();
            }
        }
        public int SelectedTaskIndex { get; set; }

        private ToDoTask _currentlySelectedTask;

        public ToDoTask CurrentlySelectedTask
        {
            get { return _currentlySelectedTask; }
            set
            {
                _currentlySelectedTask = value;
                if (_currentlySelectedTask != null)
                {
                    CurrentTaskTitle = value.Title;
                    CurrentTaskDescription = value.ShortDescription;
                    CurrentTaskDetails = value.LongDescription;
                }
                else
                {
                    CurrentTaskTitle = string.Empty;
                    CurrentTaskDescription = string.Empty;
                    CurrentTaskDetails = string.Empty;
                }
                OnPropertyChanged();
            }
        }
        private double mainWindowOpacity = 1.0;
        public double MainWindowOpacity
        {
            get
            {
                return mainWindowOpacity;
            }
            set
            {
                mainWindowOpacity = value;
                OnPropertyChanged();
            }
        }

        private string _currenUserName;

        public string CurrentUserName
        {
            get { return _currenUserName; }
            set
            {
                _currenUserName = value;
                OnPropertyChanged();
            }
        }

        private int _activeTasks;
        public int ActiveTasks
        {
            get { return _activeTasks; }
            set
            {
                _activeTasks = value;
                OnPropertyChanged();
            }
        }

        private int _finishedTasks;

        public int FinishedTasks
        {
            get { return _finishedTasks; }
            set
            {
                _finishedTasks = value;
                OnPropertyChanged();
            }
        }
        private string _loginButtonText;

        public string LoginButtonText
        {
            get { return _loginButtonText; }
            set
            {
                _loginButtonText = value;
                OnPropertyChanged();
            }
        }

        private Window loginWindow;

        public MainWindowViewModel()
        {
            //database = new DataContext();
            DescribeSection = new TaskDescriptionView();

            MinimizeButton = new RelayCommand<Window>(_minimizeAction, _canMinimizeExecute);
            MaximizeButton = new RelayCommand<Window>(_maximizeAction, _canMaximizeExecute);
            CloseWindow = new RelayCommand<MainWindowViewModel>(CloseWindowAction, CanCloseWindow);
            LoginButton = new RelayCommand<MainWindowViewModel>(OpenLoginWindow, CanOpenLoginWindow);
            DeleteTaskButton = new RelayCommand<MainWindowViewModel>(DeleteTask, (vm) => true);
            AddTaskButton = new RelayCommand<MainWindowViewModel>(AddTask, (vm) => true);

            TasksList = new ObservableCollection<ToDoTask>();

            LoginButtonText = "ZALOGUJ";

            Messenger.Default.Register<LoggedUserMessage>(this, HandleMessage);
            Messenger.Default.Register<AddNewTaskMessage>(this, HandleAddTaskMessage);
            Messenger.Default.Register<UpdateTaskMessage>(this, UpdateTask);
        }

        private void UpdateTask(UpdateTaskMessage msg)
        {
            using (DataContext database = new DataContext())
            {
                var tasks = from t in database.Tasks where t.Id == msg.TaskToUpdate.Id select t;

                foreach (ToDoTask task in tasks)
                {
                    task.Title = msg.TaskToUpdate.Title;
                    task.ShortDescription = msg.TaskToUpdate.ShortDescription;
                    task.LongDescription = msg.TaskToUpdate.LongDescription;
                    task.Deadline = msg.TaskToUpdate.Deadline;
                }

                database.SaveChanges();
            }

            CurrentTaskTitle = msg.TaskToUpdate.Title;
            DescribeSection = msg.TaskToUpdate.ShortDescription;
            CurrentTaskDetails = msg.TaskToUpdate.LongDescription;
            
            updateTasksList(TasksList);
        }
        private void updateTasksList(ObservableCollection<ToDoTask> tasksList)
        {
            TasksCollectionView = new CollectionView(tasksList);
        }

        private void generateUserTasksList(User user)
        {
            using (DataContext database = new DataContext())
            {
                var tasks = from t in database.Tasks where t.User.Username == CurrentUserName select t;


                foreach (ToDoTask task in tasks)
                {
                    TasksList.Add(task);
                }
            }
        }
        private void HandleAddTaskMessage(AddNewTaskMessage taskToAdd)
        {
            using (DataContext database = new DataContext())
            {
                var users = from u in database.Users where u.Username == CurrentUserName select u;

                foreach (User user in users)
                {
                    taskToAdd.TaskToAdd.User = user;
                }

                database.Tasks.Add(taskToAdd.TaskToAdd);
                database.SaveChanges();
            }

            TasksList.Add(taskToAdd.TaskToAdd);
            updateTasksList(TasksList);
            CalculateFinishedTasks();
        }

        private void HandleMessage(LoggedUserMessage message)
        {
            CurrentUserName = message.LoggedUser.Username;
            UserLogged = true;

            generateUserTasksList(message.LoggedUser);
            updateTasksList(TasksList);
            CalculateFinishedTasks();
            LoginButtonText = "WYLOGUJ";
        }

        private readonly Action<MainWindowViewModel> CloseWindowAction = vm => vm.Window.Close();
        private readonly Predicate<MainWindowViewModel> CanCloseWindow = vm => true;

        private readonly Predicate<Window> _canMinimizeExecute = window => (window.WindowState != WindowState.Minimized);
        private readonly Action<Window> _minimizeAction = window => window.WindowState = WindowState.Minimized;

        private readonly Predicate<Window> _canMaximizeExecute = window => true;
        private readonly Action<Window> _maximizeAction = window =>
        {
            if (window.WindowState != WindowState.Maximized)
                window.WindowState = WindowState.Maximized;
            else window.WindowState = WindowState.Normal;
        };

        private readonly Action<MainWindowViewModel> DeleteTask = vm =>
         {
             if (vm.CurrentlySelectedTask != null)
             {
                 using (DataContext database = new DataContext())
                 {
                     foreach(ToDoTask task in database.Tasks)
                     {
                         if(task.Id==vm.CurrentlySelectedTask.Id)
                         {
                             database.Tasks.Remove(task);
                         }
                     }
                     
                     database.SaveChanges();
                 }
                 vm.TasksList.Remove(vm.CurrentlySelectedTask);
                 vm.TasksCollectionView = new CollectionView(vm.TasksList);
                 vm.CalculateFinishedTasks();

             }
         };

        private readonly Action<MainWindowViewModel> AddTask = vm =>
        {
            if (vm.UserLogged)
            {
                TaskEditViewModel taskEditVM = new TaskEditViewModel(false);
                taskEditVM.CancelAddingTaskEvent += () =>
                {
                    vm.DescribeSection = new TaskDescriptionViewModel();
                };
                vm.DescribeSection = taskEditVM;
            }
        };

        public void EditSelectedTask(ToDoTask taskToEdit)
        {
            if(UserLogged)
            {
                TaskEditViewModel taskEditVM = new TaskEditViewModel(true, taskToEdit);

                taskEditVM.CancelAddingTaskEvent += () =>
                {
                    DescribeSection = new TaskDescriptionViewModel();
                };
                DescribeSection = taskEditVM;
            }
        }

        private readonly Action<MainWindowViewModel> OpenLoginWindow = vm =>
        {
            if (!vm.UserLogged)
            {
                LoginViewModel loginVM = new LoginViewModel();
                loginVM.LoginWindowClosing += () => vm.MainWindowOpacity = 1.0;

                vm.loginWindow = WindowBuilder.BuildWindow(loginVM);
                vm.MainWindowOpacity = 0.3;
                vm.loginWindow.ShowDialog();
            }
            else
            {
                MainWindowStateManager manager = new MainWindowStateManager();
                manager.CleanUpUser(vm);

                vm.FinishedTasks = 0;
                vm.UserLogged = false;
                vm.LoginButtonText = "ZALOGUJ";
            }

        };
        private readonly Predicate<MainWindowViewModel> CanOpenLoginWindow = vm => true;

        public void SetFinishedTask(ToDoTask clickedTask, bool state)
        {
            using (DataContext database = new DataContext())
            {
                foreach(ToDoTask task in database.Tasks)
                {
                    if (task.Id == clickedTask.Id)
                        task.Finished = state;
                }
                database.SaveChanges();
            }
        }

        public void CalculateFinishedTasks()
        {
            var finishedTasks = from ft in TasksList where ft.Finished == true select ft;

            if (finishedTasks != null)
            {
                FinishedTasks = finishedTasks.ToList().Count;
            }
            else
                FinishedTasks = 0;
        }
    }  

}
