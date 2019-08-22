using System;
using System.ComponentModel;

namespace FodyLogin.ViewModels
{

    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Email { get; set; }
        public string Password { get; set; }
        public string Notification
        {
            get
            {
                if (String.IsNullOrEmpty(Email))
                    return "Email cannot be empty";
                else if (String.IsNullOrEmpty(Password))
                    return "Password cannot be empty.";
                else return "";
            }
        }

        public bool ButtonEnabled
        {
            get
            {
                if (!String.IsNullOrEmpty(Email) && !String.IsNullOrEmpty(Password))
                    return true;
                return false;
            }
        }
    }
}
