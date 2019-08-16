using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskPlanningApp.Model;
using TaskPlanningApp.ViewModel;

namespace TaskPlanningApp.View
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window, IClosable, IAbstractWindow
    {
        public WindowInformator LoginWindowInformator { get; set; }
        private LoginViewModel loginVM = new LoginViewModel();
        public string WindowName { get; set; }
        public LoginWindow()
        {
            InitializeComponent();

            loginVM.ShowMainWindowEvent += () =>
                                                {
                                                    this.Hide();
                                                    SendStateChangeRequest("LoginWindow", "MainWindow", WindowOperation.Show);
                                                };
        }

        public void SendStateChangeRequest(string from, string to, WindowOperation stateToBeSet)
        {
            LoginWindowInformator.Send(from, to, stateToBeSet);
        }

        public void ReceiveStateChangeRequest(string from, WindowOperation stateToBeSet)
        {
            switch(stateToBeSet)
            {
                case WindowOperation.Hide:
                    this.Hide();
                    break;

                case WindowOperation.Show:
                    this.DataContext = loginVM;
                    this.Show();  
                    break;
                case WindowOperation.Close:
                    this.Close();
                    break;
                default:
                    break;
            }

        }
    }
}
