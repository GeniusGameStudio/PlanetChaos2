using UnityEngine;
/// <summary>
/// 角色的行为抽象类
/// </summary>
public abstract class ICharacterBehaviour : MonoBehaviour
{
    protected Animator anim;

    protected Rigidbody2D rb;

    protected Collider2D coll;

    protected void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    /// <summary>
    /// 移动
    /// </summary>
    protected virtual void Move() { }

    /// <summary>
    /// 跳跃
    /// </summary>
    protected virtual void Jump() { }

    /// <summary>
    /// 受伤
    /// </summary>
    protected virtual void Hurt() { }

    /// <summary>
    /// 死亡
    /// </summary>
    protected virtual void Die() { }
}
