using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 主菜单面板类
/// </summary>
public class MainMenuPanel : BasePanel
{
    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            case "StartButton":
                //Test 测试阶段，跳转到空场景BattleScene
                //UIManager.GetInstance().HideAllPanel(()=> { 
                //    EventCenter.GetInstance().EventTrigger<Enum_SceneState>("场景切换", Enum_SceneState.Battle);
                //});

                //TODO 应该是选英雄和队伍调配阶段...
                UIManager.GetInstance().ShowPanel<ChooseHeroPanel>("MainMenu/ChooseHeroPanel");
                break;

            case "AboutButton":
                UIManager.GetInstance().ShowPanel<AboutPanel>("MainMenu/AboutPanel", E_UI_Layer.System);
                break;

            case "ExitGameButton":
                //TODO 最好有个确认弹窗
                Application.Quit();
                break;
        }
    }
}
