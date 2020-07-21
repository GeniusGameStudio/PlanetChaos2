using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBuff : MonoBehaviour, IEffect
{
    [Header("受Buff影响的层")]
    public LayerMask effectLayer;   //受Buff影响的层

    protected Collider2D coll;      //Buff影响的触发器

    protected Collider2D[] colls;   //受Buff影响的物体的碰撞器

    protected SpriteRenderer sprite;//精灵渲染器

    public virtual void Effect(){ }

    public virtual void EndEffect() { }

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
}
