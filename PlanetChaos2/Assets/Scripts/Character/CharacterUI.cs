using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public Transform turnImage;

    public Text playerNameText;

    public Image panelImage;

    public Text playerHP_Text;

    private void Start()
    {
        EventCenter.GetInstance().AddEventListener<BaseCharacterController>("血量变化", OnHP_Changed);
    }

    private void OnHP_Changed(BaseCharacterController baseCharacterController)
    {
        if(baseCharacterController == GetComponent<BaseCharacterController>())
            playerHP_Text.text = baseCharacterController.CurrentHP.ToString();
    }

    public void SetColor(Color color)
    {
        SetPanelColor(color);
        SetPlayerHPTextColor(color);
        SetTurnImageColor(color);
    }

    public void SetPlayerHPTextColor(Color color)
    {
        playerHP_Text.color = color;
    }

    public void SetPanelColor(Color color)
    {
        panelImage.color = color;
    }

    public void SetPlayerName(string name)
    {
        playerNameText.text = name;
    }

    public void SetTurnImageColor(Color color)
    {
        turnImage.GetComponent<Image>().color = color;
    }

    public void SetTurnImageVisible(bool isVisible)
    {
        turnImage.gameObject.SetActive(isVisible);
    }

    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener<BaseCharacterController>("血量变化", OnHP_Changed);
    }
}
