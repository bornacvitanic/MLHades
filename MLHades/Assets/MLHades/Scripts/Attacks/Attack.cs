using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] protected int damage;
    [Header("Optional")]
    [SerializeField] protected List<GameObject> whitelistedTargets;
}
