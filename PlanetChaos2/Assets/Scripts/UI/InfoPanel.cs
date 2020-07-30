using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InfoPanel : BasePanel
{
    private Text infoText;

    public override void ShowMe()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.2f).SetUpdate(true);
    }

    public override void HideMe(UnityAction callBack)
    {
        transform.DOScale(0, 0.2f).SetUpdate(true).OnComplete(() => { callBack(); });
    }

    public void SetInfoText(string info)
    {
        infoText = GetControl<Text>("Text");
        infoText.text = info;
    }
}
