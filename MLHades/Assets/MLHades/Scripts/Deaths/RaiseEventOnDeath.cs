using UnityEngine;

public class RaiseEventOnDeath : Death
{
    [SerializeField] private GameEvent targetEvent;

    public override void OnDeath() => targetEvent.Raise();
}
