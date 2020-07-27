using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色控制器基类，重载了角色行为基类中的虚方法，实现了接口
/// </summary>
public class BaseCharacterController : BaseCharacterBehaviour
{
    protected float horizontalInput;

    protected new void Awake()
    {
        base.Awake();
        
    }

    protected void Start()
    {
        EventCenter.GetInstance().AddEventListener<bool>("武器朝右", OnIsWeaponRight);
    }

    protected void OnIsWeaponRight(bool isRight)
    {
        if (isAlive && IsControlling)
        {
            sprite.flipX = !isRight;
        }
    }

    

    protected void Update()
    {
        if (isAlive && IsControlling)
        {
            isOnGround = OnGround();
            ProcessInput();
            SwitchAnim();
        }
    }

    protected void FixedUpdate()
    {
        if (isAlive && IsControlling)
        {
            Move();
            Jump();
        }
    }

    protected void ProcessInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    protected void SwitchAnim()
    {
        anim.SetBool("isWalk", horizontalInput != 0);
        if(horizontalInput < 0)
        {
            sprite.flipX = true;
        }
        else if(horizontalInput > 0)
        {
            sprite.flipX = false;
        }

        anim.SetBool("isJump", isJump);

    }

    /// <summary>
    /// 移动
    /// </summary>
    protected override void Move()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    /// <summary>
    /// 跳跃
    /// </summary>
    protected override void Jump()
    {
        // 按下Jump
        if (Mathf.Abs(Input.GetAxis("Jump") - 1) < tolerance && !isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingSpeed);
            isJump = true;
            MusicMgr.GetInstance().PlaySound("Jump", false);
        }
        //抬起jump
        if (isOnGround && Mathf.Abs(Input.GetAxis("Jump")) < tolerance)
        {
            isJump = false;
        }

        // UP 上升， 减小重力
        if (rb.velocity.y > 0 && Mathf.Abs(Input.GetAxis("Jump") - 1) > tolerance)
        {
            rb.velocity += Vector2.up * ((lowUpMultiple - 1) * Physics2D.gravity.y * Time.fixedDeltaTime);
        }

        // DOWN 下降， 增加重力
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * ((fallMultiple - 1) * Physics2D.gravity.y * Time.fixedDeltaTime);
        }
    }

    /// <summary>
    /// 被治愈
    /// </summary>
    /// <param name="healPoint"></param>
    public override void Heal(int healPoint)
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

        //有回复量的情况下，显示回血字体，并向上飘动，逐渐消失
        if (increaseHP > 0)
        {
            if (!isAlive)
            {
                isAlive = true;
                anim.SetTrigger("Reborn");
            }
            GameObject healTextObj = ResMgr.GetInstance().Load<GameObject>("UI/Character/HealText");
            healTextObj.transform.SetParent(canvasTransform, false);
            FadeText healText = healTextObj.GetComponent<FadeText>();
            healText.SetHealPoint(increaseHP);
            healText.TextMoved();
        }
    }

    /// <summary>
    /// 受到damage点伤害
    /// </summary>
    /// <param name="damage"></param>
    public override void DoHurt(int damage)
    {
        int decreaseHP;
        if (currentHP - damage <= 0)
        {
            decreaseHP = currentHP;
        }
        else
        {
            decreaseHP = damage;
        }

        currentHP -= decreaseHP;
        Debug.Log("减少血量:" + decreaseHP);

        //有掉血量的情况下，显示掉血血字体，并向上飘动，逐渐消失
        if (decreaseHP > 0)
        {
            GameObject hurtTextObj = ResMgr.GetInstance().Load<GameObject>("UI/Character/HurtText");
            hurtTextObj.transform.SetParent(canvasTransform, false);
            FadeText healText = hurtTextObj.GetComponent<FadeText>();
            healText.SetHurtPoint(decreaseHP);
            healText.TextMoved();
            if (currentHP > 0)
            {
                Hurt();
            }
            else
            {
                Die();
            }
        }
    }

    /// <summary>
    /// 受伤
    /// </summary>
    protected override void Hurt()
    {
        anim.SetBool("isHurt", true);
        Invoke("EndHurt", 1f);
    }

    protected void EndHurt()
    {
        anim.SetBool("isHurt", false);
    }

    protected override void Die()
    {
        anim.SetTrigger("Die");
        isAlive = false;
        EquipMgr.GetInstance().Unload(transform);
        EventCenter.GetInstance().EventTrigger<BaseCharacterController>("玩家死亡", this);  //广播自己死亡的消息
    }

    protected void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener<bool>("武器朝右", OnIsWeaponRight);
    }
}
