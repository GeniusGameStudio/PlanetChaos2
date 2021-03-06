﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireZone : BaseBuff
{
    [Header("减少生命点数")]
    public int damagePoint;         //减少生命点数

    public override void Effect()
    {
        colls = Physics2D.OverlapCircleAll(transform.position, (coll as CircleCollider2D).radius, effectLayer);
        foreach (var coll in colls)
        {
            IHurt hurt = coll.GetComponent<IHurt>();
            if (hurt != null)
            {
                hurt.DoHurt(damagePoint);
                //MusicMgr.GetInstance().PlaySound("HealthZone", false);
            }
        }
    }

    public override void EndEffect()
    {
        //TODO__DOTween Sprite color 的alpha 从1到0，0.5秒，回调里摧毁本身
        Tweener fade = sprite.DOFade(0, 0.5f);
        fade.OnComplete(() => {
            Destroy(gameObject);
        });

    }

    private void Start()
    {
        //1秒后开始，每1秒产生一次回血效果
        InvokeRepeating("Effect", 1f, 1f);

        //2.2秒后开始终止回血效果，相当于只回复两次
        Invoke("EndEffect", 2.2f);
    }
}
