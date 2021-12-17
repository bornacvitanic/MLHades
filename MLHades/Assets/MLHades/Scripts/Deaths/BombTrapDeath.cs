using UnityEngine;
using System.Collections.Generic;

public class BombTrapDeath : Death
{
    [Header("Objects")]
    [SerializeField] private GameObject rangeIndicator;
    [SerializeField] private List<GameObject> partileSystemPrefabs;
    [SerializeField] private GameObject explosionCollider;

    private bool triggered = false;
    private float explosionDelay = 3f;
    private float lastTriggerTime;
    private float blinkingDelay = 0.25f;
    private float lastBlinkTIme;
    private bool isEmissionOn = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        rangeIndicator.SetActive(false);
        explosionCollider.SetActive(false);
    }

    public override void OnDeath()
    {
        triggered = true;
        lastTriggerTime = Time.time;
        lastBlinkTIme = Time.time;
        rangeIndicator.SetActive(true);
    }

    private void FixedUpdate()
    {
        if(triggered && Time.time - lastTriggerTime >= explosionDelay)
        {
            Explode();
            triggered = false;
        }
        else if(triggered && Time.time - lastBlinkTIme > blinkingDelay)
        {
            ToggleBlink();
            lastBlinkTIme = Time.time;
        }
    }

    private void Explode()
    {
        ActivateExplosionCollider();
        Invoke("DeactivateExplosionCollider", 0.5f);

        if(partileSystemPrefabs!=null && partileSystemPrefabs.Count>0){
            foreach (var prefab in partileSystemPrefabs){
                var particleSystem = ObjectPooler.SharedInstance.Instantiate(prefab, transform.position + new Vector3(0f,0.5f,0f), Quaternion.identity);
                particleSystem.transform.parent = GameObject.FindGameObjectWithTag("dynamic").transform;
            }
            
        }
    }

    private void ActivateExplosionCollider()
    {
        explosionCollider.SetActive(true);
        TurnEmissionOn();
    }

    private void DeactivateExplosionCollider()
    {
        explosionCollider.SetActive(false);
        TurnEmissionOff();
        rangeIndicator.SetActive(false);
        gameObject.SetActive(false);
    }

    private void ToggleBlink()
    {
        if(isEmissionOn)
        {
            TurnEmissionOff();
        }
        else
        {
            TurnEmissionOn();
        }
    }

    private void TurnEmissionOn()
    {
        rangeIndicator.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        isEmissionOn = true;
    }

    private void TurnEmissionOff()
    {
        rangeIndicator.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        isEmissionOn = false;
    }
}
