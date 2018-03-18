using System;
using System.Collections.Generic;
using Xamvvm;
using Xamarin.Forms;

namespace FaceRecognitionFrontEnd
{
    public partial class MainPage : ContentPage, IBasePage<MainPageModel>
    {
        public MainPage()
        {
            InitializeComponent();
            System.Diagnostics.Debug.WriteLine(App.teacherId);
        }
    }
}
