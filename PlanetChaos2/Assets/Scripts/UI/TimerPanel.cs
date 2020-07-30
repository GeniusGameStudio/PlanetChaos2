using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerPanel : BasePanel
{
    private Text timeText;

    void Start()
    {
        timeText = GetControl<Text>("TimeText");
        EventCenter.GetInstance().AddEventListener<int>("倒计时更新", OnTimerUpdate);
    }

    private void OnTimerUpdate(int remainingTime)
    {
        timeText.text = remainingTime.ToString();
    }

    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener<int>("倒计时更新", OnTimerUpdate);
    }
}
