using System;

using Xamarin.Forms;

[assembly: ExportRenderer(typeof(MainPage), typeof(FridgeContentPageRenderer))]
namespace FaceRecognitionFrontEnd.Droid.PageRenderers
{
    public class MyPage : ContentPage
    {
        public MyPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

