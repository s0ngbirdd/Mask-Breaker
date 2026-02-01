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

    public void unregisterEvent(string eventName, Callback callback)
    {
        if (registeredEvents.ContainsKey(eventName))
        {
            registeredEvents[eventName].Remove(callback);
            if (registeredEvents[eventName].Count == 0)
            {
                registeredEvents.Remove(eventName);
            }
        }
    }

    public void triggerEvent(string eventName, object eventData = null)
    {
        if (registeredEvents.ContainsKey(eventName))
        {
            foreach (var callback in registeredEvents[eventName])
            {
                callback.Invoke();
            }
        }
    }

    void OnSceneLoaded()
    {
        ClearAllEvents();
    }

    public void ClearAllEvents()
    {
        registeredEvents.Clear();
    }
}
