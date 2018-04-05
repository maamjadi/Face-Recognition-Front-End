using System;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;

namespace FaceRecognitionFrontEnd
{
    public partial class AddSubject : ContentPage
    {
        public const string path = "/subjects";
        public string subjectName { get; set; }
        public string numberOfSessions { get; set; }
        public string[] students;

        public AddSubject()
        {
            InitializeComponent();
            BindingContext = this;
        }
        async void AddNewSubject(object sender, EventArgs e)
        {
            if (!CheckEntries())
            {
                return;
            }
            Subject newSubject = new Subject();
            newSubject.Name = subjectName;
            newSubject.NumberOfSession = int.Parse(numberOfSessions);
            //TODO get the students from the other page
            try
            {
                var response = await RestClient.Post(path + "/" + App.teacherId, newSubject);
                //if (response.StatusCode == HttpStatusCode.BadRequest)
                //{
                //    DisplayErrorAlert("Email already exist!");
                //}
                //else if (response.StatusCode == HttpStatusCode.Created)
                //{
                //    var data = await response.Content.ReadAsStringAsync();
                //    var teacherId = JObject.Parse(data);
                //    App.teacherId = teacherId["teacherId"].ToString();
                //    App.Current.MainPage = new NavigationPage(new MainPage());
                //}
                //else
                //{
                //    DisplayErrorAlert("Something went wrong!");
                //}
                System.Diagnostics.Debug.WriteLine(response);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                DisplayErrorAlert("Something went wrong!");
            }

        }
        private void DisplayErrorAlert(string message)
        {
            DisplayAlert("Alert", message, "OK");
        }
        private bool CheckEntries()
        {
            if (!ValidationService.CheckEntry(SubjectNameEntry, subjectName) ||
                !ValidationService.CheckEntry(numberOfSessionsEntry, numberOfSessions) ||
                !ValidationService.IsNumric(numberOfSessionsEntry, numberOfSessions))
            {
                return false;
            }
            return true;
        }
    }
}
