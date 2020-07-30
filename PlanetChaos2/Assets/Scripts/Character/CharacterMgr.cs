using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 角色管理器，用于对所有角色实例进行管理
/// </summary>
public class CharacterMgr : BaseManager<CharacterMgr>
{
    public List<Transform> characterTransforms = new List<Transform>();

    private List<CharacterData> characterDatas = new List<CharacterData>();

    private Dictionary<Character, string> characterResDic = new Dictionary<Character, string>();

    private List<Color> teamColors = new List<Color>();

    private void InitCharacterResDic()
    {
        characterResDic.Add(Character.Alice, "Objects/Alice");
        characterResDic.Add(Character.Bobi, "Objects/Bobi");
    }

    private void InitTeamColors()
    {
        teamColors.Add(new Color(98f/255f, 98f/255f, 1f));
        teamColors.Add(new Color(1f, 98f/255f, 98/255f));
        teamColors.Add(new Color(1f, 1f, 98/255f));
        teamColors.Add(new Color(98f/255f, 1f, 98/255f));
    }

    public CharacterMgr()
    {
        InitCharacterResDic();
        InitTeamColors();
    }

    public void SetCharacterDatas(List<CharacterData> characterDatas)
    {
        this.characterDatas = characterDatas;
    }

    private void InstantiateCharacter(CharacterData data, Transform position, UnityAction<GameObject> callBack)
    {
        if (characterResDic.ContainsKey(data.Character))
        {
            ResMgr.GetInstance().LoadAsync<GameObject>(characterResDic[data.Character], (obj) => {
                obj.transform.position = position.position;
                obj.GetComponent<BaseCharacterController>().CharacterData = data;
                CharacterUI characterUI = obj.GetComponent<CharacterUI>();
                characterUI.SetPlayerName(data.Name);
                characterUI.SetColor(teamColors[data.TeamID]);
                characterTransforms.Add(obj.transform);
                callBack(obj);
            });
        }
    }

    public void InstantiateCharacters(GameObject initPointObj, List<int> initPoints, UnityAction callBack)
    {
        int instantiatedCount = 0;
        int positionIndex = 0;
        if(characterDatas != null)
        {
            foreach(var data in characterDatas)
            {
                Transform position = initPointObj.transform.GetChild(initPoints[positionIndex++]);
                InstantiateCharacter(data, position, (obj) => {
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
