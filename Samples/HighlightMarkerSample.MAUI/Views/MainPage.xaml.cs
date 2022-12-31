using System.Collections.Generic;
using HighlightMarkerSample.Model;
using ObservableView;

namespace HighlightMarkerSample.MAUI
{
    public partial class MainPage : ContentPage
    {
        public ObservableView<Mall> Malls { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            this.Malls = new ObservableView<Mall>(MallManager.GetMalls());
            this.Malls.SearchSpecification.Add(mall => mall.Title);
            this.Malls.SearchSpecification.Add(mall => mall.Subtitle);

            this.BindingContext = this;
        }

        private void SearchBarOnTextChanged(object sender, TextChangedEventArgs e)
        {
            this.Malls.Search(e.NewTextValue);
        }
    }
}