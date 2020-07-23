using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
