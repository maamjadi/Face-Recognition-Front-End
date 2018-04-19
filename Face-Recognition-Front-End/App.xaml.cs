using DLToolkit.Forms.Controls;
using Xamarin.Forms;
using Xamvvm;
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

<<<<<<< HEAD
            MainPage = new NavigationPage(new MainPage());
=======
            FlowListView.Init();

            MainPage = new NavigationPage(new MainPage());

>>>>>>> adding_subjects
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
