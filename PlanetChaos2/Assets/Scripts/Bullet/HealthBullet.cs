using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBullet : BaseBullet
{
    [Header("治愈区的预制体")]
    public GameObject healthZonePrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            GameObject healthZoneObj = Instantiate(healthZonePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
