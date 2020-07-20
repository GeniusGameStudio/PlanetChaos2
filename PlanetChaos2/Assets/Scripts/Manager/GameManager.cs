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
        Init();
    }

    private void Init()
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
    }

    private void OnMainMenuSceneLoaded()
    {
        Debug.Log("主菜单场景加载完成");
        SceneState = Enum_SceneState.MainMenu;
    }

    private void OnBattleSceneLoaded()
    {
        Debug.Log("战斗场景加载完成");
        SceneState = Enum_SceneState.Battle;
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

    private void Update()
    {

    }

}
