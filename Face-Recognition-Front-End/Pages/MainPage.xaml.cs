using System;
using System.Collections.Generic;
using Xamvvm;
using Xamarin.Forms;

namespace FaceRecognitionFrontEnd
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
           System.Diagnostics.Debug.WriteLine(App.teacherId);
        }

        //async void NavigateToSettings(object sender, EventArgs e) {
        //    App.Current.MainPage = new NavigationPage(new LoginPage());
        //}
    }
}
