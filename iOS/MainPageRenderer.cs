using System;

using Xamarin.Forms;

namespace FaceRecognitionFrontEnd.iOS
{
    public class MainPageRenderer : ContentPage
    {
        public MainPageRenderer()
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

