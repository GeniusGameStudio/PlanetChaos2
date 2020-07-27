using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    /// <summary>
    /// 注册执行方法，在timer秒后执行acition
    /// </summary>
    public static void Invoke(Action action, float timer)
    {
        MonoMgr.GetInstance().StartCoroutine(DoAction(action, timer));
    }

    /// <summary>
    /// 在timer秒后执行方法
    /// </summary>
    private static IEnumerator DoAction(Action action, float timer)
    {
        yield return new WaitForSeconds(timer);
        action.Invoke();
    }

    /// <summary>
    ///     注册重复执行方法，在timer秒后执行acition并在每duration后再次执行,如果要停下来，返回值需要记录下。
    /// </summary>
    public static IEnumerator InvokeRepeat(Action action, float timer, float duration)
    {
        var temp = DoRepeat(action, timer, duration);
        MonoMgr.GetInstance().StartCoroutine(temp);
        return temp;
    }

    /// <summary>
    ///     执行重复调用
    /// </summary>
    private static IEnumerator DoRepeat(Action action, float timer, float duration)
    {
        yield return new WaitForSeconds(timer);
        while (true)
        {
            action.Invoke();
            yield return new WaitForSeconds(duration);
        }
    }

    /// <summary>
    ///     停止重复执行的携程
    /// </summary>
    public static void StopRepeat(IEnumerator iEnumerator)
    {
        MonoMgr.GetInstance().StopCoroutine(iEnumerator);
    }
}
