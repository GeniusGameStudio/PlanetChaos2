using UnityEngine;
/// <summary>
/// 装备接口
/// </summary>
public interface IEquipment
{
    /// <summary>
    /// 装备
    /// </summary>
    void Equip(Transform player);

    /// <summary>
    /// 卸下
    /// </summary>
    void Unload(Transform player);

    /// <summary>
    /// 检查是否已经装备过武器
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    bool CheckIfEquiped(Transform player);

    /// <summary>
    /// 瞄准鼠标
    /// </summary>
    void AimMouse();
}
