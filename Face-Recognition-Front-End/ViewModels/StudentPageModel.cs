using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using FaceRecognitionFrontEnd.Models;
using System.Net;
using Newtonsoft.Json.Linq;
using Plugin.Media;
using FaceRecognitionFrontEnd.utilities;

namespace FaceRecognitionFrontEnd.ViewModels
{
    public class StudentPageModel : ContentPage
    {
        private List<string> studentsNames = new List<string>();
        private List<int> allattendances = new List<int>();
        private List<Student> studentsDB;
        public const string path = "/subjects/getStudents";

        public ICommand Recognize { get; set; }
        private string recognizedPerson = "Uknown";


        public string RecognizedPerson
        {
            get { return recognizedPerson; }
            set
            {
                recognizedPerson = value;
                OnPropertyChanged("RecognizedPerson");
            }
        }

        private ObservableCollection<StudentItemModel> items;
        public ObservableCollection<StudentItemModel> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }


        public StudentPageModel()
        {
             //Task.Run(async () => { await getStudentsFromDB(); });

            ShowTheView();
            Recognize = new Command(RecognizeStudent);


        }
        async private Task getStudentsFromDB()
        {
            //try
            //{

                var response = await RestClient.Get(path + "/5aeb55c7ac37007780fda3ea");
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    System.Diagnostics.Debug.WriteLine("bad reponse from the backend");
                    // DisplayErrorAlert("Wrong email or password");
                    //TODO display an error to the user
                }
                else if (response.StatusCode == HttpStatusCode.OK)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    var results = JObject.Parse(data);
                    //var obj = JsonConvert.DeserializeObject<dynamic>(data);
                    //var subjects2 = ((JArray)obj.teacherSubjects)[0].ToObject<Subject>();
                    //students = subjects2.Students;

                    studentsDB = JObject.Parse(data)["students"].ToObject<List<Student>>();
                    GetAllStudentsNames();
                    GetAllStudentsPercentages();
                    App.Current.MainPage = new NavigationPage(new MainPage());
               }
                else
                {
                    // DisplayErrorAlert("Something went wrong!");
                    //TODO display an error to the user
                }
            //}
            //catch (Exception ex)
            //{
            //    System.Diagnostics.Debug.WriteLine(ex.Message);
            //    //TODO display an error to the user
            //}
        }
       async private void RecognizeStudent(){

           
            //System.Diagnostics.Debug.WriteLine("hi ali");

            var mediaFile = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });
            if (mediaFile == null)
                return;
            var userName = await RecMan.ExecuteFindSimilarFaceCommandAsync(Constants.GroupId, mediaFile);
            //TODO show the user name of the recognized student or show unknown
            RecognizedPerson = userName;

        }
        private void GetAllStudentsNames()
        {
            foreach (var student in studentsDB)
            {
                if (student != null)
                    studentsNames.Add(student.UserName);
            }

        }
        private void GetAllStudentsPercentages()
        {
            foreach (var student in studentsDB)
            {
                allattendances.Add(0);
            }
            ShowTheView();
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void ShowTheView()
        {
            var list = new ObservableCollection<StudentItemModel>();

            title = "Client Side";

            string[] pictures = {
                "profileSample.png",
                "profileSample.png",
                "profileSample.png",
                "profileSample.png",
                "profileSample.png",
                "profileSample.png"
            };

            //String[] names = studentsNames.ToArray();
            String[] names = {
                "Ali",
                "Amra",
                "Amin",
                "Nilufar",
                "noredin",
                "zebi"
            };
            //int[] attendances = allattendances.ToArray();
            int[] attendances = {
                57,
                23,
                24,
                65,
                75,
                34,
                23,
                10,
                90,
            };

            for (int i = 0; i < pictures.Length; i++)
            {
                String attendance = "None";
                if (attendances[i] != 0)
                {
                    attendance = string.Format("{0} Absents", attendances[i]);
                }
                var item = new StudentItemModel()
                {
                    ImageName = pictures[i],
                    NameOfPerson = names[i],
                    NumberOfAttendence = attendance,
                };

                list.Add(item);
            }

            Items = list;

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
