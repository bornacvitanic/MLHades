using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnChildrenToPoolOnDestroy : MonoBehaviour
{
    private void OnDestroy()
    {
        foreach(ReturnToPool r in GetComponentsInChildren<ReturnToPool>())
        {
            r.gameObject.SetActive(false);
        }
    }
}
