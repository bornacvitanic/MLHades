using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGameObjectOnDeath : Death
{
    [SerializeField] private GameObject target;
    public override void OnDeath() => target.SetActive(true);
}
