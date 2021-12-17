using UnityEngine;

public class RoomController : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private int enemiesPerWave = 10;
    [SerializeField] private int numberOfWaves = 1;
    [Header("Room Elements")]
    [SerializeField] private GameObject entranceSpawnPoint;
    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject spawnPointPrefab;
    [SerializeField] private GameObject dynamic;
    [SerializeField] private GameObjectList rewardPrefabs;
    [Header("Objects")]
    [SerializeField] private GameObjectReference playerReference;
    [SerializeField] private GameObjectList enemyPrefabs;
    [SerializeField] private GameObjectList enemyInstancesSO;
    [Header("GameEvents")]
    [SerializeField] private GameEvent roomEnteredEvent;
    [SerializeField] private GameEvent roomClearedEvent;

    private int enemiesSpawnedThisWave;
    private int numberOfWavesLeft;

    // Start is called before the first frame update
    private void Start()
    {
        SpawnPlayer();
        numberOfWavesLeft=numberOfWaves;
    }

    #region Helper Functions
    private Vector3 PickPositionInRoom()
    {
        Vector3 position;
        var floorMeshColliders = ground.GetComponentsInChildren<MeshCollider>();
        int randPart;
        randPart = Random.Range(0, floorMeshColliders.Length);
        var dimensions = floorMeshColliders[randPart].transform.localScale * 4.5f;
        position = floorMeshColliders[randPart].transform.position + new Vector3(Random.Range(-dimensions.x, dimensions.x), 0f, Random.Range(-dimensions.z, dimensions.z));
        return position;
    }

    private Vector3 PickPositionAroundPlayer(int range = 8)
    {
        Vector3 position;
        RaycastHit hit;
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        if(Physics.Raycast(playerReference.GetReference().transform.position, randomDirection, out hit, range, 28672))
        {
            position = playerReference.GetReference().transform.position + randomDirection* Random.Range(0.5f, Mathf.Max(0, hit.distance - 1f)) ;
        }
        else
        {
            position = playerReference.GetReference().transform.position + randomDirection * Random.Range(1f, range);
        }
        return position;
    }
    #endregion Helper Functions

    public void EnemyRemoved()
    {
        if(enemyInstancesSO.objects.Count == 0)
        {
            if(numberOfWavesLeft > 1)
            {
                SpawnWave();
            }
            else
            {
                roomClearedEvent.Raise();
            }
        }
        else
        {
            if(enemiesSpawnedThisWave < enemiesPerWave)
            {
                SpawnSpawnPoint();
                enemiesSpawnedThisWave += 1;
            }
        }
    }

    #region Spawning
    private void SpawnPlayer()
    {
        playerReference.GetReference().transform.position = new Vector3(entranceSpawnPoint.transform.position.x, playerReference.GetReference().transform.position.y, entranceSpawnPoint.transform.position.z);
        roomEnteredEvent.Raise();
    }

    public void SpawnEnemy()
    {
        var randomNum = Random.Range(0, enemyPrefabs.objects.Count);
        var enemy = Instantiate(enemyPrefabs.objects[randomNum], PickPositionInRoom() + new Vector3(0f, enemyPrefabs.objects[randomNum].transform.localPosition.y, 0f), Quaternion.identity);
        enemy.transform.parent = dynamic.transform;
    }

    public void SpawnSpawnPoint()
    {
        var randomNum = Random.Range(0, enemyPrefabs.objects.Count);
        var spawnPointInstance = ObjectPooler.SharedInstance.Instantiate(spawnPointPrefab, PickPositionInRoom(), Quaternion.identity);
        spawnPointInstance.transform.parent = dynamic.transform;
    }

    private void SpawnWave()
    {
        for(int i = 0; i < enemiesPerWave / 2; i++)
        {
            SpawnSpawnPoint();
        }
        numberOfWavesLeft -= 1;
        enemiesSpawnedThisWave = enemiesPerWave / 2;
    }

    public void SpawnInitialWave()
    {
        for(int i = 0; i < enemiesPerWave / 2; i++)
        {
            SpawnEnemy();
        }
        numberOfWavesLeft -= 1;
        enemiesSpawnedThisWave = enemiesPerWave / 2;
    }

    public void SpawnReward()
    {
        var rand = Random.Range(0, rewardPrefabs.objects.Count);
        var reward = ObjectPooler.SharedInstance.Instantiate(rewardPrefabs.objects[rand], PickPositionAroundPlayer()-Vector3.up, Quaternion.identity);
        reward.transform.localScale = rewardPrefabs.objects[rand].transform.localScale * 3f;
        reward.transform.parent = dynamic.transform;
    }
    #endregion Spawning

    public int GetEnemiesPerWave(){
        return enemiesPerWave;
    }

    public int GetNumberOfWaves(){
        return numberOfWaves;
    }

    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        var floorMeshColliders = ground.GetComponentsInChildren<MeshCollider>();
        foreach(MeshCollider p in floorMeshColliders)
        {
            var dimensions = p.transform.localScale * 4.5f;
            Gizmos.DrawCube(p.transform.position, new Vector3(dimensions.x*2, 0.2f, dimensions.z*2));
        }
    }
    #endif
}
