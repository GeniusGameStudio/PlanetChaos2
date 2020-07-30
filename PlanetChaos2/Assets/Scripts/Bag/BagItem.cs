using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagItem : BasePanel
{
    [TextArea]
    public string info;

    protected Button button;

    private void Start()
    {
        button = GetComponentInChildren<Button>();

        button.onClick.AddListener(OnButtonClick);

        UIManager.AddCustomEventListener(button, EventTriggerType.PointerEnter, OnPointerEnter);
        UIManager.AddCustomEventListener(button, EventTriggerType.PointerExit, OnPointerExit);
    }

    protected virtual void OnButtonClick()
    {
        Debug.Log("道具被点击");
    }

    protected virtual void OnPointerEnter(BaseEventData data)
    {
        Debug.Log("鼠标进入道具");
        UIManager.GetInstance().ShowPanel<InfoPanel>("Battle/InfoPanel", E_UI_Layer.Top, (panel)=> {
            panel.SetInfoText(info);
        });
    }

    protected virtual void OnPointerExit(BaseEventData data)
    {
        Debug.Log("鼠标移出道具");
        UIManager.GetInstance().HidePanel("Battle/InfoPanel");
    }

}
