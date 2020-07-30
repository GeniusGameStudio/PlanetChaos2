using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftMenuPanel : BasePanel
{
    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            case "ResumeGameButton":
                Time.timeScale = 1;
                UIManager.GetInstance().HidePanel("Battle/LeftMenuPanel");
                GameManager.GetInstance().IsShowLeftMenu = false;
                GameManager.GetInstance().IsGamePaused = false;
                break;

            case "BackToMainMenuButton":
                Time.timeScale = 1;
                GameManager.GetInstance().Restart();
                UIManager.GetInstance().HideAllPanel(() => {
                    EventCenter.GetInstance().EventTrigger<SceneStateData>("场景切换", new SceneStateData(Enum_SceneState.MainMenu, ()=> {
                        MusicMgr.GetInstance().PlayBkMusic("BGM2");
                    }));
                });
                break;

            case "AudioButton":
                UIManager.GetInstance().ShowPanel<AudioManagerPanel>("MainMenu/AudioManagerPanel", E_UI_Layer.System);
                break;

            case "ExitGameButton":
                Application.Quit();
                break;
        }
    }

}
