using GalaSoft.MvvmLight.Messaging;
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
using TaskPlanningApp.Model.Messages;

namespace TaskPlanningApp.ViewModel
{
    public class LoginViewModel : WorkspaceViewModel
    {
        //private DataContext database;
        private UserValidator userValidator;
        private Window registerWindow;
        private bool _isTryingToLogin = false;

        public ICommand CancelButton { get; set; }
        public ICommand RegisterButton { get; set; }
        public ICommand LoginButton { get; set; }

        public delegate void OnLoginWindowClose();
        public event OnLoginWindowClose LoginWindowClosing;

        private bool emptyFieldWarningVisibility = false;
        public bool EmptyFieldWarningVisibility
        {
            get { return emptyFieldWarningVisibility; }
            set
            {
                emptyFieldWarningVisibility = value;
                OnPropertyChanged();
            }
        } 

        public string UsernameField { get; set; }
        public string PasswordField { get; set; }

        public LoginViewModel()
        {
            this.CancelButton = new RelayCommand<LoginViewModel>(CloseWindow, (vm) => true);
            this.RegisterButton = new RelayCommand<LoginViewModel>(RegisterUser, (vm) => !_isTryingToLogin);
            this.LoginButton = new RelayCommand<object[]>(executeLogin, (vm) => true);

            userValidator = new UserValidator();
        }

        //public LoginViewModel(DataContext db) : this()
        //{
        //    database = db;
        //    userValidator = new UserValidator(database);
        //}

        private Action<object[]> executeLogin = (parameters) =>
        {
            PasswordBox pwdBox = parameters[0] as PasswordBox;
            LoginViewModel vm = parameters[1] as LoginViewModel;

            vm.EmptyFieldWarningVisibility = false;

            if (!string.IsNullOrEmpty(vm.UsernameField))
            {
                _ = vm.CheckLogin(new User(vm.UsernameField, pwdBox.Password));
            }
            else vm.EmptyFieldWarningVisibility = true;
        };

        private Action<LoginViewModel> CloseWindow = vm =>
        {
            if (vm._isTryingToLogin)
                vm.userValidator.CancelLoginValidation();

            vm.Window.Close();
            vm.LoginWindowClosing.Invoke();
        };

        private Action<LoginViewModel> RegisterUser = vm =>
        {
            LoginViewModel loginVM = new LoginViewModel();
            RegisterWindowViewModel registerVM = new RegisterWindowViewModel();
            registerVM.RegisterWindowClosing += () => vm.Window.Show();
            vm.registerWindow = WindowBuilder.BuildWindow(registerVM);
            vm.Window.Hide();
            vm.registerWindow.ShowDialog();

        };

        private async Task CheckLogin(User user)
        {
            _isTryingToLogin = true;

            try
            {
                if (await userValidator.IsUserInDatabaseAsync(user))
                {
                    if (await userValidator.TryToMatchWithPasswordAsync(user.Username, user.Password))
                    {
                        _isTryingToLogin = false;
                        Messenger.Default.Send<LoggedUserMessage>(new LoggedUserMessage(user));
                        Window.Close();
                        LoginWindowClosing.Invoke();
                    }
                    else
                    {
                        _isTryingToLogin = false;
                        MessageBoxResult message = MessageBox.Show("Błędna nazwa użytkownika lub hasło.");
                    }
                }
                else
                {
                    _isTryingToLogin = false;
                    MessageBoxResult message = MessageBox.Show("Błędna nazwa użytkownika lub hasło.");
                }
                }
            catch(OperationCanceledException ex)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Logowanie zostało anulowane");
            }
        }
    }
}
