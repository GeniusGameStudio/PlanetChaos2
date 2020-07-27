using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 爆炸物类
/// </summary>
public class Explosives : BaseBuff, IDestructable
{ 
    public void Destruct(CircleCollider2D coll)
    {
        Effect();
    }

    public override void Effect()
    {
        GameObject boomEffect = ResMgr.GetInstance().Load<GameObject>("FX/BoomEffect2");
        boomEffect.transform.position = transform.position;
        EndEffect();
    }

    public override void EndEffect()
    {
        Destroy(gameObject);
    }
}
