using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 逐渐消失并上升的文字效果类
/// </summary>
public class FadeText : MonoBehaviour
{
    private Text fadeText;
    private void Awake()
    {
        fadeText = GetComponent<Text>();
    }

    /// <summary>
    /// 设置治疗量
    /// </summary>
    /// <param name="healPoint"></param>
    public void SetHealPoint(int healPoint)
    {
        fadeText.text = "+" + healPoint;
    }

    /// <summary>
    /// 设置受伤量
    /// </summary>
    /// <param name="hurtPoint"></param>
    public void SetHurtPoint(int hurtPoint)
    {
        fadeText.text = "-" + hurtPoint;
    }

    /// <summary>
    /// 文字移动
    /// </summary>
    public void TextMoved()
    {
        Graphic graphic = fadeText;

        //获得Text的rectTransform，和颜色，并设置颜色微透明

        RectTransform rect = graphic.rectTransform;

        Color color = graphic.color;

        graphic.color = new Color(color.r, color.g, color.b, 0);

        //设置一个DOTween队列

        Sequence textMoveSequence = DOTween.Sequence();

        //设置Text移动和透明度的变化值

        Tweener textMove01 = rect.DOMoveY(rect.position.y + 0.2f, 0.5f);

        Tweener textMove02 = rect.DOMoveY(rect.position.y + 0.2f, 0.5f);
        Tweener textMove03 = rect.DOMoveY(rect.position.y, 0.5f);


        Tweener textColor01 = graphic.DOColor(new Color(color.r, color.g, color.b, 1), 0.5f);

        Tweener textColor02 = graphic.DOColor(new Color(color.r, color.g, color.b, 0), 0.5f);


        //Append 追加一个队列，Join 添加一个队列

        //中间间隔一秒

        //Append 再追加一个队列，再Join 添加一个队列

        textMoveSequence.Append(textMove01);

        textMoveSequence.Join(textColor01);

        textMoveSequence.AppendInterval(0.6f);

        textMoveSequence.Append(textMove02);

        textMoveSequence.Join(textColor02);
        textMoveSequence.Append(textMove03);

        textMoveSequence.Join(textColor02);

        //完成动画后，销毁，以免占用内存
        textMoveSequence.OnComplete(()=> {
            Destroy(gameObject);
        });
    }
}
