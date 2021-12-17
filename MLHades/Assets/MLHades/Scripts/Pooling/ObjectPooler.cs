using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance { get; private set; }

    public GameObject dynamic;

    public Dictionary<string,Queue<GameObject>> pooledObjects;
    [Header("Optional")]
    [SerializeField] private ObjectPoolPrefabList prefabs;

    void Awake()
    {
        SharedInstance = this;
        pooledObjects = new Dictionary<string, Queue<GameObject>>();
        if(prefabs != null)
        {
            StartCoroutine("PreloadPool");
        }
    }

    #region Preloading
    IEnumerator PreloadPool()
    {
        foreach(ObjectPoolPrefabList.PoolPrefabs pair in prefabs.poolPrefabs)
        {
            if(!pooledObjects.ContainsKey(pair.prefab.name))
            {
                pooledObjects.Add(pair.prefab.name, new Queue<GameObject>());
            }
            for(int i = 0; i< pair.frequency/2; i++)
            {
                GameObject temp = CreateNewInstance(pair.prefab, Vector3.zero, Quaternion.identity);
                ReturnObject(temp);
            }
        }
        return null;
    }

    private void AddPrefabToSO(GameObject prefab)
    {
        bool exists = false;
        foreach(ObjectPoolPrefabList.PoolPrefabs pair in prefabs.poolPrefabs)
        {
            if(pair.prefab.name == prefab.name)
            {
                exists = true;
            }
        }
        if(!exists)
        {
            ObjectPoolPrefabList.PoolPrefabs temp = new ObjectPoolPrefabList.PoolPrefabs();
            temp.prefab = prefab;
            temp.frequency = 1;
            prefabs.poolPrefabs.Add(temp);
        }
    }

    private void UpdatePrefabsSO()
    {
        List<ObjectPoolPrefabList.PoolPrefabs> tempList = new List<ObjectPoolPrefabList.PoolPrefabs>();
        for(int i = 0; i < prefabs.poolPrefabs.Count; i++)
        {
            ObjectPoolPrefabList.PoolPrefabs temp = prefabs.poolPrefabs[i];
            temp.prefab = prefabs.poolPrefabs[i].prefab;
            if(pooledObjects[prefabs.poolPrefabs[i].prefab.name].Count > temp.frequency)
            {
                temp.frequency = pooledObjects[prefabs.poolPrefabs[i].prefab.name].Count;
            }
            tempList.Add(temp);
        }
        prefabs.poolPrefabs = tempList;
    }
    #endregion

    private GameObject CreateNewInstance(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject newObject = UnityEngine.Object.Instantiate(prefab, position, rotation);
        newObject.AddComponent<ReturnToPool>();
        newObject.transform.parent = dynamic.transform;
        newObject.SetActive(true);
        return newObject;
    }

    private void PositionAndRotateObject(GameObject objInstance, Vector3 position, Quaternion rotation)
    {
        objInstance.transform.position = position;
        objInstance.transform.rotation = rotation;
    }

    public GameObject Instantiate(GameObject prefab, Vector3? position = null, Quaternion? rotation = null)
    {
        Vector3 _position = Vector3.zero;
        Quaternion _rotation = Quaternion.identity;
        if(position != null)
        {
           _position = (Vector3)position;
        }
        if(rotation != null)
        {
            _rotation = (Quaternion)rotation;
        }
        if(pooledObjects.ContainsKey(prefab.name))
        {
            if(pooledObjects[prefab.name].Count > 0)
            {
                try{
                    GameObject tempObject = pooledObjects[prefab.name].Dequeue();
                    PositionAndRotateObject(tempObject, _position, _rotation);
                    tempObject.transform.parent = dynamic.transform;
                    tempObject.SetActive(true);
                    return tempObject;
                }
                catch
                {
                    Debug.LogWarning("Error occured while processing object of type " + prefab.name + ". The queue for this type contains " + pooledObjects[prefab.name].Count.ToString() + " instances.");
                }
                
            }
            return CreateNewInstance(prefab, _position, _rotation);
        }
        pooledObjects.Add(prefab.name, new Queue<GameObject>());
        if(prefabs != null)
        {
            AddPrefabToSO(prefab);
        }
        return CreateNewInstance(prefab, _position, _rotation);
    }

    public void ReturnObject(GameObject returnedObject)
    {
        returnedObject.transform.parent = transform;
        returnedObject.SetActive(false);
        var key = returnedObject.name.Substring(0, returnedObject.name.Length - "(Clone)".Length);
        pooledObjects[key].Enqueue(returnedObject);
    }

    public void DestroyAllObjects()
    {
        foreach(Queue<GameObject> objQueue in pooledObjects.Values)
        {
            foreach (GameObject obj in objQueue)
            {
                Destroy(obj);
            }
            objQueue.Clear();
        }
    }

    private void OnDisable()
    {
        if(prefabs != null)
        {
            UpdatePrefabsSO();
        }
    }

}
