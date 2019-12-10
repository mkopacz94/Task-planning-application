using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanningApp.Model
{
    public class User : IComparable<User>
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User()
        {
            Username = Password = "Not defined";
        }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public int CompareTo(User other)
        {
            return this.Username.CompareTo(other.Username);
        }
    }
}
