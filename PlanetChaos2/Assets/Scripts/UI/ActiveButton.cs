using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActiveButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(button.interactable)
            MusicMgr.GetInstance().PlaySound("Button", false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable)
            transform.DOScale(1.2f, 0.5f).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (button.interactable)
            transform.DOScale(1f, 0.5f).SetUpdate(true);
    }
}
