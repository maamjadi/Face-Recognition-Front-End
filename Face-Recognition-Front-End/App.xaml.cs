﻿
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FaceRecognitionFrontEnd
{
    public partial class App : Application
    {
        public static string teacherId = "";
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new StartPage());
        }

     
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
