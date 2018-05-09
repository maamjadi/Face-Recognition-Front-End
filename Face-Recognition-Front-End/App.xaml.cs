using DLToolkit.Forms.Controls;
using Xamarin.Forms;
using Xamvvm;
using Xamarin.Forms.Xaml;
using FaceRecognitionFrontEnd.utilities;
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FaceRecognitionFrontEnd
{
    public partial class App : Application
    {
        public static string teacherId = "5af22bf476caa89aad2627b1";
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
