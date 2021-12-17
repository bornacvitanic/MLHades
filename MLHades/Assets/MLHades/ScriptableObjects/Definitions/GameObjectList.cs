using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectList", menuName = "ScriptableObjects/GameObjectList")]
public class GameObjectList : ScriptableObject
{
    public List<GameObject> objects;
}
