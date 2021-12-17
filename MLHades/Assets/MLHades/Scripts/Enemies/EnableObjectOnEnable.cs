using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectOnEnable : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private void OnEnable()
    {
        target.SetActive(true);
    }
}
