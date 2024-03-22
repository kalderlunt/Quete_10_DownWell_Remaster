using System.Collections.Generic;
using UnityEngine;

public abstract class EventSystem : MonoBehaviour
{
    protected Dictionary<GameEventType, EventSystem> events = new();

    private void Start()
    {
        foreach (GameEventType eventType in System.Enum.GetValues(typeof(GameEventType)))
        {
            events.Add(eventType, null);
        }
    }

    public void AddEvent(GameEventType eventType, EventSystem gameEvent)
    {
        events[eventType] = gameEvent;
    }

    public void PlayEvent(GameEventType eventType)
    {
        if (events.ContainsKey(eventType))
        {
            events[eventType].Execute();
        }
        else
        {
            Debug.LogError("ERROR #0100 : Event not found for type: " + eventType);
        }
    }

    public abstract void Execute();
}