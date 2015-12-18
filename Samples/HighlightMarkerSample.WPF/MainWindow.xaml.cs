using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using ObservableView;

namespace HighlightMarkerSample.WPF
{
    public partial class MainWindow : Window
    {
        public ObservableView<ListItem> ListItemsView { get; private set; }

        public MainWindow()
        {
            this.InitializeComponent();

            var listItems = new List<ListItem>
            {
                new ListItem { Title = "Groceries", Subtitle = "Buy bread, cheese, apples" },
                new ListItem { Title = "Devices", Subtitle = "Buy Nexus, Galaxy, Droid" },
                new ListItem { Title = "Toys", Subtitle = "Buy Lego" }
            };

            this.ListItemsView = new ObservableView<ListItem>(listItems);
            this.ListItemsView.AddSearchSpecification(x => x.Title);
            this.ListItemsView.AddSearchSpecification(x => x.Subtitle);

            this.searchBox.TextChanged += this.OnSearchBoxTextChanged;

            this.DataContext = this;
        }

        private void OnSearchBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            this.ListItemsView.Search(((TextBox)e.Source).Text);
        }
    }
}