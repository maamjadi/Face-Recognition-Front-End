using System;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using FaceRecognitionFrontEnd.utilities;
using Plugin.Media.Abstractions;
using Plugin.Media;
using System.Threading.Tasks;

namespace FaceRecognitionFrontEnd
{
    public partial class AddSubject : ContentPage
    {
        public const string path = "/subjects";
        public string subjectName { get; set; }
        public string numberOfSessions { get; set; }
        public static List<Student> studentsRecgnitionArray = new List<Student>();
        public static List<Student> students = new List<Student>();

        public static List<string> studentsID = new List<string>();

            
        public AddSubject()
        {
            InitializeComponent();
            BindingContext = this;
            //TODO get the students from the other page and replace it with this temp students
            

        }
        async void AddNewSubject(object sender, EventArgs e)
        {
            if (!CheckEntries())
            {
                return;
            }
            Subject newSubject = new Subject();
            newSubject.Name = subjectName;
            newSubject.NumberOfSessions= int.Parse(numberOfSessions);
            newSubject.Students = studentsID.ToArray();
            try
            {
                var response = await RestClient.Post(path + "/" + App.teacherId, newSubject);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    DisplayErrorAlert("name already exist!");
                }
                else if (response.StatusCode == HttpStatusCode.Created)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var responseJson = JObject.Parse(data);
                    var subjectId = responseJson["subjectId"].ToString();

                    //For each subject we are creating a list of students 
                    await RecMan.RegisterStudents(students, Constants.GroupId);
                    studentsID.Clear();
                    students.Clear();
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

        }
        //async private  Task GetStudetnsFromDB(string subjectID){
        //    try
        //    {

        //        var response = await RestClient.Get(path + "/getStudents/" + subjectID);
        //        if (response.StatusCode == HttpStatusCode.BadRequest)
        //        {
        //            //System.Diagnostics.Debug.WriteLine("bad reponse from the backend");
        //             DisplayErrorAlert("Something went wrong");
        //            //TODO display an error to the user
        //        }
        //        else if (response.StatusCode == HttpStatusCode.OK)
        //        {
        //            var data = await response.Content.ReadAsStringAsync();
        //            var results = JObject.Parse(data);
                  
        //            students = JObject.Parse(data)["students"].ToObject<List<Student>>();
        //            await RecMan.AddStudents(students, "alialsaeediNew");
        //            studentsID.Clear();
        //            students.Clear();
        //            App.Current.MainPage = new NavigationPage(new MainPage());
        //        }
        //        else
        //        {
        //            // DisplayErrorAlert("Something went wrong!");
        //            //TODO display an error to the user
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //        //TODO display an error to the user
        //    }
        //}
        //private void GetAllStudents()
        //{
        //    foreach (var subject in subjectsDB)
        //    {
        //        if (subject != null)
        //            subjectsNames.Add(subject.Name);
        //    }

        //}
        async void GoToStudentPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterStudent());


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
