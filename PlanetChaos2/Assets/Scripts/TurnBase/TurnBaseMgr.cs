using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 回合制管理器
/// </summary>
public class TurnBaseMgr : BaseManager<TurnBaseMgr>
{
    public TurnBaseMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(Update);
        
    }

    private int oneTurnDuration = 15;           //一回合的操作时间，默认为15秒
    public int OneTurnDuration { get { return oneTurnDuration; } set { oneTurnDuration = value; } }

    private int currentTurnIndex = 0;           //当前回合数
    public int CurrentTurnIndex { get { return currentTurnIndex; } set { currentTurnIndex = value; } }

    private int currentPlayerIndex;             //当前轮到的玩家的下标

    private int remainingTime;                  //当前回合剩余时间
    public int RemainingTime { get { return remainingTime; } set { remainingTime = value; } }

    private IEnumerator timer;                  //计时器协程的返回值

    private bool isPauseTimer;                  //是否暂停计时
    public bool IsPauseTimer { get { return isPauseTimer; } set { isPauseTimer = value; } }

    /// <summary>
    /// 开始回合制逻辑
    /// </summary>
    public void Start()
    {
        remainingTime = oneTurnDuration;
        currentPlayerIndex = 0;
        currentTurnIndex = 0;
        isPauseTimer = false;
        if (timer != null)
            Timer.StopRepeat(timer);
        
        currentPlayerIndex = currentTurnIndex % CharacterMgr.GetInstance().characterTransforms.Count;
        Transform currentTurnPlayer = CurrentPlayer();
        BaseCharacterController baseCharacterController = currentTurnPlayer.GetComponent<BaseCharacterController>();
        if (baseCharacterController.isAlive)
        {
            Debug.Log(remainingTime);
            timer = Timer.InvokeRepeat(TimerCountDown, 1f, 1f);
            CharacterMgr.GetInstance().Control(currentTurnPlayer);
            CharacterMgr.GetInstance().characterTransforms[currentPlayerIndex].GetComponent<CharacterUI>().SetTurnImageVisible(true);
        }
        else
        {
            NextTurn();
        }
    }

    public Transform CurrentPlayer()
    {
        return CharacterMgr.GetInstance().characterTransforms[currentPlayerIndex];
    }

    private void NextTurn()
    {
        currentPlayerIndex = currentTurnIndex % CharacterMgr.GetInstance().characterTransforms.Count;
        Transform currentTurnPlayer = CurrentPlayer();
        CharacterMgr.GetInstance().characterTransforms[currentPlayerIndex].GetComponent<CharacterUI>().SetTurnImageVisible(false);
        EquipMgr.GetInstance().Unload(currentTurnPlayer);
        CharacterMgr.GetInstance().CancelControl(currentTurnPlayer);
        currentTurnIndex++;
        currentPlayerIndex = currentTurnIndex % CharacterMgr.GetInstance().characterTransforms.Count;
        currentTurnPlayer = CurrentPlayer();
        BaseCharacterController baseCharacterController = currentTurnPlayer.GetComponent<BaseCharacterController>();
        if (baseCharacterController.isAlive)
        {
            remainingTime = oneTurnDuration;
            Debug.Log(remainingTime);
            EventCenter.GetInstance().EventTrigger("倒计时更新", remainingTime);
            timer = Timer.InvokeRepeat(TimerCountDown, 1f, 1f);
            CharacterMgr.GetInstance().Control(currentTurnPlayer);
            CharacterMgr.GetInstance().characterTransforms[currentPlayerIndex].GetComponent<CharacterUI>().SetTurnImageVisible(true);
            EventCenter.GetInstance().EventTrigger("回合数变更");
        }
        else
        {
            NextTurn();
        }
    }

    private void TimerCountDown()
    {
        if(remainingTime > 0)
        {
            if(!IsPauseTimer)
                remainingTime--;
            Debug.Log(remainingTime);
            EventCenter.GetInstance().EventTrigger("倒计时更新", remainingTime);
        }
        else
        {
            Timer.StopRepeat(timer);
            NextTurn();
        }
    }



    private void Update()
    {

    }
}
