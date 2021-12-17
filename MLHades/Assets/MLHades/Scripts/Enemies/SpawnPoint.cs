using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObjectList enemyPrefabs;
    [SerializeField] private float spawnDelay = 2f;

    private float timeFromStart;
    private bool spawned = false;
    private int random;

    private void OnEnable()
    {
        spawned = false;
        timeFromStart = Time.time;
        random = Random.Range(0, enemyPrefabs.objects.Count);
    }

    private void FixedUpdate()
    {
        if(!spawned && Time.time - timeFromStart > spawnDelay)
        {
            Spawn();
        }
        else if(spawned && Time.time - timeFromStart > spawnDelay / 4f)
        {
            gameObject.SetActive(false);
        }
    }

    private void Spawn()
    {
        var enemy = Instantiate(enemyPrefabs.objects[random], transform.position + new Vector3(0f, enemyPrefabs.objects[random].transform.localPosition.y, 0f), Quaternion.identity);
        enemy.transform.parent = GameObject.FindGameObjectWithTag("dynamic").transform;
        timeFromStart = Time.time;
        spawned = true;
    }
}
