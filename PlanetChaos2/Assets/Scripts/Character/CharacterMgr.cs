using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMgr : BaseManager<CharacterMgr>
{
    public List<Transform> characterTransforms = new List<Transform>();

    private List<CharacterData> characterDatas = new List<CharacterData>();

    private Dictionary<Character, string> characterResDic = new Dictionary<Character, string>();

    private void InitCharacterResDic()
    {
        characterResDic.Add(Character.Alice, "Objects/Alice");
        characterResDic.Add(Character.Bobi, "Objects/Bobi");
    }

    public CharacterMgr()
    {
        InitCharacterResDic();
    }

    public void SetCharacterDatas(List<CharacterData> characterDatas)
    {
        this.characterDatas = characterDatas;
    }

    private void InstantiateCharacter(CharacterData data, UnityAction<GameObject> callBack)
    {
        if (characterResDic.ContainsKey(data.Character))
        {
            ResMgr.GetInstance().LoadAsync<GameObject>(characterResDic[data.Character], (obj) => {
                obj.GetComponent<BaseCharacterController>().CharacterData = data;
                characterTransforms.Add(obj.transform);
                callBack(obj);
            });
        }
    }

    public void InstantiateCharacters(UnityAction callBack)
    {
        int instantiatedCount = 0;
        if(characterDatas != null)
        {
            foreach(var data in characterDatas)
            {
                InstantiateCharacter(data, (obj) => {
                    instantiatedCount++;
                    if (instantiatedCount == characterDatas.Count)
                    {
                        callBack();
                    }
                });
            }
        }
    }

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
