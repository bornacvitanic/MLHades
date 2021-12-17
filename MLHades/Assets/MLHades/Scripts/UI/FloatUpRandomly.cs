using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class FloatUpRandomly : MonoBehaviour
{
    private RectTransform rectTransform;
    private float randomDirection;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        randomDirection = Random.Range(-0.3f,0.3f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rectTransform.localPosition += new Vector3(randomDirection, 0.3f, 0f);
    }
}
