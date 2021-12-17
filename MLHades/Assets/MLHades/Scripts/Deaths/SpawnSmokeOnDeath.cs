using UnityEngine;

public class SpawnSmokeOnDeath : Death
{
    [SerializeField] private GameObject smokePartileSystemPrefab;

    public override void OnDeath() => SpawnSmoke();

    private void SpawnSmoke()
    {
        if(GetComponent<Health>().GetHealth() != 0)
        {
            return;
        }
        var particleSystem = ObjectPooler.SharedInstance.Instantiate(smokePartileSystemPrefab, transform.position + new Vector3(0f,0.5f,0f), Quaternion.identity);
        particleSystem.transform.parent = GameObject.FindGameObjectWithTag("dynamic").transform;
    }
}
