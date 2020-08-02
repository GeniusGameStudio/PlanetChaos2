using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipBagItem : BagItem
{
    
    public string equipName;

    protected override void OnButtonClick()
    {
        base.OnButtonClick();
        Transform currentPlayer = TurnBaseMgr.GetInstance().CurrentPlayer();
        EquipMgr.GetInstance().Unload(currentPlayer);
        EquipMgr.GetInstance().Equip(equipName, currentPlayer);
        UIManager.GetInstance().HidePanel("Battle/ItemPanel");
        GameManager.GetInstance().IsShowItemPanel = false;
        GameManager.GetInstance().IsChooseItem = false;
        GameManager.GetInstance().IsUsedItem = true;
    }
}
