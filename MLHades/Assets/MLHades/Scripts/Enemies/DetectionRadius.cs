using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class DetectionRadius : MonoBehaviour
{
    [SerializeField] private GameObject intruder;
    [SerializeField] private GameObject alertSign;

    public event Action OnIntruderDetectedEvent = delegate { };

    private void Awake()
    {
        if(alertSign != null)
        {
            alertSign.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag(intruder.tag))
        {
            return;
        }

        OnIntruderDetectedEvent.Invoke();
        gameObject.SetActive(false);
        if(alertSign != null)
        {
            FlashAlertSign();
        }
    }

    private void FlashAlertSign()
    {
        EnableAlertSign();
        Invoke("DisableAlertSign", 0.5f);
    }

    private void EnableAlertSign() => alertSign.SetActive(true);

    private void DisableAlertSign() => alertSign.SetActive(false);
}
