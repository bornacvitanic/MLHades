using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepStartRotation : MonoBehaviour
{
    private Vector3 rotation;

    public void SetAngle(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    private void OnEnable() => rotation = transform.eulerAngles;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.eulerAngles = rotation;
    }

    private void OnDisable() => transform.eulerAngles = Vector3.zero;
}
