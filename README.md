<img src="https://raw.githubusercontent.com/thomasgalliker/HighlightMarker/master/logo_cropped.png" ><br>
HighlightMarker is a library which supports you in highlighting text of a UI label. This can be particularly helpful if you want to highlight the user's search input e.g. in a list of text items. 

| Xamarin Android | Xamarin iOS | Windows Presentation Foundation |
| ----------------|-------------|---------------------------------|
| <img src="/Img/Screenshot.Android.png" width="190"> | <img src="/Img/Screenshot.iOS.png" width="190"> | <img src="/Img/Screenshot.WPF.png" width="190"> |

### Download and Install HighlightMarker
This library is available on NuGet: https://www.nuget.org/packages/HighlightMarker/
Use the following command to install HighlightMarker using NuGet package manager console:

    PM> Install-Package HighlightMarker

The library is compatible with .NET Framework 4.5+, .NET 5+ and .NET Standard 1.0+.
There is a special NuGet package for Xamarin.Forms available:

    PM> Install-Package HighlightMarker.Forms

### How to use HighlightMarker
Essentially, HighlightMarker tells you from/to which character of a given text you need to start/stop highlighting the user's search input. To explain how HighlightMarker works, it's best to consult the a simple unit test. Following test shows how a given ```FullText``` is highlighted with the string in variable ```SearchText```:

```
// Arrange
const string FullText = "full text for highlight marking";
const string SearchText = "highlight";
var highlightMarker = new HighlightMarker(FullText, SearchText);

// Act
var highlightList = highlightMarker.ToList();

// Assert
...
```
![Debug view of highlightList](/Img/Debug_HighlightList.png)

In order to make this highlighting logic accessible to *any* UI, there is a couple of platform-specific implementations for UI text highlighting. Have a look at the following subchapters:

#### Using HighlightMarker in Xamarin.Forms
In the folder Samples\HighlightMarker.Forms you can find a Xamarin.Forms demo project which displays a searchable list of shopping malls. The ```<ViewCell.View>``` defines a custom cell template for the malls list. The most interesting part are the custom bindings named ```TextHighlightBehavior.HighlightedText``` and ```TextHighlightBehavior.FullText```. All you need to do is binding the HighlightedText property to the search string (in our case we reference the Text property of the SearchBar) and binding the FullText property to the ViewModel property. 

```
    <Label forms:TextHighlightBehavior.HighlightedText="{Binding Text, Source={x:Reference SearchBar}}"
           forms:TextHighlightBehavior.FullText="{Binding Title}" />
```

#### Using HighlightMarker in Native Xamarin.Android projects
Create a custom ListFragment and update each list item's content (which can be a custom FrameLayouts for example) when the highlight text has changed. There is an extension method  ```TextViewExtensions.HighlightText``` for highlighting the text of a ```TextView``` object.

#### Using HighlightMarker in Native Xamarin.iOS projects
The sample project HighlightMarkerSample.iOS illustrates how to use HighlightMarker in Xamarin.iOS projects. CustomTableViewCell (which is an implementation of UITableViewCell) is responsible for updating the highlighting. ```UILabelExtensions``` contains an extension method ```HighlightText``` which is used to highlight the text of a UILabel.
```
public class CustomTableViewCell : UITableViewCell
{
    private readonly UILabel headingLabel;
    private readonly UILabel subheadingLabel;

    public CustomTableViewCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
    {
       //...
    }

    public void UpdateCell(string caption, string subtitle, UIImage image, string searchText)
    {
        this.headingLabel.Text = caption;
        this.headingLabel.HighlightText(searchText, UIColor.Blue);
        this.subheadingLabel.Text = subtitle;
        this.subheadingLabel.HighlightText(searchText, UIColor.Blue);
    }
    
    //...
```

### Feedback
Let me know your optinion and how we can improve this project. You are kindly invited to write an issue if you want to discuss problems and/or propose new features. Contributors are highly welcome!

### License
HighlightMarker is Copyright &copy; 2019 [Thomas Galliker](https://ch.linkedin.com/in/thomasgalliker). Free for non-commercial use. For commercial use please contact the author.

### Sources

https://developer.xamarin.com/recipes/ios/standard_controls/text_field/style_text/

https://www.syntaxismyui.com/xamarin-forms-searchbar-recipe/

https://developer.xamarin.com/guides/cross-platform/xamarin-forms/user-interface/listview/customizing-cell-appearance/#Custom_Cells




