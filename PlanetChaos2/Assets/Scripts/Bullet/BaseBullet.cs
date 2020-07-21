using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour, IShoot
{
    protected Rigidbody2D rb;

    public void Shoot(Vector2 shootDirection, float shootPower)
    {
        rb.velocity = shootDirection * shootPower;
    }

    protected void UpdateAngle()
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
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAngle();
    }
}
