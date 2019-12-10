using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPlanningApp.Model;

namespace TaskPlanningApp.Database
{
    public class UsersMapping : EntityTypeConfiguration<User>
    {
        public UsersMapping()
        {
            ToTable("Users");
        }
    }
}
