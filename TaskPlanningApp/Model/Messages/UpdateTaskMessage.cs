using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanningApp.Model.Messages
{
    public class UpdateTaskMessage
    {
        public ToDoTask TaskToUpdate { get; set; }

        public UpdateTaskMessage(ToDoTask taskToUpdate)
        {
            TaskToUpdate = taskToUpdate;
        }
    }
}
