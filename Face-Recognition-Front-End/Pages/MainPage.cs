using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FaceRecognitionFrontEnd.Pages
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            System.Diagnostics.Debug.WriteLine(App.teacherId);
            List<RelativeLayout> list = new List<RelativeLayout> { };
            for (int i = 0; i < 20; i++)
            {
                RelativeLayout layout = new RelativeLayout() { };
                var image = new Image
                {
                    Aspect = Aspect.AspectFill,
                    HorizontalOptions = LayoutOptions.Start,
                };
                image.Source = "landing/shoe.png";
                layout.Children.Add(
                    image,
                    Constraint.RelativeToParent((parent) => {
                        return (parent.Width * 0);
                    }),
                    Constraint.RelativeToParent((parent) => {
                        return (parent.Height * 0);
                    }),
                    Constraint.RelativeToParent((parent) => {
                        return (parent.Width * 1);
                    }),
                    Constraint.RelativeToParent((parent) => {
                        return (parent.Height * 1);
                    })
                );
                list.Add(layout);
            }
            CollectionView collection = new CollectionView();
            collection.ItemTouchedAction = new Action<int>(Clicked);
            Content = collection;
            collection.Items = list;
        }

        void Clicked(int index)
        {
            System.Diagnostics.Debug.WriteLine("Index " + index + " clicked!");
        }
    }
}
