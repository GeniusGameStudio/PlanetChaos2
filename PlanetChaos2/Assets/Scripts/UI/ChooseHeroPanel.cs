using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseHeroPanel : BasePanel
{
    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            case "BackToMainMenuButton":
                UIManager.GetInstance().HidePanel("MainMenu/ChooseHeroPanel");
                break;
        }
    }
}
