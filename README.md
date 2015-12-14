# HighlightMarker
HighlightMarker is a library which supports you in highlighting an input string in a given text. Essentially, it tells you from/to which character of a given text you need to start/stop highlighting the user's search input.

### Download and Install HighlightMarker
This library is available on NuGet: https://www.nuget.org/packages/HighlightMarker/
Use the following command to install HighlightMarker using NuGet package manager console:

    PM> Install-Package HighlightMarker

You can use this library in any .Net project which is compatible to PCL (e.g. Xamarin Android, iOS, Windows Phone, Windows Store, Universal Apps, etc.)

### How to use HighlightMarker
To explain the usage of HighlightMarker, it's best to consult the existing unit tests. Following test shows how a certain ```FullText``` is highlighted with the string ```SearchText```:

```
// Arrange
const string FullText = "full text for highlight marking";
const string SearchText = "highlight";
var highlightMarker = new HighlightMarker(FullText, SearchText);

// Act
var highlightEnumerator = highlightMarker.GetEnumerator();
var highlightList = highlightEnumerator.ToList<HighlightIndex>();

// Assert
```
![GitHub Logo](/readme/images/highlightlist.png)

### License
HighlightMarker is Copyright &copy; 2015 [Thomas Galliker](https://ch.linkedin.com/in/thomasgalliker). Free for non-commercial use. For commercial use please contact the author.


### Sources

https://developer.xamarin.com/recipes/ios/standard_controls/text_field/style_text/

https://www.syntaxismyui.com/xamarin-forms-searchbar-recipe/

https://developer.xamarin.com/guides/cross-platform/xamarin-forms/user-interface/listview/customizing-cell-appearance/#Custom_Cells




