using UnityEngine;

/// <summary>
/// 可发射的，接口
/// </summary>
public interface IShoot
{
    /// <summary>
    /// 发射
    /// </summary>
    /// <param name="shootDirection"></param>
    /// <param name="shootTime"></param>
    void Shoot(Vector2 shootDirection, float shootTime);
}
