using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using MVMMLogin.Models;
using Xamarin.Forms;

namespace MVMMLogin.Views
{
    class MyPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand LogInCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public string LoginErrors { get; set; }
        public User User { get; set; }

        public MyPageViewModel()
        {
            User = new User();
            LogInCommand = new Command(async () =>
            {

                if (String.IsNullOrEmpty(User.Username))
                {
                    LoginErrors = "Username cannot be empty!";
                }
                else if (String.IsNullOrEmpty(User.Password))
                {
                    LoginErrors = "Password cannot be empty!";
                }
                else
                {
                    bool signUpSuccessful = await App.Database.LoginUserAsync(User.Username, User.Password);
                    if (signUpSuccessful)
                    {
                        var foundUser = await App.Database.GetUserAsync(User.Username, User.Password);
                        App.CurrentUser = foundUser;
                        Debug.WriteLine($"Set user to {App.CurrentUser.Email}");
                        App.Current.MainPage = new NavigationPage(new ContactsPage())
                        {
                            BarBackgroundColor = Color.FromHex("#C6C6C6"),
                            BarTextColor = Color.Black
                            
                        };
                        await App.Current.MainPage.Navigation.PopToRootAsync(true);
                    }
                    else
                    {
                        LoginErrors = "Check your credentials!";
                    }
                }
            });

            RegisterCommand = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new MySignUpPage());
            });
        }


    }
}
