using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPlanningApp.CustomControls;
using System.Windows.Controls;

namespace TaskPlanningApp.Model
{
    public class TaskPanelManager
    {
        private List<Task> _allTasks;

        public Action<StackPanel> AddNewTask;
        public Predicate<StackPanel> CanAddNewTask = panelToAdd => true;

        public Action<StackPanel> DeleteLastTask;
        public Predicate<StackPanel> CanDeleteTask = panelToDelete => true;

        public TaskPanelManager()
        {
            _allTasks = new List<Task>();
            AddNewTask = AddNewTaskExecuter;
            DeleteLastTask = DeleteLastTaskExecuter;
        }

       
        private void AddNewTaskExecuter(StackPanel panelToAdd)
        {
            TaskListControl newTask = new TaskListControl();
            panelToAdd.Children.Add(newTask);
            _allTasks.Add(new Task("Zadanie1"));
        }

        private void DeleteLastTaskExecuter(StackPanel panelToAdd)
        {
            if(panelToAdd.Children.Count>0)
                panelToAdd.Children.RemoveAt(panelToAdd.Children.Count - 1);
            if (_allTasks.Count > 0)
                _allTasks?.RemoveAt(_allTasks.Count - 1);
        }

    }
}
