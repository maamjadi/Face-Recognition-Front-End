using System;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using FaceRecognitionFrontEnd.utilities;
using Plugin.Media.Abstractions;
using Plugin.Media;

namespace FaceRecognitionFrontEnd
{
    public partial class AddSubject : ContentPage
    {
        public const string path = "/subjects";
        public string subjectName { get; set; }
        public string numberOfSessions { get; set; }
        public List<Student> students;
            
            
        public AddSubject()
        {
            InitializeComponent();
            BindingContext = this;
            students = new List<Student>();
            //TODO get the students from the other page and replace it with this temp students
            students.Add(new Student { UserName = "Ali", PhotoURL = "https://alialsaeedi19.blob.core.windows.net/recfa/IMG_7802.jpg"});


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
           
            try
            {
                var response = await RestClient.Post(path + "/" + App.teacherId, newSubject);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    DisplayErrorAlert("Email already exist!");
                }
                else if (response.StatusCode == HttpStatusCode.Created)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var responseJson = JObject.Parse(data);
                    var subjectId = responseJson["subjectId"].ToString();
                    //For each subject we are creating a list of students 
                    await RecMan.RegisterStudents(students, subjectId);
                    App.Current.MainPage = new NavigationPage(new MainPage());
                }
                else
                {
                    DisplayErrorAlert("Something went wrong!");
                }
                System.Diagnostics.Debug.WriteLine(response);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                DisplayErrorAlert("Something went wrong!");
            }



            //TODO move this to when the user press on his profile in student page
            //System.Diagnostics.Debug.WriteLine("hi ali");

            //var mediaFile = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            //{
            //    Directory = "Sample",
            //    Name = "test.jpg"
            //});
            //if (mediaFile == null)
            //    return;
            //var userName = await RecMan.ExecuteFindSimilarFaceCommandAsync("alialsaeedi191", mediaFile);
           

        }
        async void GoToStudentPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StudentPage());


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
