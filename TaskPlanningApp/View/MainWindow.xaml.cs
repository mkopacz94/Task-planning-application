using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    
    public partial class MainWindow : Window
    {
        //private MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
        public WindowInformator MainWindowInformator { get;  set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        public void SendStateChangeRequest(string from, string to, WindowOperation stateToBeSet)
        {
            MainWindowInformator.Send(from, to, stateToBeSet);
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)  //Test drag window mode - need for change to ViewModel
        {
            if(e.LeftButton == MouseButtonState.Pressed) DragMove();

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindowViewModel vm = (MainWindowViewModel)this.DataContext;

            vm.CurrentlySelectedTask = (ToDoTask)(sender as ListView).SelectedItem;
            vm.SelectedTaskIndex = (sender as ListView).SelectedIndex;
        }

        private void Image_Drop(object sender, DragEventArgs e)
        {
            MainWindowViewModel vm = (MainWindowViewModel)this.DataContext;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                ImageSource source = new BitmapImage(
                    new Uri(files[0]));
                vm.ImageNo1 = source;
            }
        }

        private void Image2_Drop(object sender, DragEventArgs e)
        {
            MainWindowViewModel vm = (MainWindowViewModel)this.DataContext;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                ImageSource source = new BitmapImage(
                    new Uri(files[0]));
                vm.ImageNo2 = source;
            }
        }

        private void Image3_Drop(object sender, DragEventArgs e)
        {
            MainWindowViewModel vm = (MainWindowViewModel)this.DataContext;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                ImageSource source = new BitmapImage(
                    new Uri(files[0]));
                vm.ImageNo3 = source;
            }
        }

        private void ListView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel vm = (MainWindowViewModel)this.DataContext;

            ToggleButton taskFinishedControl = sender as ToggleButton;

            if(taskFinishedControl != null)
            {
                ToDoTask clickedTask = taskFinishedControl.DataContext as ToDoTask;

                if(clickedTask != null)
                {
                    vm.SetFinishedTask(clickedTask, true);
                }
                
            }
            
            vm.CalculateFinishedTasks();
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel vm = (MainWindowViewModel)this.DataContext;

            ToggleButton taskFinishedControl = sender as ToggleButton;

            if (taskFinishedControl != null)
            {
                ToDoTask clickedTask = taskFinishedControl.DataContext as ToDoTask;

                if (clickedTask != null)
                {
                    vm.SetFinishedTask(clickedTask, false);
                }

            }
            vm.CalculateFinishedTasks();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView list)
            {
                if(list != null && list.Items.Count > 0 && list.SelectedItems.Count > 0 && list.SelectedItems[0] is ToDoTask selectedTask)
                {
                    MainWindowViewModel vm = (MainWindowViewModel)this.DataContext;
                    vm.EditSelectedTask(selectedTask);
                }
            }
        }
    }
}
