using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : BaseBuff, IDestructable
{
    [Header("回复生命点数")]
    public int healthPoint;         //回复生命点数

    private Collision2D collision;

    public void Destruct(CircleCollider2D coll)
    {
        Destroy(gameObject);
    }

    public override void Effect()
    {
        IHeal heal = collision.gameObject.GetComponent<IHeal>();
        if (heal != null)
        {
            heal.Heal(healthPoint);
            MusicMgr.GetInstance().PlaySound("HealthBox", false);
            EndEffect();
        }
    }

    public override void EndEffect()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.collision = collision;
        Effect();
    }
}
