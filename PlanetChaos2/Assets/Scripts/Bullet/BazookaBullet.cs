using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaBullet : BaseBullet
{
    [Header("本身触发器的半径")]
    public float selfRadius = 0.15f;

    [Header("摧毁范围的触发器")]
    public CircleCollider2D destructAreaColl;

    [Header("物理检测的层")]
    public LayerMask layer;

    private float destructRadius;

    private void Update()
    {
        UpdateAngle();
        PhysicCheck();
    }

    private void PhysicCheck()
    {
        destructRadius = destructAreaColl.radius;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, selfRadius, layer);
        foreach (var coll in colliders)
        {
            IDestructable destructableTerrain = coll.GetComponent<IDestructable>();
            if (destructableTerrain != null)
            {
                destructableTerrain.Destruct(destructAreaColl);
                Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, destructRadius, layer);
                foreach(var cc in colls)
                {
                    IDestructable destructable = cc.GetComponent<IDestructable>();
                    if (destructable != null)
                    {
                        destructable.Destruct(destructAreaColl);
                    }
                }
                GameObject boomEffect = ResMgr.GetInstance().Load<GameObject>("FX/BoomEffect");
                boomEffect.transform.position = transform.position;
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// 画出角色底部的物理检测区域
    /// </summary>
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, selfRadius);
        Gizmos.DrawWireSphere(transform.position, destructRadius);
    }
}
