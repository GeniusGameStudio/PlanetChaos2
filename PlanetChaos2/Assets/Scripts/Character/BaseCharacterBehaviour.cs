using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 角色的行为抽象类
/// </summary>
public abstract class BaseCharacterBehaviour : MonoBehaviour, IHeal, IHurt, IControl
{
    public CharacterData CharacterData { get; set; }

    protected Animator anim;                //动画机

    protected Rigidbody2D rb;               //刚体

    protected Collider2D coll;              //碰撞器

    protected Transform canvasTransform;    //画布的位置

    protected SpriteRenderer sprite;        //精灵渲染器

    public bool isAlive = true;             //是否存活

    public bool IsControlling = false;      //是否正在被控制

    protected bool dropHurt = false;        //下次触碰地面是否受伤

    [Header("最大生命值")]
    public int maxHP = 100;                 //最大生命值，默认100

    [Header("当前生命值")]
    [SerializeField]
    protected int currentHP;                //当前生命值
    public int CurrentHP { get { return currentHP; } set { currentHP = value; } }

    [Header("移动速度")]
    public float moveSpeed = 1;             //移动速度，默认1

    [Header("跳跃")]
    public float jumpingSpeed;
    public float lowUpMultiple;
    public float fallMultiple;

    [Header("地板检测")]
    public Vector2 offset; // 检测盒和物件position的偏移
    public Vector2 size; // 检测盒的大小
    public LayerMask groundLayerMask; // 地面层

    protected bool isOnGround;
    protected float speedX;
    protected bool isJump;
    protected float tolerance = 1e-7f;  // 浮点数和int类型的最小误差（可有可无）

    protected void InitData()
    {
        currentHP = maxHP;

        //测试血量
        //currentHP = 8;
    }

    protected void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        canvasTransform = transform.Find("Canvas");
        
        InitData();
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

    /// <summary>
    /// 跌落受伤
    /// </summary>
    protected virtual void DropHurt() { }

    /// <summary>
    /// 被治愈
    /// </summary>
    /// <param name="healPoint"></param>
    public virtual void Heal(int healPoint) { }

    protected bool OnGround()
    {
        Collider2D overlapBox = Physics2D.OverlapBox((Vector2)transform.position + offset, size, 0, groundLayerMask);
        return overlapBox != null;
    }

    /// <summary>
    /// 画出角色底部的物理检测盒
    /// </summary>
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube((Vector2)transform.position + offset, size);
    }

    /// <summary>
    /// 受到damage点伤害
    /// </summary>
    /// <param name="damage"></param>
    public virtual void DoHurt(int damage) { }

    /// <summary>
    /// 被控制
    /// </summary>
    public void Control()
    {
        IsControlling = true;
    }

    /// <summary>
    /// 取消控制
    /// </summary>
    public void CancleControl()
    {
        IsControlling = false;
    }
}
