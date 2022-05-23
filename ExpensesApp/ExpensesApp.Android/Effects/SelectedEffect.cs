
using Android.Graphics.Drawables;
using ExpensesApp.Droid.Effects;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("LPA")]
[assembly: ExportEffect(typeof(SelectedEffect), "SelectedEffect")]
namespace ExpensesApp.Droid.Effects
{
    public class SelectedEffect : PlatformEffect
    {
        Android.Graphics.Color SelectedColor;
        protected override void OnAttached()
        {
            SelectedColor = new Android.Graphics.Color(176, 152, 164);
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            try
            {
                if (args.PropertyName == "IsFocused")
                {
                    if (((ColorDrawable)Control.Background).Color != SelectedColor)
                    {
                        Control?.SetBackgroundColor(SelectedColor);
                    }
                    else
                    {
                        Control?.SetBackgroundColor(Android.Graphics.Color.White);
                    }
                }
            }
            catch (InvalidCastException)
            {

                Control?.SetBackgroundColor(SelectedColor);
            }

            
        }

        protected override void OnDetached()
        {
        }
    }
}