using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
