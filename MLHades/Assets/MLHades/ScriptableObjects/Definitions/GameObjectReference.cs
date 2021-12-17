using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectReference", menuName = "ScriptableObjects/GameObjectReference")]
public class GameObjectReference : ScriptableObject
{
    [SerializeField] private GameObject reference;

    public void AddReference(GameObject obj)
    {
        reference = obj;
    }

    public void RemoveReference()
    {
        reference = null;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
