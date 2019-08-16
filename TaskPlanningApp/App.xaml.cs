using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TaskPlanningApp.Model;
using TaskPlanningApp.View;

namespace TaskPlanningApp
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        WindowInformator mainInformator = new WindowInformator();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow mainWindow = new MainWindow();
            LoginWindow loginWindow = new LoginWindow();

            mainInformator.AddWindow("MainWindow", mainWindow).AddWindow("LoginWindow", loginWindow);
            mainWindow.MainWindowInformator = loginWindow.LoginWindowInformator = mainInformator;

            mainWindow.Show();

        }
    }
}
