using ExpensesApp.iOS.Effects;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("LPA")]

[assembly: ExportEffect(typeof(SelectedEffect), "SelectedEffect")]
namespace ExpensesApp.iOS.Effects
{
    public class SelectedEffect : PlatformEffect
    {
        UIColor SelectedColor; 
        protected override void OnAttached()
        {
            SelectedColor = new UIColor(176.0f / 255, 152.0f / 255, 164.0f / 255, 255.0f / 255);
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == "IsFocused")
            {
                if (Control.BackgroundColor != SelectedColor)
                {
                    Control.BackgroundColor = SelectedColor;
                }
                else
                {
                    Control.BackgroundColor = UIColor.White;
                }
            }
        }

        protected override void OnDetached()
        {
        }
    }
}