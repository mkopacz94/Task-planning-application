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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskPlanningApp.Model;
using TaskPlanningApp.View;
using TaskPlanningApp.ViewModel;

namespace TaskPlanningApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window, IClosable, IAbstractWindow
    {
        private MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
        public WindowInformator MainWindowInformator { get;  set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;

            mainWindowViewModel.MyEvent += () =>
                                                 {
                                                     this.Hide();
                                                     SendStateChangeRequest("MainWindow","LoginWindow", WindowOperation.Show);
                                                 };

            mainWindowViewModel.OnClosingEvent += () =>
                                                        {
                                                            SendStateChangeRequest("MainWindow", "LoginWindow", WindowOperation.Close);
                                                            this.Close();
                                                        };
            
        }

        public void ReceiveStateChangeRequest(string from, WindowOperation stateToBeSet)
        {
            switch (stateToBeSet)
            {
                case WindowOperation.Hide:
                    this.Hide();
                    break;

                case WindowOperation.Show:
                    this.Show();
                    this.DataContext = mainWindowViewModel;
                    break;
                case WindowOperation.Close:
                    this.Close();
                    break;
                default:
                    break;
            }

        }

        public void SendStateChangeRequest(string from, string to, WindowOperation stateToBeSet)
        {
            MainWindowInformator.Send(from, to, stateToBeSet);
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)  //Test drag window mode - need for change to ViewModel
        {
            if(e.LeftButton == MouseButtonState.Pressed) DragMove();

        }

    }
}
