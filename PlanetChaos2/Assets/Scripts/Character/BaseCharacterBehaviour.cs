using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 角色的行为抽象类
/// </summary>
public abstract class BaseCharacterBehaviour : MonoBehaviour, IHeal
{
    protected Animator anim;                //动画机

    protected Rigidbody2D rb;               //刚体

    protected Collider2D coll;              //碰撞器

    protected Transform canvasTransform;    //画布的位置

    [Header("最大生命值")]
    public int maxHP = 100;                 //最大生命值，默认100

    protected int currentHP;                //当前生命值
    public int CurrentHP { get { return currentHP; } set { currentHP = value; } }

    protected void InitData()
    {
        //currentHP = maxHP;

        //测试血量
        currentHP = 8;
    }

    protected void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
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
    /// 被治愈
    /// </summary>
    /// <param name="healPoint"></param>
    public virtual void Heal(int healPoint) 
    {
        int increaseHP;
        if (currentHP + healPoint >= maxHP)
        {
            increaseHP = maxHP - currentHP;
        }
        else
        {
            increaseHP = healPoint;
        }

        currentHP += increaseHP;
        Debug.Log("增加血量:" + increaseHP);
        //TODO__UI显示增加HP

        //有回复量的情况下，显示回血字体，并向上飘动，逐渐消失
        if (increaseHP > 0)
        {
            //TODO
            GameObject healTextObj = ResMgr.GetInstance().Load<GameObject>("UI/Character/HealText");
            healTextObj.transform.SetParent(canvasTransform, false);
            HealText healText = healTextObj.GetComponent<HealText>();
            healText.SetHealPoint(increaseHP);
            healText.TextMoved();
        }

    }
}
