using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹基类
/// </summary>
public abstract class BaseBullet : MonoBehaviour, IShoot, IDisappear
{
    protected Rigidbody2D rb;

    public void Disappear()
    {
        TurnBaseMgr.GetInstance().RemainingTime = 6;
        TurnBaseMgr.GetInstance().IsPauseTimer = false;
        Destroy(gameObject);
    }

    public void Shoot(Vector2 shootDirection, float shootPower)
    {
        rb.velocity = shootDirection * shootPower;
    }

    protected virtual void UpdateAngle()
    {
        Vector2 dir = new Vector2(rb.velocity.x, rb.velocity.y);

        dir.Normalize();
        float angle = Mathf.Asin(dir.y) * Mathf.Rad2Deg;
        if (dir.x < 0f)
        {
            angle = 180 - angle;
        }

        transform.localEulerAngles = new Vector3(0f, 0f, angle + 45f);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //15秒后销毁，以免占用内存
        Invoke("Disappear", 15f);
    }

    private void Start()
    {
        UpdateAngle();
    }

    protected void CheckBorder()
    {
        if(transform.position.y < -5f || transform.position.x < -10f || transform.position.x > 10f)
        {
            
            Disappear();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAngle();
        CheckBorder();
    }
}
