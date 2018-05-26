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
        /// <summary>
        /// Gos to signup.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        async void GoToSignup(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }
        /// <summary>
        /// Gos to login.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        async void GoToLogin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
