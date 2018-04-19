using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamvvm;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using DLToolkit.Forms.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using FaceRecognitionFrontEnd.Models;

namespace FaceRecognitionFrontEnd.ViewModels
{
    public class MainPageModel : INotifyPropertyChanged
    {
        private ObservableCollection<ItemModel> items;
        public ObservableCollection<ItemModel> Items
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
            var list = new ObservableCollection<ItemModel>();

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
                var item = new ItemModel()
                {
                    SubjectName = subjects[i],
                    Percentage = string.Format("{0}%", percentages[i]),
                };

                list.Add(item);
            }

            list.Add(addCreateNewSubjectBtn());

            Items = list;
        }

        private ItemModel addCreateNewSubjectBtn()
        {
            var item = new ItemModel()
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