using System;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;

namespace FaceRecognitionFrontEnd
{
    public partial class SignUpPage : ContentPage
    {
        public const string path = "/teacher";
        public string email { get; set; }
        public string password { get; set; }
        public string verifyPassword { get; set; }

        public SignUpPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
        async void CreateAccount(object sender, EventArgs e)
        {
            if (!CheckEntries())
            {
                return;
            }
            Teacher newTeacher = new Teacher();
            newTeacher.Email = email;
            newTeacher.Password = password;
            try
            {
                var response = await RestClient.Post(path + "/", newTeacher);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    DisplayErrorAlert("Email already exist!");
                }
                else if (response.StatusCode == HttpStatusCode.Created)
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
        void check_password(object sender, TextChangedEventArgs e)
        {
            var oldText = e.OldTextValue;
            var newText = e.NewTextValue;
        }
        private bool CheckEntries()
        {
            if (!ValidationService.CheckEntry(EmailEntry, email) ||
               !ValidationService.CheckEntry(PasswordEntry, password) ||
               !ValidationService.CheckEntry(VerifyPasswordEntry, verifyPassword) ||
               !ValidationService.VerfiyPassword(password, verifyPassword, VerifyPasswordEntry) ||
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
