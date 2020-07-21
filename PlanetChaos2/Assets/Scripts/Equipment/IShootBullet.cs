using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 射击子弹的接口
/// </summary>
public interface IShootBullet
{
    /// <summary>
    /// 在射击点发射子弹
    /// </summary>
    /// <param name="shootTransform"></param>
    /// <param name="shootPower"></param>
    void ShootBullet(Transform shootTransform, float shootPower);

    /// <summary>
    /// 取消发射子弹
    /// </summary>
    void CancelShootBullet();
}
