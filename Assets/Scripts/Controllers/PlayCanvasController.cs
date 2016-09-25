using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayCanvasController : CanvasController {

    public Text adjectiveText;
    public Text nounText;

    public RectTransform buttonRectTransform;

    void Start()
    {
        Reset();
    }

    public override void OnTouchDown( Vector2 position )
    {
        if( buttonRectTransform && RectTransformUtility.RectangleContainsScreenPoint( buttonRectTransform, position ) )
        {
            Reset();
        }
    }

    private void Reset()
    {
        if( adjectiveText )
        {
            adjectiveText.text = DictionaryManager.GetNextAdjective();
        }

        if( nounText )
        {
            nounText.text = DictionaryManager.GetNextNoun();
        }
    }
}
