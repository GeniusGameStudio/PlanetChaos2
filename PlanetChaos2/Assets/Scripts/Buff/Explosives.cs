using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 爆炸物类
/// </summary>
public class Explosives : BaseBuff, IDestructable
{
    private Rigidbody2D rb;

    private float gravityScale;

    private bool effected;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityScale = rb.gravityScale;
    }

    public void Destruct(CircleCollider2D coll)
    {
        if(!effected)
            Effect();
    }

    public override void Effect()
    {
        Debug.Log("爆炸物爆炸，生成爆炸特效");
        effected = true;
        GameObject boomEffect = ResMgr.GetInstance().Load<GameObject>("FX/BoomEffect2");
        boomEffect.transform.position = transform.position;
        EndEffect();
    }

    public override void EndEffect()
    {
        Destroy(gameObject);
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
