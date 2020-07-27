using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 爆炸特效类
/// </summary>
public class BoomFX : BaseFX
{
    [Header("爆炸造成的最大伤害")]
    public int maxDamage = 50;

    [Header("最大冲击力")]
    public float maxForce;

    private CircleCollider2D coll;

    private Dictionary<GameObject, bool> effectObjDic = new Dictionary<GameObject, bool>();

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDestructable destructable = collision.GetComponent<IDestructable>();
        if (destructable != null && !effectObjDic.ContainsKey(collision.gameObject))
        {
            Debug.Log(collision.name);
            destructable.Destruct(coll);
            effectObjDic.Add(collision.gameObject, true);
        }
        IHurt hurt = collision.GetComponent<IHurt>();
        if(hurt != null && !effectObjDic.ContainsKey(collision.gameObject))
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

            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                Vector2 vec = boomToCollision.normalized;
                float force = maxForce * 100000 / (distance + 1f);
                rb.AddForce(vec * force);
            }
            effectObjDic.Add(collision.gameObject, true);
        }
    }
}
