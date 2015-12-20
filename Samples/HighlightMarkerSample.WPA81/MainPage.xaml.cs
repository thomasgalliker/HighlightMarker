using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using HighlightMarkerSample.Data.Model;

using ObservableView;

namespace HighlightMarkerSample.WPA81
{
    public sealed partial class MainPage : Page
    {
        public ObservableView<Mall> ListItemsView { get; }

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            var listItems = MallManager.GetMalls();

            this.ListItemsView = new ObservableView<Mall>(listItems);
            this.ListItemsView.AddSearchSpecification(x => x.Title);
            this.ListItemsView.AddSearchSpecification(x => x.Subtitle);

            this.searchBox.Focus(FocusState.Keyboard);
            this.searchBox.TextChanged += this.OnSearchBoxTextChanged; // You could use SearchText data binding in XAML instead

            this.DataContext = this;
        }

        private void OnSearchBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            this.ListItemsView.Search(this.searchBox.Text);
        }

        /// <summary>
        ///     Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">
        ///     Event data that describes how this page was reached.
        ///     This parameter is typically used to configure the page.
        /// </param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }
    }
}