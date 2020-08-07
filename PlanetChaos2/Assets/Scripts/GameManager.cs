using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 场景状态的枚举
/// </summary>
public enum Enum_SceneState
{
    Start,
    MainMenu,
    Battle
}

public class SceneStateData
{
    public Enum_SceneState state;

    public UnityAction callBack;

    public SceneStateData(Enum_SceneState state, UnityAction callBack = null)
    {
        this.state = state;
        this.callBack = callBack;
    }
}

/// <summary>
/// 游戏管理器，主逻辑的入口
/// </summary>
public class GameManager : BaseManager<GameManager>
{
    public Enum_SceneState SceneState { get; set; }

    public bool IsShowLeftMenu { get; set; }

    public bool IsGamePaused { get; set; }

    public bool IsShowItemPanel { get; set; }

    public bool IsChooseItem { get; set; }

    public bool IsUsedItem { get; set; }

    public GameManager()
    {
        MonoMgr.GetInstance().AddUpdateListener(Update);
        InputMgr.GetInstance().StartOrEndCheck(true);
        EventCenter.GetInstance().AddEventListener<KeyCode>("某键按下", OnKeyDown);
        EventCenter.GetInstance().AddEventListener("回合数变更", OnTurnChanged);
        EventCenter.GetInstance().AddEventListener<BaseCharacterController>("玩家死亡", OnPlayerDead);
    }

    private void OnPlayerDead(BaseCharacterController baseCharacterController)
    {
        CheckWin();
    }

    private void OnTurnChanged()
    {
        CheckWin();
        IsUsedItem = false;
    }

    private void OnKeyDown(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.Escape:
                if(SceneState == Enum_SceneState.Battle)
                {
                    if (!IsShowLeftMenu)
                    {
                        Time.timeScale = 0;
                        UIManager.GetInstance().ShowPanel<LeftMenuPanel>("Battle/LeftMenuPanel");
                        UIManager.GetInstance().HidePanel("Battle/EscapeTipPanel");
                        IsShowLeftMenu = true;
                        IsGamePaused = true;
                    }
                    else
                    {
                        Time.timeScale = 1;
                        UIManager.GetInstance().HidePanel("Battle/LeftMenuPanel");
                        UIManager.GetInstance().ShowPanel<BasePanel>("Battle/EscapeTipPanel");
                        IsShowLeftMenu = false;
                        IsGamePaused = false;
                    }
                }
                break;

            case KeyCode.B:
                if(SceneState == Enum_SceneState.Battle)
                {
                    if (!IsGamePaused && !IsUsedItem)
                    {
                        if (!IsShowItemPanel)
                        {
                            UIManager.GetInstance().ShowPanel<ItemPanel>("Battle/ItemPanel");
                            UIManager.GetInstance().HidePanel("Battle/ItemTipPanel");
                            IsChooseItem = true;
                            IsShowItemPanel = true;
                            Cursor.visible = true;
                        }
                        else
                        {
                            UIManager.GetInstance().HidePanel("Battle/ItemPanel");
                            UIManager.GetInstance().ShowPanel<BasePanel>("Battle/ItemTipPanel");
                            IsChooseItem = false;
                            IsShowItemPanel = false;
                        }
                    }
                }
                break;
        }
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
            EventCenter.GetInstance().EventTrigger<SceneStateData>("场景切换", new SceneStateData(Enum_SceneState.MainMenu));
        }
    }

    public void Restart()
    {
        CharacterMgr.GetInstance().characterTransforms.Clear();
        IsShowLeftMenu = false;
        IsShowItemPanel = false;
        IsGamePaused = false;
        IsUsedItem = false;
    }

    public void Init()
    {
        Debug.Log("Init");
        SceneState = Enum_SceneState.Start;
        MonoMgr.GetInstance().AddUpdateListener(Update);

        EventCenter.GetInstance().AddEventListener<SceneStateData>("场景切换", OnSceneStateChanged);


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
        Timer.Invoke(() => {
            GameObject characterInitPoints = GameObject.Find("CharacterInitPoints");
            List<int> pointIndexs = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            List<int> randomPointIndexs = Shuffle<int>(pointIndexs);
            CharacterMgr.GetInstance().InstantiateCharacters(characterInitPoints, randomPointIndexs, () => {
                UIManager.GetInstance().ShowPanel<TimerPanel>("Battle/TimerPanel", E_UI_Layer.Top);
                UIManager.GetInstance().ShowPanel<BasePanel>("Battle/ItemTipPanel", E_UI_Layer.Bot);
                UIManager.GetInstance().ShowPanel<BasePanel>("Battle/EscapeTipPanel", E_UI_Layer.Bot);
                TurnBaseMgr.GetInstance().Start();
            });
        }, 0.2f);
    }

    /// <summary>
    /// 随机打乱list元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="original"></param>
    /// <returns></returns>
    public List<T> Shuffle<T>(List<T> original)
    {
        System.Random randomNum = new System.Random();
        int index = 0;
        T temp;
        for (int i = 0; i < original.Count; i++)
        {
            index = randomNum.Next(0, original.Count - 1);
            if (index != i)
            {
                temp = original[i];
                original[i] = original[index];
                original[index] = temp;
            }
        }
        return original;
    }

    private void OnSceneStateChanged(SceneStateData data)
    {
        if (data.state == SceneState)
            return;

        switch (data.state)
        {
            case Enum_SceneState.Start:
                data.callBack += OnStartSceneLoaded;
                ScenesMgr.GetInstance().LoadScene("StartScene", data.callBack);
                break;

            case Enum_SceneState.MainMenu:
                data.callBack += OnMainMenuSceneLoaded;
                ScenesMgr.GetInstance().LoadScene("MainMenuScene", data.callBack);
                break;

            case Enum_SceneState.Battle:
                data.callBack += OnBattleSceneLoaded;
                ScenesMgr.GetInstance().LoadScene("BattleScene", data.callBack);
                break;

            default:
                Debug.LogError("SceneState Error");
                break;
        }
    }

    private void CheckWin()
    {
        Dictionary<int, int> aliveDic = new Dictionary<int, int>();
        int teamID = -1;
        foreach(var character in CharacterMgr.GetInstance().characterTransforms)
        {
            BaseCharacterController baseCharacterController = character.GetComponent<BaseCharacterController>();
            if (baseCharacterController.isAlive)
            {
                if (!aliveDic.ContainsKey(baseCharacterController.CharacterData.TeamID))
                {
                    teamID = baseCharacterController.CharacterData.TeamID;
                    aliveDic.Add(teamID, 0);
                }
            }
        }
        if(aliveDic.Keys.Count == 1)
        {
            Debug.Log("队伍" + teamID + "Win");
            //Time.timeScale = 0;
            IsGamePaused = true;
            TurnBaseMgr.GetInstance().IsPauseTimer = true;
            UIManager.GetInstance().ShowPanel<WinPanel>("Battle/WinPanel", E_UI_Layer.System, (panel) => {
                panel.SetWinText("队伍" + (teamID + 1).ToString() + "获胜！");
            });
        }
    }

}
