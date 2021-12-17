using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumbersUI : MonoBehaviour
{
    [SerializeField] private GameObject damageNumberPrefab;
    [SerializeField] private Health healthComponent;
    private int health = -1;
    private Queue<GameObject> damageNumberInstances;

    private void Awake()
    {
        healthComponent.OnHealthChangedEvent += ShowDamageNumber;
        damageNumberInstances = new Queue<GameObject>();
    }

    private void OnEnable()
    {
        health = healthComponent.GetHealth();
    }
    
    public void ShowDamageNumber()
    {
        var healthChange = health - healthComponent.GetHealth();
        if(healthChange>0)
        {
            var damageNumberInstance = Instantiate(damageNumberPrefab);
            damageNumberInstance.transform.SetParent(transform, false);
            damageNumberInstance.GetComponent<Text>().text = healthChange.ToString();
            damageNumberInstances.Enqueue(damageNumberInstance);
            Invoke("ResetDamageNumber", 2f);
        }
        health = healthComponent.GetHealth();
    }

    private void ResetDamageNumber()
    {
        Destroy(damageNumberInstances.Dequeue());
    }

    private void OnDestroy()
    {
        healthComponent.OnHealthChangedEvent -= ShowDamageNumber;
    }
}
