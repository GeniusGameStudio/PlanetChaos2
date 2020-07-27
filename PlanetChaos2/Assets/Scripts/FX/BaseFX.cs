using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 特效基类
/// </summary>
public abstract class BaseFX : MonoBehaviour, IDisappear
{
    protected SpriteRenderer sprite;

    protected void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        MusicMgr.GetInstance().PlaySound("Boom", false);
    }

    public void Disappear()
    {
        Destroy(gameObject);
    }

}
