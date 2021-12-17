using UnityEngine;

public class EntranceDoor : MonoBehaviour
{
    [SerializeField] private GameObject door;

    private void Awake()
    {
        door.SetActive(true);
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
