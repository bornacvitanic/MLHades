using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterDelay : MonoBehaviour
{
    [SerializeField] private float delay;
    private void OnEnable() {
        Invoke("DisableGameObject", delay);
    }

    private void DisableGameObject(){
        gameObject.SetActive(false);
    }
}
