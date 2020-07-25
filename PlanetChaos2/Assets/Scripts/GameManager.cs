using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enum_SceneState
{
    Start,
    MainMenu,
    Battle
}

public class GameManager : BaseManager<GameManager>
{
    public Enum_SceneState SceneState { get; set; }

    public GameManager()
    {
        MonoMgr.GetInstance().AddUpdateListener(Update);
    }

    private void Update()
    {
        switch (SceneState)
        {
            case Enum_SceneState.Start:
                StartSceneUpdate();
                break;

            case Enum_SceneState.MainMenu:

                break;

            case Enum_SceneState.Battle:

                break;

            default:
                Debug.LogError("没有该场景状态!");
                break;
        }
    }

    private void StartSceneUpdate()
    {
        if (Input.anyKey)
        {
            EventCenter.GetInstance().EventTrigger<Enum_SceneState>("场景切换", Enum_SceneState.MainMenu);
        }
    }

    public void LoadScene(string sceneName)
    {

    }

    public void Init()
    {
        Debug.Log("Init");
        SceneState = Enum_SceneState.Start;
        MonoMgr.GetInstance().AddUpdateListener(Update);

        EventCenter.GetInstance().AddEventListener<Enum_SceneState>("场景切换",OnSceneStateChanged);

        OnStartSceneLoaded();
    }

    private void OnStartSceneLoaded()
    {
        Debug.Log("开始场景加载完成");
        SceneState = Enum_SceneState.Start;
        MusicMgr.GetInstance().PlayBkMusic("BGM2");
        
    }

    private void OnMainMenuSceneLoaded()
    {
        Debug.Log("主菜单场景加载完成");
        SceneState = Enum_SceneState.MainMenu;
        UIManager.GetInstance().ShowPanel<MainMenuPanel>("MainMenu/MainMenuPanel");
        UIManager.GetInstance().ShowPanel<BasePanel>("MainMenu/CopyrightPanel", E_UI_Layer.Bot);
        UIManager.GetInstance().ShowPanel<SettingPanel>("MainMenu/SettingPanel");
    }

    private void OnBattleSceneLoaded()
    {
        Debug.Log("战斗场景加载完成");
        SceneState = Enum_SceneState.Battle;
        MusicMgr.GetInstance().PlayBkMusic("BGM1");
    }

    private void OnSceneStateChanged(Enum_SceneState sceneState)
    {
        if (sceneState == SceneState)
            return;

        switch (sceneState)
        {
            case Enum_SceneState.Start:
                ScenesMgr.GetInstance().LoadScene("StartScene", OnStartSceneLoaded);
                break;

            case Enum_SceneState.MainMenu:
                ScenesMgr.GetInstance().LoadScene("MainMenuScene", OnMainMenuSceneLoaded);
                break;

            case Enum_SceneState.Battle:
                ScenesMgr.GetInstance().LoadScene("BattleScene", OnBattleSceneLoaded);
                break;

            default:
                Debug.LogError("SceneState Error");
                break;
        }
    }

    private void StartGame()
    {
        
    }

}
