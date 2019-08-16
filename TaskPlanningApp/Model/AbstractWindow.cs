using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TaskPlanningApp.Model
{
    public interface IAbstractWindow
    {
        void SendStateChangeRequest(string from, string to, WindowOperation stateToBeSet);
        void ReceiveStateChangeRequest(string from, WindowOperation stateToBeSet);   
    }
}
