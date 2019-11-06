using System.Windows;
using HighlightMarkerSample.Model;
using ObservableView;

namespace HighlightMarkerSample.WPF
{
    public partial class MainWindow : Window
    {
        public ObservableView<Mall> ListItemsView { get; private set; }

        public MainWindow()
        {
            var backgroundProperty = HighlightMarker.WPF.TextBlockHighlighting.BackgroundProperty;
            this.InitializeComponent();

            var listItems = MallManager.GetMalls();

            this.ListItemsView = new ObservableView<Mall>(listItems);
            this.ListItemsView.SearchSpecification.Add(x => x.Title);
            this.ListItemsView.SearchSpecification.Add(x => x.Subtitle);

            this.searchBox.Focus();

            
            // In this example we use the binding Text = "{Binding ListItemsView.SearchText, Mode=TwoWay}"
            // in order to bind the searchBox.Text to the SearchText property of the ObservableView< Mall >.
            // Therefore, the following OnSearchBoxTextChanged event handler can be removed:

            // this.searchBox.TextChanged += this.OnSearchBoxTextChanged;
            

            this.DataContext = this;
        }
    }
}
