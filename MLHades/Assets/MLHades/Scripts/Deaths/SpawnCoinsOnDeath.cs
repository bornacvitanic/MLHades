using UnityEngine;

public class SpawnCoinsOnDeath : Death
{
    [SerializeField] private GameObject coinPartileSystemPrefab;
    [SerializeField] private int amount;
    [SerializeField] private IntValue coins;
    [SerializeField] private GameEvent coinAmountChangedEvent;

    public override void OnDeath() => SpawnCoins();

    private void SpawnCoins()
    {
        if(GetComponent<Health>().GetHealth() != 0)
        {
            return;
        }
        coins.value += amount;
        coinAmountChangedEvent.Raise();
        var particleSystem = ObjectPooler.SharedInstance.Instantiate(coinPartileSystemPrefab, transform.position, Quaternion.identity);
        particleSystem.GetComponent<ParticleSystem>().emission.SetBurst(0, new ParticleSystem.Burst(0f, (short)amount, (short)amount, 1, 0.001f));
        particleSystem.transform.parent = GameObject.FindGameObjectWithTag("dynamic").transform;
    }
}
