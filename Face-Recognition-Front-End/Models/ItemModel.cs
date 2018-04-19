using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace FaceRecognitionFrontEnd.Models
{
    public class ItemModel : INotifyPropertyChanged
    {
        // implement  INotifyPropertyChanged interface

        public ItemModel()
        {
            ToggleCommand = new Command(CmdToggle);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CmdToggle()
        {
            IsSelected = !IsSelected;
        }

         private string subjectName;
            public string SubjectName
            {
                get { return subjectName; }
            set {
                subjectName = value;
                OnPropertyChanged("SubjectName");
            }
            }

            private string percentage;
            public string Percentage
            {
                get { return percentage; }
            set { 
                percentage = value;
                OnPropertyChanged("Percentage");
            }
            }

            private string add;
            public string Add
            {
                get { return add; }
            set {
                add = value;
                OnPropertyChanged("Add");
            }
            }

        public bool IsSelected
        {
            get;
            set; //call OnPropertyChanged
        }

        public ICommand ToggleCommand { get; private set; }

    }
}

