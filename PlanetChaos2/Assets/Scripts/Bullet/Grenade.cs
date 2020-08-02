using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : BaseBullet
{
    private bool isTouchingGround;
    protected override void UpdateAngle()
    {
        Vector2 dir = new Vector2(rb.velocity.x, rb.velocity.y);

        dir.Normalize();
        float angle = Mathf.Asin(dir.y) * Mathf.Rad2Deg;
        if (dir.x < 0f)
        {
            angle = 180 - angle;
        }
        rb.angularVelocity = angle;
    }

    private void Update()
    {
        UpdateAngle();
        CheckBorder();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Entity"))
        {
            if (!isTouchingGround)
            {
                Invoke("Explode", 3f);      //触碰地面后3秒爆炸
                isTouchingGround = true;
            }
        }
    }

    private void Explode()
    {
        GameObject boomEffect = ResMgr.GetInstance().Load<GameObject>("FX/BoomEffect3");
        boomEffect.transform.position = transform.position;
        TurnBaseMgr.GetInstance().RemainingTime = 6;
        TurnBaseMgr.GetInstance().IsPauseTimer = false;
        Destroy(gameObject);
    }
}
