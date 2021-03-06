﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 角色（英雄）的枚举
/// </summary>
public enum Character
{
    Alice = 0,
    Bobi = 1
}

/// <summary>
/// 角色（英雄）在选择时的属性
/// </summary>
public class CharacterData
{
    public CharacterData(string name, Character character, int teamID)
    {
        Name = name;
        Character = character;
        TeamID = teamID;
    }
    public string Name { get; set; }

    public Character Character { get; set; }

    public int TeamID { get; set; }
}
/// <summary>
/// 角色Item面板类
/// </summary>
public class CharacterItemPanel : BasePanel, IPointerEnterHandler, IPointerExitHandler
{
    private Dictionary<int, string> avatarDic = new Dictionary<int, string>();

    private Text playerNameText;

    private Image heroAvatarImage;

    private Transform btnRemoveTransform;

    private Dropdown heroDropdown;

    private Dropdown teamDropdown;

    public CharacterData CharacterData { get; set; }

    private void InitAvatarDic()
    {
        avatarDic.Add(0, "UI/Character/Alice");
        avatarDic.Add(1, "UI/Character/Bobi");
    }

    private new void Awake()
    {
        base.Awake();
        
        playerNameText = GetControl<Text>("PlayerNameText");
        playerNameText.text = "Default";
        heroAvatarImage = GetControl<Image>("AvatarImage");

        heroDropdown = GetControl<Dropdown>("HeroDropdown");

        teamDropdown = GetControl<Dropdown>("TeamDropdown");
        CharacterData = new CharacterData(playerNameText.text, (Character)heroDropdown.value, teamDropdown.value);
        btnRemoveTransform = GetControl<Button>("RemoveButton").transform;
        btnRemoveTransform.gameObject.SetActive(false);

        InitAvatarDic();
    }

    private void Start()
    {
        ShowMe();
    }

    protected override void OnValueChanged(string objName, int value)
    {
        base.OnValueChanged(objName, value);
        switch (objName)
        {
            case "HeroDropdown":
                Sprite sprite = ResMgr.GetInstance().Load<Sprite>(avatarDic[value]);
                heroAvatarImage.sprite = sprite;
                CharacterData.Character = (Character)value;
                break;

            case "TeamDropdown":
                CharacterData.TeamID = value;
                EventCenter.GetInstance().EventTrigger<int>("队伍选择更改", value);
                break;
        }
    }

    protected override void OnValueChanged(string objName, string value)
    {
        base.OnValueChanged(objName, value);
        switch (objName)
        {
            case "PlayerNameInput":
                CharacterData.Name = value;
                break;
        }
    }

    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            case "RemoveButton":
                HideMe(() =>
                {
                    DestroyImmediate(gameObject);
                    EventCenter.GetInstance().EventTrigger("玩家数变更");
                });
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        btnRemoveTransform.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        btnRemoveTransform.gameObject.SetActive(false);
    }

    public int GetTeamID()
    {
        return CharacterData.TeamID;
    }
}
