using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 设置面板类（暂时只有音频管理）
/// </summary>
public class SettingPanel : BasePanel
{

    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            case "AudioButton":
                UIManager.GetInstance().ShowPanel<AudioManagerPanel>("MainMenu/AudioManagerPanel", E_UI_Layer.Top);
                break;
        }
    }
}
