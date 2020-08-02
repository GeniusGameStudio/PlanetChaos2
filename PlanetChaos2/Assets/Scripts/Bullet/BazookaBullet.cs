using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 火箭弹类
/// </summary>
public class BazookaBullet : BaseBullet
{
    private bool isBoomed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBoomed)
            return;
        if (collision.CompareTag("Ground") || collision.CompareTag("Entity"))
        {
            Debug.Log("火箭弹爆炸，生成爆炸特效");
            GameObject boomEffect = ResMgr.GetInstance().Load<GameObject>("FX/BoomEffect");
            boomEffect.transform.position = transform.position;
            TurnBaseMgr.GetInstance().RemainingTime = 6;
            TurnBaseMgr.GetInstance().IsPauseTimer = false;
            isBoomed = true;
            Destroy(gameObject);
        }
    }
}
