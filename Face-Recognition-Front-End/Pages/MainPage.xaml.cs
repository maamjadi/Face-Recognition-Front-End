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

        async void addSubject(object sender, EventArgs e) {
            await Navigation.PushAsync(new AddSubject());
        }
    }
}
