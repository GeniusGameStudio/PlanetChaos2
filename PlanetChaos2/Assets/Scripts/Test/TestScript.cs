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
        EquipMgr.GetInstance().Equip("回血枪", GameObject.Find("Alice").transform);
        //EquipMgr.GetInstance().Unload(GameObject.Find("Alice").transform);    //卸装!
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
