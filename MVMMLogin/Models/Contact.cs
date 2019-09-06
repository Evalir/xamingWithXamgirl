using System;
using SQLite;

namespace MVMMLogin.Models
{
    public class Contact
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        // It's a sin, I know
        public string ParentUserEmail { get; set; }

        public override string ToString()
        {
            return $"{Name} {LastName}";
        }

        public Contact(string Name, string LastName, string Phone, string Email)
        {
            this.Name = Name;
            this.LastName = LastName;
            this.Phone = Phone;
            this.Email = Email;
        }

        public Contact()
        {
        }
    }
}
