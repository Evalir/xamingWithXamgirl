using System;

using Xamarin.Forms;

namespace Extra1
{
    public class ExercisePage : ContentPage
    {
        public ExercisePage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

