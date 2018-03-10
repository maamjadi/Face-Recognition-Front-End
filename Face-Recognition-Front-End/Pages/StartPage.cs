using System;

using Xamarin.Forms;

namespace FaceRecognitionFrontEnd.Pages
{
    public class StartPage : ContentPage
    {
        public StartPage()
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

