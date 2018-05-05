using System;

using Xamarin.Forms;

namespace FaceRecognitionFrontEnd.Views
{
    public class GridView : ContentPage
    {
        public GridView()
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

