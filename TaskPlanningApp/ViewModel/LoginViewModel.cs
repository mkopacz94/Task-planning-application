using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPlanningApp.Model;

namespace TaskPlanningApp.ViewModel
{
    public class LoginViewModel
    {
        public RelayCommand<LoginViewModel> CancelButton { get; set; }
        public delegate void ShowMainWindow();
        public event ShowMainWindow ShowMainWindowEvent;
        public LoginViewModel()
        {
            this.CancelButton = new RelayCommand<LoginViewModel>(ShowBackMainWindow, CanShowBackMainWindow);
        }

        private Action<LoginViewModel> ShowBackMainWindow = vm =>
        {
            vm.ShowMainWindowEvent?.Invoke();
        };

        private Predicate<LoginViewModel> CanShowBackMainWindow = vm => true;
    }
}
