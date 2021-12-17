using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject startingRoom;
    [SerializeField] private GameObject startingRoomPrefab;
    [SerializeField] private StringList roomList;
    [SerializeField] private GameObjectList enemyInstances;
    [Header("Scriptable Objects")]
    [SerializeField] private IntValue coins;

    private bool swaping;

    public void SpawnRoom()
    {
        var rand = Random.Range(1, roomList.strings.Count);
        SwapRoom(roomList.strings[rand]);
    }

    public void SpawnStartingRoom()
    {
        SwapRoom(roomList.strings[0]);
    }

    private void SwapRoom(string room_name)
    {
        UnloadAllScenesWithName("Room");
        LoadRoom(room_name);
    }

    private void LoadRoom(string room_name)
    {
        SceneManager.LoadScene(room_name, LoadSceneMode.Additive);
    }

    private void UnloadAllScenesWithName(string sceneName)
    {
        int c = SceneManager.sceneCount;
        for(int i = 0; i < c; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if(scene.name.Contains(sceneName))
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        } 
    }

    public void ResetCoinAmount()
    {
        coins.value = 0;
    }

    private void OnDestroy()
    {
        ResetCoinAmount();
    }
}
