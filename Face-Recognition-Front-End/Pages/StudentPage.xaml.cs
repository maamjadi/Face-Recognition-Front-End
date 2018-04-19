using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FaceRecognitionFrontEnd
{
    public partial class StudentPage : ContentPage
    {
        public StudentPage()
        {
            InitializeComponent();
        }
        async void addStudent(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new  RegisterStudent());
        }
    }
}
