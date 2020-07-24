using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        GameManager.GetInstance().Init();           //游戏管理器，初始化
    }

    void Update()
    {
        
    }
}
