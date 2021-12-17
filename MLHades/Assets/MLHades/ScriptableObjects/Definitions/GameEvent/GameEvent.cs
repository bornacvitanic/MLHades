using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<GameEventListener> eventListeners =
        new List<GameEventListener>();

    public void Raise()
    {
        for(int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        if(!eventListeners.Contains(listener))
        {
            eventListeners.Add(listener);
        }
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if(eventListeners.Contains(listener))
        {
            eventListeners.Remove(listener);
        }
    }

    public List<GameObject> getEventListeners()
    {
        List<GameObject> gameObjects = new List<GameObject>();
        foreach (GameEventListener l in eventListeners)
        {
            gameObjects.Add(l.gameObject);
        }
        return gameObjects;
    }
}
