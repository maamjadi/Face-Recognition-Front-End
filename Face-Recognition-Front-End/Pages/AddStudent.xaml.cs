using System;
using System.Net;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using Plugin.Media;
using FaceRecognitionFrontEnd.utilities;
using Plugin.Media.Abstractions;
using System.Collections.Generic;

namespace FaceRecognitionFrontEnd
{
    public partial class AddStudent : ContentPage
    {
        public const string path = "/students";
        public string userName { get; set; }

        public AddStudent()
        {
            InitializeComponent();
            BindingContext = this;

        }
        async void AddNewStudent(object sender, EventArgs e)
        {
            
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                DisplayErrorAlert(":( No camera available.");
                return;
            }
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                // Supply media options for saving our photo after it's taken.
                var mediaFile = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg"
                });
                if (mediaFile == null)
                    return;

                //upload it to the azure

                string uri = await BlobMan.Instance.UploadFileAsync(mediaFile.Path, mediaFile.GetStream());


                Student student = new Student();
                student.UserName = userName;
                student.PhotoURL = uri;
                try
                {
                    var response = await RestClient.Post(path + "/", student);
                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        DisplayErrorAlert("User Name already exist!");
                    }
                    else if (response.StatusCode == HttpStatusCode.Created)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        var responseId = JObject.Parse(data);
                        var studentId = responseId["studentId"].ToString();
                        AddSubject.studentsID.Add(studentId);
                        AddSubject.students.Add(student);
                        //TODO pop twice to the add subject don't create a new instance

                        App.Current.MainPage = new NavigationPage(new AddSubject());

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

           

        }
        private void DisplayErrorAlert(string message)
        {
            DisplayAlert("Alert", message, "OK");
        }
    }
}
