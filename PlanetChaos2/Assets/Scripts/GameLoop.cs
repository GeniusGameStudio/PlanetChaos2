using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏循环，用于初始化游戏管理器
/// </summary>
public class GameLoop : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        GameManager.GetInstance().Init();           //游戏管理器，初始化
    }

}
