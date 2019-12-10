using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskPlanningApp.ViewModel;

namespace TaskPlanningApp.View
{
    /// <summary>
    /// Logika interakcji dla klasy TaskEditView.xaml
    /// </summary>
    public partial class TaskEditView : UserControl
    {
        public TaskEditView()
        {
            InitializeComponent();
        }

        private void TaskName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TaskEditViewModel taskEditVM = (TaskEditViewModel)this.DataContext;
            taskEditVM.TaskName = string.Empty;
        }

        private void TaskDescription_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TaskEditViewModel taskEditVM = (TaskEditViewModel)this.DataContext;
            taskEditVM.TaskDescription = string.Empty;
        }

        private void TaskDetails_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TaskEditViewModel taskEditVM = (TaskEditViewModel)this.DataContext;
            taskEditVM.TaskDetails = string.Empty;
        }
    }
}
