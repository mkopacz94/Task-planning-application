using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskPlanningApp.Database;
using TaskPlanningApp.Model;

namespace TaskPlanningApp.ViewModel
{
    
    public class RegisterWindowViewModel : WorkspaceViewModel
    {
        //private DataContext database;

        public string Username { get; set; }
        public string FirstPassword { get; set; }
        public string SecondPassword { get; set; }
        public ICommand RegisterButton { get; set; }
        public ICommand CancelButton { get; set; }

        public delegate void OnRegisterWindowClose();
        public event OnRegisterWindowClose RegisterWindowClosing;
        public RegisterWindowViewModel()
        {
            RegisterButton = new RelayCommand<object[]>(RegisterNewUser, vm => true);
            CancelButton = new RelayCommand<RegisterWindowViewModel>(CloseRegisterWindow, vm => true);
        }
        //public RegisterWindowViewModel(DataContext database) : this()
        //{
        //    this.database = database;
        //}

        private  Action<object[]> RegisterNewUser = parameters =>
        {
            MessageBoxResult wrongFormat;

            PasswordBox pwdBox1 = parameters[0] as PasswordBox;
            PasswordBox pwdBox2 = parameters[1] as PasswordBox;
            RegisterWindowViewModel vm = parameters[2] as RegisterWindowViewModel;

            if (vm.CheckInput(vm.Username, pwdBox1.Password, pwdBox2.Password))
                _ = vm.TryToRegisterUser(new User(vm.Username, pwdBox1.Password));
            else
                wrongFormat = MessageBox.Show("Podane hasła nie mogą być puste i różnić się od siebie.");
        };

        private async Task TryToRegisterUser(User newUser)
        {
            MessageBoxResult message;
            UserValidator userValidator = new UserValidator();

            if(await userValidator.IsUserInDatabaseAsync(newUser))
                message = MessageBox.Show("Podana nazwa użytkownika już istnieje.");
            else
            {
                using (DataContext database = new DataContext())
                {
                    database.Users.Add(new User(newUser.Username, newUser.Password));
                    await database.SaveChangesAsync();
                }

                message = MessageBox.Show("Pomyślnie zarejestrowano.");
                Window.Close();
                RegisterWindowClosing.Invoke();
            }
        }
        
        private bool CheckInput(string username, string firstPassword, string secondPassword)
        {
            return (username != null && username != string.Empty
                && firstPassword != null && firstPassword != string.Empty && firstPassword == secondPassword);
        }
        private Action<RegisterWindowViewModel> CloseRegisterWindow = vm =>
        {
            vm.Window.Close();
            vm.RegisterWindowClosing.Invoke();
        };
    }
}
