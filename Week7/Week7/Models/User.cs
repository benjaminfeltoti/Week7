using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Week7.Models
{
    public class User
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public User()
        {

        }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public bool checkInformation()
        {
            if (Username == null || Password == null)
            {
                return false;
            }
            else if(Username.Equals("") || Password.Equals(""))
            {
                return false;
            }

            return true;
        }
    }
}
