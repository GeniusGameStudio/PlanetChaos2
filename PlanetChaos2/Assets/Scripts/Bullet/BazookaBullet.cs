using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 火箭弹类
/// </summary>
public class BazookaBullet : BaseBullet
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Entity"))
        {
            GameObject boomEffect = ResMgr.GetInstance().Load<GameObject>("FX/BoomEffect");
            boomEffect.transform.position = transform.position;
            TurnBaseMgr.GetInstance().RemainingTime = 6;
            TurnBaseMgr.GetInstance().IsPauseTimer = false;
            Destroy(gameObject);
        }
    }
}
