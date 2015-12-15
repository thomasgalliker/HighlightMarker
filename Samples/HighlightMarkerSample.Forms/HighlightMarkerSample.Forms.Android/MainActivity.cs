using Android.App;
using Android.OS;

using Xamarin.Forms.Platform.Android;

namespace HighlightMarkerSample.Forms.Android
{
    [Activity(Label = "HighlightMarkerSample.Forms.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);
            this.LoadApplication(new App());
        }
    }
}

