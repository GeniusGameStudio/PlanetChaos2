using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomFX : BaseFX
{
    [Header("爆炸造成的最大伤害")]
    public int maxDamage = 50;

    private CircleCollider2D coll;

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHurt hurt = collision.GetComponent<IHurt>();
        if(hurt != null)
        {
            Vector2 boomToCollision = collision.transform.position - transform.position;
            float distance = boomToCollision.magnitude;
            int damage;
            float radius = coll.radius;
            float one = radius / 4;
            if (distance <= one)
                damage = (int)(maxDamage * Random.Range(0.8f, 1.0f));
            else if (distance <= one * 2)
                damage = (int)(maxDamage * Random.Range(0.6f, 0.8f));
            else if (distance <= one * 3)
                damage = (int)(maxDamage * Random.Range(0.4f, 0.6f));
            else if (distance <= one * 4)
                damage = (int)(maxDamage * Random.Range(0.2f, 0.4f));
            else
                damage = 0;
            hurt.DoHurt(damage);
        }
    }
}
