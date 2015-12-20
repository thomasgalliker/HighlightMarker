using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ObservableView;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using HighlightMarkerSample.Data.Model;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HighlightMarkerSample.WindowsStore81
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableView<Mall> ListItemsView { get; }

        public MainPage()
        {
            this.InitializeComponent();

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
    }
}
