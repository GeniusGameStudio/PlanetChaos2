using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseFX : MonoBehaviour, IDisappear
{
    protected void Start()
    {
        MusicMgr.GetInstance().PlaySound("Boom", false);
    }

    public void Disappear()
    {
        Destroy(gameObject);
    }

}
