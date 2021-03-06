﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备管理器
/// </summary>
public class EquipMgr : BaseManager<EquipMgr>
{
    /// <summary>
    /// 装备名称对应Resources文件夹的路径的字典
    /// </summary>
    private Dictionary<string, string> equipResDic = new Dictionary<string, string>();

    //当前装备的名称
    public string CurrentEquipmentName { get; set; }

    /// <summary>
    /// 初始化装备资源字典
    /// </summary>
    private void InitEquipResDic()
    {
        equipResDic.Add("回血枪", "Objects/HealthGun");
        equipResDic.Add("火箭筒", "Objects/Bazooka");
        equipResDic.Add("火焰枪", "Objects/FireGun");
        equipResDic.Add("手榴弹", "Objects/Grenade");
        equipResDic.Add("喷射机", "Objects/Jetpack");
    }

    /// <summary>
    /// 在单例首次实例化的时候调用
    /// </summary>
    public EquipMgr()
    {
        InitEquipResDic();
    }

    
    public void Equip(string equipName, Transform player)
    {
        if (equipResDic.ContainsKey(equipName))
        {
            GameObject equipObj = ResMgr.GetInstance().Load<GameObject>(equipResDic[equipName]);
            IEquipment equipment = equipObj.GetComponent<IEquipment>();
            equipment.Equip(player);
            CurrentEquipmentName = equipName;
        }
    }

    
    public void Unload(Transform player)
    {
        IEquipment equipment = player.GetComponentInChildren<IEquipment>();
        if(equipment != null)
        {
            equipment.Unload(player);
        }
    }
}
