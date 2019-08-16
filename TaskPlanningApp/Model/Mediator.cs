using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TaskPlanningApp.Model
{
    public enum WindowOperation
    {
        Hide,
        Show,
        Close
    }
    public abstract class AbstractWindowInformator
    {
        public abstract void Send(string from, string to, WindowOperation stateToSet);
        public abstract WindowInformator AddWindow(string name, IAbstractWindow window);
    }

    public class WindowInformator : AbstractWindowInformator
    {
        private Dictionary<string, IAbstractWindow> _windows = new Dictionary<string, IAbstractWindow>();

        public override WindowInformator AddWindow(string name, IAbstractWindow window)
        {
            if (!_windows.ContainsValue(window))
                _windows.Add(name, window);

            return this;
        }

        public override void Send(string from, string to, WindowOperation stateToBeSet)
        {
            IAbstractWindow windowToBeChanged = (IAbstractWindow)_windows[to];

            if (windowToBeChanged != null)
            {
                windowToBeChanged.ReceiveStateChangeRequest(from, stateToBeSet);
            }
        }
    }
}
