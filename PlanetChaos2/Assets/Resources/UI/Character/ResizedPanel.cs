using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResizedPanel : BasePanel
{
    private Text text;

    private void Start()
    {
        text = GetControl<Text>("Text");
        (transform as RectTransform).sizeDelta = new Vector2(text.rectTransform.sizeDelta.x + 8, (transform as RectTransform).sizeDelta.y);
    }

    private void Update()
    {
        (transform as RectTransform).sizeDelta = new Vector2(text.rectTransform.sizeDelta.x + 8, (transform as RectTransform).sizeDelta.y);
    }
}
