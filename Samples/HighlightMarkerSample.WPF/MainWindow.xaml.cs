using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using HighlightMarkerSample.Data.Model;

using ObservableView;

namespace HighlightMarkerSample.WPF
{
    public partial class MainWindow : Window
    {
        public ObservableView<Mall> ListItemsView { get; }

        public MainWindow()
        {
            this.InitializeComponent();

            var listItems = MallManager.GetMalls();

            this.ListItemsView = new ObservableView<Mall>(listItems);
            this.ListItemsView.AddSearchSpecification(x => x.Title);
            this.ListItemsView.AddSearchSpecification(x => x.Subtitle);

            this.searchBox.Focus();
            this.searchBox.TextChanged += this.OnSearchBoxTextChanged;

            this.DataContext = this;
        }

        private void OnSearchBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            this.ListItemsView.Search(((TextBox)e.Source).Text);
        }
    }
}