using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter 
{
    private static EventCenter _Instance = null;
    private EventCenter()
    {
        Console.WriteLine("Created");
    }

    public static EventCenter Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new EventCenter();
            }
            return _Instance;
        }
    }

    /// <summary>
    /// 事件容器
    /// </summary>
    private Dictionary<string, UnityAction<Vector3>> eventDic = new Dictionary<string, UnityAction<Vector3>>();
    public void Init()
    {

    }

    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="name">事件名字</param>
    /// <param name="action">准备用来处理事件的委托函数</param>
    public void AddEventListener(string name, UnityAction<Vector3> action)
    {
        if (eventDic.ContainsKey(name))
        {
            eventDic[name] += action;
        }
        else
        {
            eventDic.Add(name, action);
        }
    }

    /// <summary>
    /// 事件触发
    /// </summary>
    /// <param name="name"></param>
    public void EventTrigger(string name, Vector3 info)
    {
        if (eventDic.ContainsKey(name))
        {
            eventDic[name].Invoke(info);
        }
    }

    /// <summary>
    /// 移除事件监听
    /// </summary>
    /// <param name="name">事件名字</param>
    /// <param name="action">委托函数</param>
    public void RemoveEventListener(string name, UnityAction<Vector3> action)
    {
        if (eventDic.ContainsKey(name))
            eventDic[name] -= action;
    }

    /// <summary>
    /// 清空事件中心
    /// </summary>
    public void Clear()
    {
        eventDic.Clear();
    }

}
