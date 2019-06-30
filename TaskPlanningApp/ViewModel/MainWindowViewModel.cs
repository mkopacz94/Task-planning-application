using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace TaskPlanningApp.ViewModel
{
    public class MainWindowViewModel
    {
        public RelayCommand<IClosable> CloseCommand {get; set;}

        public MainWindowViewModel()
        {
            this.CloseCommand = new RelayCommand<IClosable>(this.CloseWindow);
        }

        private void CloseWindow(IClosable window)
        {
            if (window != null)
                window.Close();
        }
    }

}
