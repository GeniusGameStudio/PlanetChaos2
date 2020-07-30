using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : BasePanel
{
    public void SetWinText(string winText)
    {
        GetComponentInChildren<Text>().text = winText;
    }

    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            case "BackToMainMenuButton":
                GameManager.GetInstance().Restart();
                UIManager.GetInstance().HideAllPanel(() => {
                    EventCenter.GetInstance().EventTrigger<SceneStateData>("场景切换", new SceneStateData(Enum_SceneState.MainMenu, () => {
                        MusicMgr.GetInstance().PlayBkMusic("BGM2");
                    }));
                });
                break;
        }
    }
}
