using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskPlanningApp.Database;

namespace TaskPlanningApp.Model
{
    public class UserValidator
    {
        //private DataContext database;
        private CancellationTokenSource _cancelToken = new CancellationTokenSource();

        //public UserValidator(DataContext database)
        //{
        //    this.database = database;
        //}

        public async Task<bool> IsUserInDatabaseAsync(User userToValidate)
        {
            bool containsUser = false;

            return await Task.Run(() =>
            {
                _cancelToken.Token.ThrowIfCancellationRequested();

                using (DataContext database = new DataContext())
                {
                    containsUser = database.Users.Any(u => u.Username == userToValidate.Username);
                }
                return containsUser;
            }, _cancelToken.Token);          
        }

        public async Task<bool> TryToMatchWithPasswordAsync(string username, string password)
        {
            return await Task.Run(() =>
            {
                _cancelToken.Token.ThrowIfCancellationRequested();

                using (DataContext database = new DataContext())
                {
                    var users = from u in database.Users where u.Username == username select u;


                    foreach (User u in users)
                    {
                        if (u.Password == password)
                            return true;
                    }
                }
                return false;
            }, _cancelToken.Token);    
        }

        public void CancelLoginValidation()
        {
            _cancelToken.Cancel();
        }
    }
}
