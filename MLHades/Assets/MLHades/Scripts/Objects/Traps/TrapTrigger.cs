using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrapTrigger : MonoBehaviour
{
    [SerializeField] public List<GameObject> traps;

    public abstract void Trigger();
    
    public bool isOnTrapList(GameObject trap)
    {
        return traps.Contains(trap);
    }

    private void OnValidate()
    {
        traps.ForEach(trap =>
        {
            if(trap != null)
            {
                trap.GetComponent<Trap>().AddToList(gameObject);
            }
        });
    }
}
