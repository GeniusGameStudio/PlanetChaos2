using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.GetInstance().ToString();
        InputMgr.GetInstance().StartOrEndCheck(true);



        EquipMgr.GetInstance().Equip("火箭筒", GameObject.Find("Alice").transform);  //给Alice装备火箭筒
        //EquipMgr.GetInstance().Unload(GameObject.Find("Alice").transform);        //给Alice卸下当前装备

        CharacterMgr.GetInstance().Control(GameObject.Find("Alice").transform);     //控制Alice!

        //EquipMgr.GetInstance().Equip("回血枪", GameObject.Find("Alice").transform);//给Alice装备回血枪
        //EquipMgr.GetInstance().Unload(GameObject.Find("Alice").transform);    //给Alice卸下当前装备

        MusicMgr.GetInstance().PlayBkMusic("BGM1");
        MusicMgr.GetInstance().ChangeBKValue(0.2f);
        MusicMgr.GetInstance().ChangeSoundValue(0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(50, 50, 300, 100), "重新加载测试关卡"))
        {
            MusicMgr.GetInstance().ClearSoundList();
            ScenesMgr.GetInstance().LoadSceneAsyn("SampleScene", ()=>{  });
        }

        if (GUI.Button(new Rect(50, 160, 300, 100), "退出游戏"))
        {
            Application.Quit();
        }

    }
}
