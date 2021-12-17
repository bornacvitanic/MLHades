using UnityEngine;

public class CoinReward : Reward
{
    [Header("Values")]
    [SerializeField] private int amount;
    [Header("Objects")]
    [SerializeField] private GameObject coinPartileSystemPrefab;
    [Header("Scriptable Objects")]
    [SerializeField] private IntValue coins;
    [Header("Events")]
    [SerializeField] private GameEvent coinAmountChangedEvent;

    public override void AdditionalEffect(GameObject origin)
    {
        SpawnCoins();
    }

    private void SpawnCoins()
    {
        coins.value += amount;
        coinAmountChangedEvent.Raise();
        var particleSystem = ObjectPooler.SharedInstance.Instantiate(coinPartileSystemPrefab, transform.position + new Vector3(Random.Range(-1f, 1f), 2f, Random.Range(-1f, 1f)), Quaternion.identity);
        particleSystem.GetComponent<ParticleSystem>().emission.SetBurst(0, new ParticleSystem.Burst(0f,(short)amount, (short)amount,1,0.001f));
        particleSystem.transform.parent = GameObject.FindGameObjectWithTag("dynamic").transform;
    }
}
