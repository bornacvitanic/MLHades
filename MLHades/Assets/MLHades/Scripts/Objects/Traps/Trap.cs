using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    public abstract void Activate(GameObject triggerGameObject);


    [SerializeField] public List<GameObject> triggers;


    public void AddToList(GameObject trigger)
    {
        if(!triggers.Contains(trigger))
        {
            triggers.Add(trigger);
        }
    }
}
