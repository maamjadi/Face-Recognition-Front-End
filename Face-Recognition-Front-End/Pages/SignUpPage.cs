using System;

using Xamarin.Forms;

namespace FaceRecognitionFrontEnd.Pages
{
    public class SignUpPage : ContentPage
    {
        public SignUpPage()
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

