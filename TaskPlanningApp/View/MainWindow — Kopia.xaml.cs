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
using TaskPlanningApp.ViewModel;

namespace TaskPlanningApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window, IClosable
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            SetUserData();   //Test data - need for change to ViewModel
         
        }

        private void SetUserData()
        {
            NameTextField.Text = "Mateusz";
            SurnameTextField.Text = "Kopacz";
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)  //Test drag window mode - need for change to ViewModel
        {
            if(e.LeftButton == MouseButtonState.Pressed) DragMove();

        }
    }
}
