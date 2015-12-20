using System.Collections.ObjectModel;

using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using ObservableView;

namespace HighlightMarkerSample.WPA81
{
    public sealed partial class MainPage : Page
    {
        public ObservableView<ListItem> ListItemsView { get; private set; }

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            var listItems = new Collection<ListItem>
            {
                new ListItem { Title = "Groceries", Subtitle = "Buy bread, cheese, apples" },
                new ListItem { Title = "Devices", Subtitle = "Buy Nexus, Galaxy, Droid" },
                new ListItem { Title = "Toys", Subtitle = "Buy Lego" }
            };

            this.ListItemsView = new ObservableView<ListItem>(listItems);
            this.ListItemsView.AddSearchSpecification(x => x.Title);
            this.ListItemsView.AddSearchSpecification(x => x.Subtitle);

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