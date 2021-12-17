using UnityEngine;

public class DestroyOnDeath : Death
{
    public override void OnDeath() => Destroy(gameObject);
}
