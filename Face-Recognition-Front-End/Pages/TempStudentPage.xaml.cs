using System;
using System.Collections.Generic;
using FaceRecognitionFrontEnd.ViewModels;
using Xamarin.Forms;

namespace FaceRecognitionFrontEnd.Pages
{
    public partial class TempStudentPage : ContentPage
    {
        public TempStudentPage()
        {
            InitializeComponent();
            BindingContext = new TempStudentPageViewModel();

        }
    }
}
