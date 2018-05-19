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
using Newtonsoft.Json;

namespace FaceRecognitionFrontEnd.ViewModels
{
    public class MainPageModel : INotifyPropertyChanged
    {
        private ObservableCollection<SubjectItemModel> items;
        public const string path = "/subjects";
        private List<string> students = new List<string>();
        private List<Subject> subjectsDB;
        private List<string> subjectsNames = new List<string>();
        private List<int> allPercentage = new List<int>() ;
        private bool isLoaded = false;
        public ObservableCollection<SubjectItemModel> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }
       
        public  MainPageModel()
        {

            Task.Run(async () => { await getSubjectsFromDB();});

           
        }
       
        private SubjectItemModel addCreateNewSubjectBtn()
        {
            var item = new SubjectItemModel()
            {
                Add = "+",
            };
            return item;
        }
       

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        private async Task getSubjectsFromDB()
        {
            if (!isLoaded)
            {
                try
                {

                    var response = await RestClient.Get(path + "/"+App.teacherId);
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

                        subjectsDB = JObject.Parse(data)["teacherSubjects"].ToObject<List<Subject>>();
                        GetAllSubjectsNames();
                        GetAllSubjectsPercentages();
                        isLoaded = true;
                        App.Current.MainPage = new NavigationPage(new MainPage());
                    }
                    else
                    {
                        // DisplayErrorAlert("Something went wrong!");
                        //TODO display an error to the user
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    //TODO display an error to the user
                }
            }
        }
        private void GetAllSubjectsNames()
        {
            foreach (var subject in subjectsDB)
            {
                if (subject != null)
                    subjectsNames.Add(subject.Name);
            }

        }
        private void GetAllSubjectsPercentages()
        {
            foreach (var subject in subjectsDB)
            {
                allPercentage.Add(subject.AttendedSessions);
            }
            ShowTheView();
        }




        private void ShowTheView()
        {
            var list = new ObservableCollection<SubjectItemModel>();

            string[] subjects = subjectsNames.ToArray();
          

            int[] percentages = allPercentage.ToArray();

            for (int i = 0; i < subjects.Length; i++)
            {
                var item = new SubjectItemModel()
                {
                    SubjectName = subjects[i],
                    Percentage = string.Format("{0}%", percentages[i]),
                };

                list.Add(item);
            }

            list.Add(addCreateNewSubjectBtn());

            Items = list;
            isLoaded = true;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}