using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimUI : MonoBehaviour
{
    private float maxShootTime;

    private Animator anim;

    private void Start()
    {
        maxShootTime = GetComponentInParent<BaseGun>().maxShootTime;
        anim = GetComponentInChildren<Animator>();
        anim.speed = anim.speed / maxShootTime; 
        EventCenter.GetInstance().AddEventListener("开始蓄力", OnStartStorage);
        EventCenter.GetInstance().AddEventListener("取消发射子弹", OnCancleShootBullet);
        EventCenter.GetInstance().AddEventListener("发射子弹", OnShootBullet);
    }

    private void OnStartStorage()
    {
        anim.SetBool("isShooting", true);
    }

    private void OnCancleShootBullet()
    {
        anim.SetBool("isShooting", false);
    }

    private void OnShootBullet()
    {
        anim.SetBool("isShooting", false);
    }

    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener("开始蓄力", OnStartStorage);
        EventCenter.GetInstance().RemoveEventListener("取消发射子弹", OnCancleShootBullet);
        EventCenter.GetInstance().RemoveEventListener("发射子弹", OnShootBullet);
    }


}
