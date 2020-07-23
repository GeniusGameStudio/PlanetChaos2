using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMgr : BaseManager<CharacterMgr>
{
    /// <summary>
    /// 控制player
    /// </summary>
    /// <param name="player"></param>
    public void Control(Transform player)
    {
        IControl control = player.GetComponent<IControl>();
        if (control != null)
        {
            control.Control();
        }
    }

    /// <summary>
    /// 取消对player的控制
    /// </summary>
    /// <param name="player"></param>
    public void CancelControl(Transform player)
    {
        IControl control = player.GetComponent<IControl>();
        if (control != null)
        {
            control.CancleControl();
        }
    }
}
