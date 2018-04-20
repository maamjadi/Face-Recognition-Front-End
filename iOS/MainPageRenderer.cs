using System;
using FaceRecognitionFrontEnd;
using FaceRecognitionFrontEnd.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using System.Collections.Generic;

[assembly: ExportRenderer(typeof(MainPage), typeof(MainPageRenderer))]
namespace FaceRecognitionFrontEnd.iOS
{
    public class MainPageRenderer : PageRenderer
    {
        public new MainPage Element
        {
            get { return (MainPage)base.Element; }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var LeftNavList = new List<UIBarButtonItem>();
            var rightNavList = new List<UIBarButtonItem>();

            var navigationItem = this.NavigationController.TopViewController.NavigationItem;

            for (var i = 0; i < Element.ToolbarItems.Count; i++)
            {

                var ItemPriority = Element.ToolbarItems[i].Priority;

                if (ItemPriority == 1)
                {
                    UIBarButtonItem LeftNavItems = navigationItem.RightBarButtonItems[i];
                    LeftNavList.Add(LeftNavItems);
                }
                else if (ItemPriority == 0)
                {
                    UIBarButtonItem RightNavItems = navigationItem.RightBarButtonItems[i];
                    rightNavList.Add(RightNavItems);
                }
            }

            navigationItem.SetLeftBarButtonItems(LeftNavList.ToArray(), false);
            navigationItem.SetRightBarButtonItems(rightNavList.ToArray(), false);

        }
    }
}

