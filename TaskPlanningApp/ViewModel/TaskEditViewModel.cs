using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskPlanningApp.Model;
using TaskPlanningApp.Model.Messages;

namespace TaskPlanningApp.ViewModel
{
    public class TaskEditViewModel : WorkspaceViewModel
    {
        private bool _editMode;
        private ToDoTask _taskToEdit;
        public ICommand AddNewTaskButton { get; set; }
        public ICommand CancelAddingTaskButton { get; set; }

        public delegate void OnCancelAddingTaskDelegate();
        public event OnCancelAddingTaskDelegate CancelAddingTaskEvent;

        private string _taskName;
        public string TaskName
        {
            get { return _taskName; }
            set
            {
                _taskName = value;
                OnPropertyChanged();
            }
        }

        private string _taskDescription;
        public string TaskDescription
        {
            get { return _taskDescription; }
            set
            {
                _taskDescription = value;
                OnPropertyChanged();
            }
        }

        private string _taskDetails;
        public string TaskDetails
        {
            get { return _taskDetails; }
            set
            {
                _taskDetails = value;
                OnPropertyChanged();
            }
        }

        private DateTime _selectedDeadlineDate;
        public DateTime SelectedDeadlineDate
        {
            get { return _selectedDeadlineDate; }
            set
            {
                _selectedDeadlineDate = value;
                OnPropertyChanged();
            }
        }

        private string _editOrCreateDescription;
        public string EditOrCreateDescription
        {
            get { return _editOrCreateDescription; }
            set
            {
                _editOrCreateDescription = value;
                OnPropertyChanged();
            }
        }

        private string _addOrSaveButtonDescription;
        public string AddOrSaveButtonDescription
        {
            get { return _addOrSaveButtonDescription; }
            set
            {
                _addOrSaveButtonDescription = value;
                OnPropertyChanged();
            }
        }
        public TaskEditViewModel(bool editMode, ToDoTask taskToEdit = null)
        {
            _editMode = editMode;
            _taskToEdit = taskToEdit;
            CancelAddingTaskButton = new RelayCommand<TaskEditViewModel>(CancelAddingTask, (vm) => true);
            AddNewTaskButton = new RelayCommand<TaskEditViewModel>(AddNewTask, (vm) => true);

            if (_taskToEdit == null)
            {
                SelectedDeadlineDate = DateTime.Now;
                TaskName = "Tytuł zadania";
                TaskDescription = "Opis zadania";
                TaskDetails = "Szczegóły zadania";
            }
            else
            {
                SelectedDeadlineDate = taskToEdit.Deadline;
                TaskName = taskToEdit.Title;
                TaskDescription = taskToEdit.ShortDescription;
                TaskDetails = taskToEdit.LongDescription;
            }
   
            if(!editMode)
            {
                AddOrSaveButtonDescription = "DODAJ";
                EditOrCreateDescription = "Dodawanie nowego zadania";
            }
            else
            {
                AddOrSaveButtonDescription = "ZAPISZ";
                EditOrCreateDescription = "Edycja zadania";
            }
        }

        private readonly Action<TaskEditViewModel> AddNewTask = (vm) =>
        {
            if (!vm._editMode)
            {
                if (vm.ValidateNewTask())
                {
                    ToDoTask taskToAdd = new ToDoTask();
                    taskToAdd.Title = vm.TaskName;
                    taskToAdd.ShortDescription = vm.TaskDescription;
                    taskToAdd.LongDescription = vm.TaskDetails;
                    taskToAdd.Deadline = vm.SelectedDeadlineDate;

                    Messenger.Default.Send<AddNewTaskMessage>(new AddNewTaskMessage(taskToAdd));

                    vm?.CancelAddingTaskEvent?.Invoke();
                }
            }
            else
            {
                if(vm.ValidateNewTask())
                {
                    vm._taskToEdit.Deadline = vm.SelectedDeadlineDate;
                    vm._taskToEdit.Title = vm.TaskName;
                    vm._taskToEdit.ShortDescription = vm.TaskDescription;
                    vm._taskToEdit.LongDescription = vm.TaskDetails;

                    Messenger.Default.Send<UpdateTaskMessage>(new UpdateTaskMessage(vm._taskToEdit));

                    vm?.CancelAddingTaskEvent?.Invoke();
                }
            }
        };

        private readonly Action<TaskEditViewModel> CancelAddingTask = (vm) =>
        {
            vm?.CancelAddingTaskEvent?.Invoke();
        };

        private bool ValidateNewTask()
        {
            if(TaskName == null || TaskName.Length == 0)
            {
                MessageBox.Show("Pole \"Tytuł zadania\" nie może pozostać puste.");
                return false;
            }
            if (TaskDescription == null || TaskDescription.Length == 0)
            {
                MessageBox.Show("Pole \"Opis zadania\" nie może pozostać puste.");
                return false;
            }
            if (TaskDetails == null || TaskDetails.Length == 0)
            {
                MessageBox.Show("Pole \"Szczegóły zadania\" nie może pozostać puste.");
                return false;
            }

            return true;
        }
    }
}
