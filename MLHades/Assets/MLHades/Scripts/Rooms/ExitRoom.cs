using Roguelike;
using UnityEngine;

public class ExitRoom : MonoBehaviour, IInteract
{
    [SerializeField] private GameEvent exitRoomEvent;

    [SerializeField] bool interacted = false;

    public void Interact(GameObject origin)
    {
        if(!interacted)
        {
            exitRoomEvent.Raise();
            interacted = true;
        }
    }
}
