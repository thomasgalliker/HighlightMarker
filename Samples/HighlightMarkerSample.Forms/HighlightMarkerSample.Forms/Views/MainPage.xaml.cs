using HighlightMarkerSample.Forms.Model;
using System.Collections.ObjectModel;
using ObservableView;
using ObservableView.Extensions;

using Xamarin.Forms;

namespace HighlightMarkerSample.Forms.Views
{
    public partial class MainPage : ContentPage
    {
        public ObservableView<Mall> Malls { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            this.Malls = new ObservableView<Mall>(MallManager.GetMalls());
            this.Malls.AddSearchSpecification(mall => mall.Title);
            this.Malls.AddSearchSpecification(mall => mall.Subtitle);

            this.BindingContext = this;
        }

        private void SearchBarOnTextChanged(object sender, TextChangedEventArgs e)
        {
            this.Malls.Search(e.NewTextValue);
        }
    }
}