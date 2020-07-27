using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 关于游戏面板
/// </summary>
public class AboutPanel : BasePanel
{
    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            case "BackToMainMenuButton":
                UIManager.GetInstance().HidePanel("MainMenu/AboutPanel");
                break;
        }
    }
}
