using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceManager : Singleton<ServiceManager>
{
    private List<object> _service = new();

    public T Get<T>(Func<T> generator = null) where T : class
    {
        generator ??= DefaultConstruct<T>;
        var srv = _service.Find(x => x is T);
        if (srv == null)
        {
            srv = generator();
            _service.Add(srv);
        }
        return srv as T;
    }

    public static T DefaultConstruct<T>() where T : class
    {
        return typeof(T).GetConstructor(new Type[] { }).Invoke(new object[] { }) as T;
    }

    public static T DefaultComponent<T>() where T : Component
    {
        var t = GameObject.FindObjectOfType<T>();
        if (t) return t;

        t = new GameObject(typeof(T).Name).AddComponent<T>();
        GameObject.DontDestroyOnLoad(t);
        return t;
    }
} 