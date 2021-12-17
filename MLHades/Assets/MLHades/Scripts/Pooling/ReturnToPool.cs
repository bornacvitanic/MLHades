using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    private void OnEnable()
    {
        hideFlags = HideFlags.HideInInspector;
    }

    private void OnDisable()
    {
        Invoke("Return", 0.01f);
    }

    public void Return()
    {
        if(ObjectPooler.SharedInstance == null)
        {
            return;
        }

        ObjectPooler.SharedInstance.ReturnObject(gameObject);
    }
}
