using System;
using System.IO;
using MVMMLogin.Models;
using MVMMLogin.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVMMLogin
{
    public partial class App : Application
    {

        static Database database;
        static User currentUser;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Contacts.db3"));
                }
                return database;
            }
        }

        public static User CurrentUser
        {
            get
            {
                return currentUser;
            }
            set
            {
                currentUser = value;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MyPage())
            {
                BarBackgroundColor = Color.White,
                BarTextColor = Color.Black
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
