using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 活跃的按钮类
/// </summary>
public class ActiveButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }
    /// <summary>
    /// 点击时，如果按钮可用，则播放声音
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if(button.interactable)
            MusicMgr.GetInstance().PlaySound("Button", false);
    }

    /// <summary>
    /// 鼠标进入时，如果按钮可用，则0.5秒将按钮尺寸从1.0放大至1.2
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable)
            transform.DOScale(1.2f, 0.5f).SetUpdate(true);
    }

    /// <summary>
    /// 鼠标离开时，如果按钮可用，则0.5秒将按钮恢复1.0倍尺寸
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (button.interactable)
            transform.DOScale(1f, 0.5f).SetUpdate(true);
    }
}
