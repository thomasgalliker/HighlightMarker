using HighlightMarkerSample.Model;
using Windows.UI.Xaml.Controls;
using ObservableView;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HighlightMarkerSample.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableView<Mall> ListItemsView { get; private set; }

        public MainPage()
        {
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
