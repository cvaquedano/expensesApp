using ExpensesApp.Interfaces;
using ExpensesApp.iOS.Dependencies;
using Foundation;
using System;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Share))]
namespace ExpensesApp.iOS.Dependencies
{
    public class Share : IShare
    {
        public async Task Show(string tittle, string message, string filePath)
        {
            var viewController = GetVisibleViewController();
            var items = new NSObject[] { NSObject.FromObject(tittle), NSUrl.FromFilename(filePath) };
            var activityController = new UIActivityViewController(items, null);

            if (activityController.PopoverPresentationController != null)
            {
                activityController.PopoverPresentationController.SourceView = viewController.View;
            }

            await viewController.PresentViewControllerAsync(activityController,true);
        }

        UIViewController GetVisibleViewController()
        {
            var rootViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;

            if (rootViewController.PresentedViewController == null)
            {
                return rootViewController;
            }
            if (rootViewController.PresentedViewController is UINavigationController controller)
            {
                return controller.TopViewController;
            }
            if (rootViewController.PresentedViewController is UITabBarController controller1)
            {
                return controller1.SelectedViewController;
            }

            return rootViewController.PresentedViewController;

        }
    }
}