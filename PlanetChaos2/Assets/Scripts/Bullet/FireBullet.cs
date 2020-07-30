using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : BaseBullet
{
    [Header("燃烧区的预制体")]
    public GameObject fireZonePrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            GameObject fireZoneObj = Instantiate(fireZonePrefab, transform.position, Quaternion.identity);
            TurnBaseMgr.GetInstance().RemainingTime = 6;
            TurnBaseMgr.GetInstance().IsPauseTimer = false;
            Destroy(gameObject);
        }
    }
}
