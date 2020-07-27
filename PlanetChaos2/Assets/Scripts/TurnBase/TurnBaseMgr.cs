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

    private int remainingTime;                  //当前回合剩余时间

    private IEnumerator timer;

    /// <summary>
    /// 开始回合制逻辑
    /// </summary>
    public void Start()
    {
        remainingTime = oneTurnDuration;
        Debug.Log(remainingTime);
        timer = Timer.InvokeRepeat(TimerCountDown, 1f, 1f);
        CharacterMgr.GetInstance().Control(CharacterMgr.GetInstance().characterTransforms[currentTurnIndex]);
    }

    private void NextTurn()
    {
        CharacterMgr.GetInstance().CancelControl(CharacterMgr.GetInstance().characterTransforms[currentTurnIndex]);
        currentTurnIndex++;
        if (currentTurnIndex >= CharacterMgr.GetInstance().characterTransforms.Count)
            currentTurnIndex = 0;
        remainingTime = oneTurnDuration;
        Debug.Log(remainingTime);
        timer = Timer.InvokeRepeat(TimerCountDown, 1f, 1f);
        CharacterMgr.GetInstance().Control(CharacterMgr.GetInstance().characterTransforms[currentTurnIndex]);
    }

    private void TimerCountDown()
    {
        if(remainingTime > 0)
        {
            remainingTime--;
            Debug.Log(remainingTime);
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
