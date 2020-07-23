using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosives : BaseBuff, IDestructable
{ 
    [Header("摧毁范围的触发器")]
    public CircleCollider2D destructAreaColl;

    [Header("物理检测的层")]
    public LayerMask layer;

    private float destructRadius;
    public void Destruct(CircleCollider2D coll)
    {
        Effect();
    }

    public override void Effect()
    {
        destructRadius = destructAreaColl.radius;
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, destructRadius, layer);
        foreach (var cc in colls)
        {
            IDestructable destructable = cc.GetComponent<IDestructable>();
            if (destructable != null)
            {
                destructable.Destruct(destructAreaColl);
            }
        }
        GameObject boomEffect = ResMgr.GetInstance().Load<GameObject>("FX/BoomEffect2");
        boomEffect.transform.position = transform.position;
        EndEffect();
    }

    public override void EndEffect()
    {
        Destroy(gameObject);
    }
}
