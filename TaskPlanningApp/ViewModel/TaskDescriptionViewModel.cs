using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanningApp.ViewModel
{
    public class TaskDescriptionViewModel : WorkspaceViewModel
    {
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
    }
}
