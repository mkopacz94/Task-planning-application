using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanningApp.Model
{
    public class User
    {
        private string _username, _password;
        private List<Task> _tasks;

        public string Name { get; }
        public string Surname { get; }

        public User(string name, string surname)
        {
            _tasks = new List<Task>();
            Name = name;
            Surname = surname;
        }

        public void AddTask(Task taskToAdd)
        {
            if (taskToAdd != null) _tasks.Add(taskToAdd);
            else throw new ArgumentNullException(nameof(taskToAdd), "Task which is trying to be add cannot be null.");
        }

        public void DeleteTask(string taskName)
        {
            if(_tasks.Exists(x => x.Name == taskName))  _tasks.Remove(new Task(taskName));
        }

    }
}
