using System;
using System.Windows.Input;
using FaceRecognitionFrontEnd.utilities;
using Plugin.Media;
using Xamarin.Forms;

namespace FaceRecognitionFrontEnd.ViewModels
{
    public class TempStudentPageViewModel : BaseViewModel
    {
        public ICommand Recognize { get; set; }
        private string recognizedPerson = "Uknown";


        public string RecognizedPerson
        {
            get { return recognizedPerson; }
            set
            {
                SetProperty(ref recognizedPerson, value);
            }
        }

        public TempStudentPageViewModel()
        {
            Recognize = new Command(RecognizeStudent);
        }
        async private void RecognizeStudent()
        {


            //System.Diagnostics.Debug.WriteLine("hi ali");

            var mediaFile = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });
            if (mediaFile == null)
                return;
            var userName = await RecMan.ExecuteFindSimilarFaceCommandAsync(Constants.GroupId, mediaFile);
            //TODO show the user name of the recognized student or show unknown
            RecognizedPerson = userName;

        }
    }
}
