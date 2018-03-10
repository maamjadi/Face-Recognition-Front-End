using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FaceRecognitionFrontEnd
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }
        async void GoToSignup(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }
        async void GoToLogin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
