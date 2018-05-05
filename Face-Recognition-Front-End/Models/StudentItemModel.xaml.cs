using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FaceRecognitionFrontEnd.Models
{
    public partial class StudentItemModel : Grid
    {
        public StudentItemModel()
        {
            InitializeComponent();
        }

        public StudentItemModel(object item)
        {
            InitializeComponent();
            BindingContext = item;
        }

        private string imageName;
        public string ImageName
        {
            get { return imageName; }
            set
            {
                imageName = value;
                OnPropertyChanged("ImageName");
            }
        }

        private string nameOfPerson;
        public string NameOfPerson
        {
            get { return nameOfPerson; }
            set
            {
                nameOfPerson = value;
                OnPropertyChanged("NameOfPerson");
            }
        }

        private string numberOfAttendence;
        public string NumberOfAttendence
        {
            get { return numberOfAttendence; }
            set
            {
                numberOfAttendence = value;
                OnPropertyChanged("NumberOfAttendence");
            }
        }

        async void studentCheck(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new StudentPage());
        }
    }
}