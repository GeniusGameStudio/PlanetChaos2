using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 选择英雄面板
/// </summary>
public class ChooseHeroPanel : BasePanel
{
    private Button btnStartGame;
    private Button btnAddPlayer;
    private Button btnAddAI;
    private ScrollRect scrollRect;
    private Transform scrollViewContent;

    private void Start()
    {
        btnStartGame = GetControl<Button>("StartGameButton");
        btnStartGame.interactable = false;
        btnAddPlayer = GetControl<Button>("AddPlayerButton");
        btnAddAI = GetControl<Button>("AddAIButton");
        btnAddAI.interactable = false;
        scrollRect = GetControl<ScrollRect>("Scroll View");
        scrollViewContent = scrollRect.transform.Find("Viewport").Find("Content");

        EventCenter.GetInstance().AddEventListener("玩家数变更", OnPlayerCountChanged);
        EventCenter.GetInstance().AddEventListener<int>("队伍选择更改", OnTeamChooseChanged);
    }

    /// <summary>
    /// 当玩家数变动时的回调
    /// </summary>
    private void OnPlayerCountChanged()
    {
        int playerCount = scrollViewContent.childCount;
        Debug.Log("玩家数变更为: " + playerCount);
        if(playerCount >= 2)
        {
            if (CheckTeamMatch())
            {
                btnStartGame.interactable = true;
            }
            else
            {
                btnStartGame.interactable = false;
            }
        }
        else
        {
            btnStartGame.interactable = false;
        }
        if(playerCount >= 6)
        {
            btnAddPlayer.interactable = false;
        }
        else
        {
            btnAddPlayer.interactable = true;
        }
    }

    /// <summary>
    /// 对所有按钮按下的监听
    /// </summary>
    /// <param name="btnName"></param>
    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            case "AddPlayerButton":
                GameObject characterItemPanel = ResMgr.GetInstance().Load<GameObject>("UI/MainMenu/CharacterItemPanel");
                characterItemPanel.transform.SetParent(scrollViewContent);
                EventCenter.GetInstance().EventTrigger("玩家数变更");
                break;

            case "StartGameButton":
                CharacterItemPanel[] characterItemPanels = scrollViewContent.GetComponentsInChildren<CharacterItemPanel>();
                List<CharacterData> characterDatas = new List<CharacterData>();
                foreach(var item in characterItemPanels)
                {
                    characterDatas.Add(item.CharacterData);
                }
                CharacterMgr.GetInstance().SetCharacterDatas(characterDatas);
                UIManager.GetInstance().HideAllPanel(()=> { 
                    EventCenter.GetInstance().EventTrigger<Enum_SceneState>("场景切换", Enum_SceneState.Battle);
                });
                break;

            case "BackToMainMenuButton":
                UIManager.GetInstance().HidePanel("MainMenu/ChooseHeroPanel");
                break;

        }
    }

    /// <summary>
    /// 当队伍选择变化时
    /// </summary>
    /// <param name="value"></param>
    private void OnTeamChooseChanged(int value)
    {
        if (CheckTeamMatch())
        {
            btnStartGame.interactable = true;
        }
        else
        {
            btnStartGame.interactable = false;
        }
    }

    /// <summary>
    /// 检查是否至少2支队伍
    /// </summary>
    private bool CheckTeamMatch()
    {
        bool isMatch = false;
        CharacterItemPanel[] characterItemPanels = scrollViewContent.GetComponentsInChildren<CharacterItemPanel>();
        Debug.Log(characterItemPanels.Length);
        if(characterItemPanels != null)
        {
            var firstItem = characterItemPanels[0];
            for(int i = 1; i < characterItemPanels.Length; i++)
            {
                Debug.Log(firstItem.GetTeamID() + ", " + characterItemPanels[i].GetTeamID());
                if(firstItem.GetTeamID() != characterItemPanels[i].GetTeamID())
                {
                    isMatch = true;
                    break;
                }

            }
        }
        return isMatch;
    }

    /// <summary>
    /// 摧毁时，注销事件监听
    /// </summary>
    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener("玩家数变更", OnPlayerCountChanged);
        EventCenter.GetInstance().RemoveEventListener<int>("队伍选择更改", OnTeamChooseChanged);
    }
}
