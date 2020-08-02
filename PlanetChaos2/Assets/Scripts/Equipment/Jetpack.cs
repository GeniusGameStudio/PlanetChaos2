using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jetpack : BaseEquipment, IFly
{
    private Animator anim;

    private Animator playerAnim;

    private bool isUp;

    private bool isUsed;

    [Header("飞行速度")]
    public float flySpeed;

    [Header("可用时间")]
    public float useTime = 8;

    private bool playerOnGround;

    [Header("能量条")]
    public Slider energySlider;

    private void Awake()
    {
        SetEquipName("喷射机");
        anim = GetComponent<Animator>();
        EventCenter.GetInstance().AddEventListener<KeyCode>("某键按下", OnKeyDown);
        EventCenter.GetInstance().AddEventListener<KeyCode>("某键抬起", OnKeyUp);
    }

    private void UnloadSelf()
    {
        if(playerTransform != null)
            Unload(playerTransform);
    }

    private void Update()
    {
        if (playerRb != null)
        {
            anim.SetFloat("VerticalSpeed", playerRb.velocity.y);
            if (playerTransform != null)
            {
                playerOnGround = playerTransform.GetComponent<BaseCharacterController>().IsOnGround;
                if (!playerOnGround)
                {
                    playerAnim = playerTransform.GetComponent<Animator>();
                    playerAnim.SetBool("isWalk", false);
                    playerAnim.SetBool("isJump", false);
                }
                else
                {
                    Land();
                }
            }
        }
        Fly();
    }

    /// <summary>
    /// 按键按下的回调
    /// </summary>
    /// <param name="key"></param>
    protected void OnKeyDown(KeyCode key)
    {
        if (GameManager.GetInstance().IsGamePaused || GameManager.GetInstance().IsChooseItem)
            return;
        if (key == KeyCode.W)
        {
            TakeOff();
        }
    }

    /// <summary>
    /// 按键抬起的回调
    /// </summary>
    /// <param name="key"></param>
    protected void OnKeyUp(KeyCode key)
    {
        if (GameManager.GetInstance().IsGamePaused || GameManager.GetInstance().IsChooseItem)
            return;
        if(key == KeyCode.W)
        {
            isUp = false;
        }
    }

    public void Land()
    {
        if (playerTransform != null)
        {
            playerTransform.GetComponent<BaseCharacterController>().Control();
        }
    }

    public void TakeOff()
    {
        if(playerRb != null && !isUp)
        {
            playerRb.gravityScale /= 3;
            playerRb.velocity = new Vector2(playerRb.velocity.x, flySpeed);
            isUp = true;
            if (!isUsed)
            {
                energySlider.DOValue(0, useTime).OnComplete(() => {
                    UnloadSelf();
                });
                isUsed = true;
            }
        }
    }

    public void Fly()
    {
        if (isUp && playerRb != null)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, flySpeed);
        }
    }

    private void OnDestroy()
    {
        if (playerRb != null)
        {
            playerRb.gravityScale *= 2;
        }
        EventCenter.GetInstance().RemoveEventListener<KeyCode>("某键按下", OnKeyDown);
        EventCenter.GetInstance().RemoveEventListener<KeyCode>("某键抬起", OnKeyUp);
    }

    
}
