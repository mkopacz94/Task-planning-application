using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TaskPlanningApp.ViewModel;

namespace TaskPlanningApp.Model
{
    public class MainWindowStateManager
    {
        public void CleanUpUser(ViewModelBase vm)
        {
            if (!(vm is MainWindowViewModel))
                return;

            MainWindowViewModel mainWindowVM = (MainWindowViewModel)vm;

            CleanTextFields(mainWindowVM).ClearTasksList(mainWindowVM).ClearTaskDescriptionField(mainWindowVM);

        }

        private MainWindowStateManager CleanTextFields(MainWindowViewModel vm)
        {
            vm.CurrentUserName = vm.CurrentTaskTitle = string.Empty;
            return this;
        }

        private MainWindowStateManager ClearTasksList(MainWindowViewModel vm)
        {
            vm.TasksList.Clear();
            vm.TasksCollectionView = new CollectionView(new ObservableCollection<ToDoTask>());
            return this;
        }

        private MainWindowStateManager ClearTaskDescriptionField(MainWindowViewModel vm)
        {
            vm.DescribeSection = new TaskDescriptionViewModel();
            return this;
        }
    }
}
