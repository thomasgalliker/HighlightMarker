using HighlightMarkerSample.Forms.Views;

using Xamarin.Forms;

namespace HighlightMarkerSample.Forms
{
    public class App : Application
    {
        public App()
        {
            this.MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}