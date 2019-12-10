using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanningApp.Model.Messages
{
    public class AddNewTaskMessage
    {
        public ToDoTask TaskToAdd { get; set; }

        public AddNewTaskMessage(ToDoTask taskToAdd)
        {
            TaskToAdd = new ToDoTask();
            TaskToAdd.Title = taskToAdd.Title;
            TaskToAdd.ShortDescription = taskToAdd.ShortDescription;
            TaskToAdd.LongDescription = taskToAdd.LongDescription;
            TaskToAdd.Deadline = taskToAdd.Deadline;
        }
    }
}
