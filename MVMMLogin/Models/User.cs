using System;
using SQLite;

namespace MVMMLogin.Models
{
    public class User
    {
        [PrimaryKey, Unique]
        public string Username { get; set; }
        [Unique]
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
