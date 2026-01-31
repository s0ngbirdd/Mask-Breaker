using UnityEngine;
using System.Collections.Generic;
public delegate void Callback();
public class GlobalEventBus : MonoBehaviour
{
    public static GlobalEventBus Instance;
    public Dictionary<string, List<Callback>> registeredEvents = new Dictionary<string, List<Callback>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void registerEvent(string eventName, Callback callback)
    {
        if (!registeredEvents.ContainsKey(eventName))
        {
            registeredEvents[eventName] = new List<Callback>();
        }
        registeredEvents[eventName].Add(callback);
    }

    public void triggerEvent(string eventName)
    {
        if (registeredEvents.ContainsKey(eventName))
        {
            foreach (var callback in registeredEvents[eventName])
            {
                callback.Invoke();
            }
        }
    }
}
