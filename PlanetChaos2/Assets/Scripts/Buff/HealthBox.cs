﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回血箱类
/// </summary>
public class HealthBox : BaseBuff, IDestructable
{
    [Header("回复生命点数")]
    public int healthPoint;         //回复生命点数

    private Collision2D collision;

    private Rigidbody2D rb;

    private float gravityScale;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityScale = rb.gravityScale;
    }

    public void Destruct(CircleCollider2D coll)
    {
        //GameObject boomEffect = ResMgr.GetInstance().Load<GameObject>("FX/BoomEffect");
        //boomEffect.transform.position = transform.position;
        Destroy(gameObject);
    }

    public override void Effect()
    {
        IHeal heal = collision.gameObject.GetComponent<IHeal>();
        if (heal != null && !isEffected)
        {
            heal.Heal(healthPoint);
            MusicMgr.GetInstance().PlaySound("HealthBox", false);
            EndEffect();
        }
    }

    public override void EndEffect()
    {
        isEffected = true;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.collision = collision;
        Effect();
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
        }
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.gravityScale = gravityScale;
            //rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
}
