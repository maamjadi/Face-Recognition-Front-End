using System;
using System.Collections.Generic;
using FaceRecognitionFrontEnd.Pages;
using Xamarin.Forms;

namespace FaceRecognitionFrontEnd.Models
{
    public partial class SubjectItemModel : Grid
    {
        public SubjectItemModel()
        {
            InitializeComponent();
        }

        public SubjectItemModel(object item)
        {
            InitializeComponent();
            BindingContext = item;
        }

        private string subjectName;
        public string SubjectName
        {
            get { return subjectName; }
            set
            {
                subjectName = value;
                OnPropertyChanged("SubjectName");
            }
        }

        private string percentage;
        public string Percentage
        {
            get { return percentage; }
            set
            {
                percentage = value;
                OnPropertyChanged("Percentage");
            }
        }

        private string add;
        public string Add
        {
            get { return add; }
            set
            {
                add = value;
                OnPropertyChanged("Add");
            }
        }

        async void addSubject(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddSubject());
        }

        async void studentPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TempStudentPage());
        }
    }
}