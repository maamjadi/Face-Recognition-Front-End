using System;
using System.Collections.Generic;
using FaceRecognitionFrontEnd.ViewModels;
using Xamarin.Forms;

namespace FaceRecognitionFrontEnd
{
    public partial class StudentPage : ContentPage
    {
        public StudentPage()
        {
            InitializeComponent();
            BindingContext = new StudentPageModel();
        }
        async void addStudent(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterStudent());
        }
    }
}