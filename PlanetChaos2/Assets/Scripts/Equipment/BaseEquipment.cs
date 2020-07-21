using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEquipment : MonoBehaviour, IEquipment
{
    //装备名称
    protected string equipName;

    //是否装备了自身
    protected bool isEquipedSelf;

    //是否已经更改过朝向
    protected bool isChangedDir;

    /// <summary>
    /// 设置装备名称
    /// </summary>
    /// <param name="equipName">装备名</param>
    protected void SetEquipName(string equipName)
    {
        this.equipName = equipName;
    }

    /// <summary>
    /// 装备朝向鼠标
    /// </summary>
    public void AimMouse()
    {
        Vector3 mousePosScreen = Input.mousePosition;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
        Vector2 weaponToMouse = new Vector2(mousePosWorld.x - transform.position.x,
                                            mousePosWorld.y - transform.position.y);

        weaponToMouse.Normalize();

        float angle = Mathf.Asin(weaponToMouse.y) * Mathf.Rad2Deg;
        if (weaponToMouse.x < 0f)
        {
            angle = 180 - angle;
        }

        if (weaponToMouse.x < 0f && !isChangedDir)
        {
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            isChangedDir = true;
        }
        else if (weaponToMouse.x > 0f && isChangedDir)
        {
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            isChangedDir = false;
        }
        transform.localEulerAngles = new Vector3(0f, 0f, angle);
    }

    /// <summary>
    /// 检查是否已经装备
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool CheckIfEquiped(Transform player)
    {
        IEquipment equipment = player.GetComponentInChildren<IEquipment>();
        return equipment != null;
    }

    /// <summary>
    /// 装备
    /// </summary>
    /// <param name="player"></param>
    public void Equip(Transform player)
    {
        if (!CheckIfEquiped(player))
        {
            Debug.Log(equipName + "，装备在了" + player.gameObject.name + "身上");
            transform.SetParent(player);
            isEquipedSelf = true;
        }
        else
        {
            Debug.LogError("已经装备过武器了");
            isEquipedSelf = false;
        }
    }

    /// <summary>
    /// 卸下装备
    /// </summary>
    /// <param name="player"></param>
    public void Unload(Transform player)
    {
        if (CheckIfEquiped(player))
        {
            Debug.Log(equipName + "，从" + player.gameObject.name + "身上卸下");
            isEquipedSelf = false;
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("未装备，无法卸下任何武器");
        }
    }

    void Update()
    {
        AimMouse();
    }
}
