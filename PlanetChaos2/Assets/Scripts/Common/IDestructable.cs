using UnityEngine;
/// <summary>
/// 可被破坏的接口
/// </summary>
public interface IDestructable
{
    /// <summary>
    /// 在coll范围内被破坏
    /// </summary>
    /// <param name="coll"></param>
    void Destruct(CircleCollider2D coll);
}
