using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaBullet : BaseBullet
{

    private void Update()
    {
        UpdateAngle();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Entity"))
        {
            GameObject boomEffect = ResMgr.GetInstance().Load<GameObject>("FX/BoomEffect");
            boomEffect.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
