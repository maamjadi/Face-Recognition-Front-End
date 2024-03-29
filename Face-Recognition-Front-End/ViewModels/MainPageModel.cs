﻿using System;
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
    public class MainPageModel : INotifyPropertyChanged
    {
        private ObservableCollection<SubjectItemModel> items;
        public ObservableCollection<SubjectItemModel> Items
        {
            get { return items;  }
            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }

        public MainPageModel() 
        {
            var list = new ObservableCollection<SubjectItemModel>();

            string[] subjects = {
                "Client Side Technologies",
                "Client Side Technologies",
                "Client Side Technologies",
                "Client Side Technologies",
                "Client Side Technologies",
                "Client Side Technologies",
                "Client Side Technologies",
                "Client Side Technologies",
                "Client Side Technologies",
            };

            int[] percentages = {
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}