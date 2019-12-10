using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanningApp.Model.Messages
{
    public class LoggedUserMessage
    {
        public User LoggedUser { get; set; }

        public LoggedUserMessage(User loggedUser)
        {
            LoggedUser = loggedUser;
        }
    }
}
