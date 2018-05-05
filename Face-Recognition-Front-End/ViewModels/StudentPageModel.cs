using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using FaceRecognitionFrontEnd.Models;

namespace FaceRecognitionFrontEnd.ViewModels
{
    public class StudentPageModel : ContentPage
    {

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

            String[] names = {
                "Ali",
                "Amra",
                "Amin",
                "Nilufar",
                "noredin",
                "zebi"
            };

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
                    if (attendances[i] != 0) {
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

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

