using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGun : BaseEquipment, IShootBullet
{
    [Header("子弹的预制体")]
    public GameObject bulletPrefab;                 //子弹的预制体

    [Header("射击点的位置")]
    public Transform shootTransform;                //射击点的位置

    protected float keyDownTimestamp;               //鼠标左键按下的时间戳

    protected float keyUpTimestamp;                 //鼠标左键抬起的时间戳

    protected float shootTime;                      //蓄力时间

    [Header("最大蓄力时间")]
    public float maxShootTime;                      //最大蓄力时间

    [Header("发射力的大小")]
    public float shootPower = 10;                   //发射力的大小

    protected bool isShooting;

    protected AudioSource storageSound;             //蓄力的音效

    [Header("弹药音效对应Resources的文件名")]
    public string bulletSoundName = "Bullet";      //弹药音效对应Resources的文件名，默认是Bullet

    private void Start()
    {
        isWeapon = true;
        EventCenter.GetInstance().AddEventListener<KeyCode>("某键按下", OnKeyDown);
        EventCenter.GetInstance().AddEventListener<KeyCode>("某键抬起", OnKeyUp);

        //设置鼠标不可见
        Cursor.visible = false;
    }

    /// <summary>
    /// 按键按下的回调
    /// </summary>
    /// <param name="key"></param>
    protected void OnKeyDown(KeyCode key)
    {
        if(key == KeyCode.Mouse0)
        {
            keyDownTimestamp = Time.time;
            isShooting = true;
            EventCenter.GetInstance().EventTrigger("开始蓄力");
            MusicMgr.GetInstance().PlaySound("Storage",false, (source) => {
                storageSound = source;
            });
        }
        else if(key == KeyCode.Mouse1)
        {
            CancelShootBullet();
            
        }
    }

    /// <summary>
    /// 按键抬起的回调
    /// </summary>
    /// <param name="key"></param>
    protected void OnKeyUp(KeyCode key)
    {
        if (key == KeyCode.Mouse0 && isShooting)
        {
            keyUpTimestamp = Time.time;
            shootTime = keyUpTimestamp - keyDownTimestamp;
            float currentShootPower = shootPower * shootTime;
            ShootBullet(shootTransform, currentShootPower);
            isShooting = false;
        }
    }

    /// <summary>
    /// 检查蓄力时间，如果超过最大蓄力时间，则按最大蓄力时间发射子弹
    /// </summary>
    protected void CheckShootTime()
    {
        if(Time.time - keyDownTimestamp >= maxShootTime && isShooting)
        {
            float currentShootPower = shootPower * maxShootTime;
            ShootBullet(shootTransform, currentShootPower);
            isShooting = false;
        }
    }

    /// <summary>
    /// 取消发射子弹
    /// </summary>
    public void CancelShootBullet()
    {
        Debug.Log("取消了发射");
        EventCenter.GetInstance().EventTrigger("取消发射子弹");
        MusicMgr.GetInstance().StopSound(storageSound);
        isShooting = false;
    }

    /// <summary>
    /// 在发射点位置，按一定力度发射子弹
    /// </summary>
    /// <param name="shootTransform"></param>
    /// <param name="shootPower"></param>
    public void ShootBullet(Transform shootTransform, float shootPower)
    {
        EventCenter.GetInstance().EventTrigger("发射子弹");
        MusicMgr.GetInstance().StopSound(storageSound);
        MusicMgr.GetInstance().PlaySound(bulletSoundName, false);
        Vector2 shootDirection;
        Vector3 mousePosScreen = Input.mousePosition;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
        Vector2 weaponToMouse = new Vector2(mousePosWorld.x - transform.position.x,
                                            mousePosWorld.y - transform.position.y);

        weaponToMouse.Normalize();

        shootDirection = weaponToMouse;

        GameObject bulletObj = Instantiate(bulletPrefab, shootTransform.position, Quaternion.identity);
        IShoot shoot = bulletObj.GetComponent<IShoot>();
        shoot.Shoot(shootDirection, shootPower);
        Debug.Log(bulletObj.name + "被" + gameObject.name + "发射出去了! 发射方向：" + shootDirection);
    }

    private void Update()
    {
        AimMouse();
        CheckShootTime();
    }

    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener<KeyCode>("某键按下", OnKeyDown);
        EventCenter.GetInstance().RemoveEventListener<KeyCode>("某键抬起", OnKeyUp);
    }
}
