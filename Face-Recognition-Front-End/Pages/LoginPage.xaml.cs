using System;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;

namespace FaceRecognitionFrontEnd
{
    public partial class LoginPage : ContentPage
    {
        public const string path = "/teacher";
        public string email { get; set; }
        public string password { get; set; }

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
        async void Login(object sender, EventArgs e)
        {
            if (!CheckEntries())
            {
                return;
            }
            Teacher teacher = new Teacher();
            teacher.Email = email;
            teacher.Password = password;
            try
            {
                var response = await RestClient.Post(path + "/login", teacher);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    DisplayErrorAlert("Wrong email or password");
                }
                else if (response.StatusCode == HttpStatusCode.OK)
                {
                    await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    DisplayErrorAlert("Something went wrong!");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                DisplayErrorAlert("Something went wrong!");
            }

        }
        private bool CheckEntries()
        {
            if (!ValidationService.CheckEntry(EmailEntry, email) ||
               !ValidationService.CheckEntry(PasswordEntry, password) ||
               !ValidationService.CheckEmailSyntax(email, EmailEntry))
            {
                return false;
            }
            return true;

        }
        private void DisplayErrorAlert(string message)
        {
            DisplayAlert("Alert", message, "OK");
        }

    }
}
