using HighlightMarkerSample.Forms.ViewModels;
using Xamarin.Forms;

namespace HighlightMarkerSample.Forms.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            var vm = new MainViewModel();
            this.BindingContext = vm;
        }
    }
}