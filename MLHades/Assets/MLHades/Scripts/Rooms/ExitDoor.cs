using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private GameObject lockedDoor;
    [SerializeField] private GameObject unlockedDoor;
    [SerializeField] private GameObjectList enemyInstances;

    public void UnlockDoor()
    {
        unlockedDoor.SetActive(true);
        lockedDoor.SetActive(false);
    }

    public void UnlockDoorIfNoEnemiesLeft()
    {
        if(enemyInstances.objects.Count == 0)
        {
            UnlockDoor();
        }
    }
}
