using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CodeEditor : MonoBehaviour
{
    // Input field for the text content
    public InputField InputField;
    // Image used for line highlighting
    public Image LineHighlight;

    // Line number that is currently being highlighted
    [Range(0, 255)]
    public int CurrentHighlightedLine = 0;
    // LineHeight/Font Size
    [Range(0, 255)]
    public int LineHeight = 20;
    // Text to display in the
    [TextArea]
    public string Code;

    // Update is called once per frame
    void Update()
    {
        // Propagate text changes from the Text Input to the CodeEditor. This only needs to happen while running
        if (Application.isPlaying && InputField)
        {
            Code = InputField.text;
        }

        if (InputField == null) return;
        var textElements = InputField.GetComponentsInChildren<Text>();

        // Adjust the font size for the preview and display text objects
        foreach (var text in textElements)
        {
            text.fontSize = LineHeight;
        }

        // For sizing take first and assume all are the same size
        var textElement = textElements.First();

        var textRect = textElement.GetComponent<RectTransform>();
        var highlightTransform = LineHighlight.transform;
        // Adjust the element line highlights vertical size to match the font size
        LineHighlight.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, LineHeight);
        // Position the LineHighlight behind the correct line number
        highlightTransform.localPosition = new Vector3(highlightTransform.localPosition.x,
            (-LineHeight * CurrentHighlightedLine) - (LineHeight * 0.5f) - textRect.rect.yMin, 1);
    }

    // Updates the InputField text to match the code that has been entered in the Editor Field
    void OnValidate()
    {
        if (InputField)
        {
            InputField.text = Code;
        }
    }
}