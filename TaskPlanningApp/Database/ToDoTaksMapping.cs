using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPlanningApp.Model;

namespace TaskPlanningApp.Database
{
    class ToDoTaksMapping : EntityTypeConfiguration<ToDoTask>
    {
        public ToDoTaksMapping()
        {
            ToTable("ToDoTasks");
        }
    }
}
