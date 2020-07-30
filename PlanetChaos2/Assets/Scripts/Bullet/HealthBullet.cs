using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回血子弹类
/// </summary>
public class HealthBullet : BaseBullet
{
    [Header("治愈区的预制体")]
    public GameObject healthZonePrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            GameObject healthZoneObj = Instantiate(healthZonePrefab, transform.position, Quaternion.identity);
            TurnBaseMgr.GetInstance().RemainingTime = 6;
            TurnBaseMgr.GetInstance().IsPauseTimer = false;
            Destroy(gameObject);
        }
    }
}
