using System;
using System.Collections.Generic;
using Xamarin.Forms;
using FaceRecognitionFrontEnd.ViewModels;
using System.Net;
using Newtonsoft.Json.Linq;

namespace FaceRecognitionFrontEnd
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
            System.Diagnostics.Debug.WriteLine(App.teacherId);
            BindingContext = new MainPageModel();


        }

    }
}