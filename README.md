# HighlightMarker
HighlightMarker is a library which supports you in highlighting an input string in a given text. Essentially, it does nothing more than telling you from/to which character of a given text you need to start/stop highlighting the user's search input.

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
