using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : BasePanel
{
    private Button btnMainSkill;

    private void Start()
    {
        btnMainSkill = GetControl<Button>("MainSkill_Button");
        OnTurnChanged();
        EventCenter.GetInstance().AddEventListener("回合数变更", OnTurnChanged);
    }

    private void OnTurnChanged()
    {
        CharacterData currentPlayerData = TurnBaseMgr.GetInstance().CurrentPlayer().GetComponent<BaseCharacterController>().CharacterData;

        GunBagItem gunBagItem;

        switch (currentPlayerData.Character)
        {
            case Character.Alice:
                btnMainSkill.image.sprite = ResMgr.GetInstance().Load<Sprite>("UI/Weapon/Alice_hpGun");
                gunBagItem = btnMainSkill.GetComponent<GunBagItem>();
                gunBagItem.info = "回血枪，可以发射一颗有<color=#94FA4D>治愈</color>效果的子弹。\n\n<color=#94FA4D>治愈</color>效果: 可以让角色回复生命值甚至从濒死状态转为存活状态。";
                gunBagItem.gunName = "回血枪";
                break;

            case Character.Bobi:
                btnMainSkill.image.sprite = ResMgr.GetInstance().Load<Sprite>("UI/Weapon/Bobi_fireGun");
                gunBagItem = btnMainSkill.GetComponent<GunBagItem>();
                gunBagItem.info = "火焰枪，可以发射一颗有<color=#DE1800>燃烧</color>效果的子弹。\n\n<color=#DE1800>燃烧</color>效果: 可以对角色或者物体造成燃烧伤害。";
                gunBagItem.gunName = "火焰枪";
                break;
        }
    }

    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener("回合数变更", OnTurnChanged);
    }
}
