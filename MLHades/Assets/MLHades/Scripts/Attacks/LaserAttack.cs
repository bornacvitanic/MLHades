using UnityEngine;

public class LaserAttack : Attack
{
    private float timeSinceEnable;
    private bool gracePeriod = false;
    private float tickInterval = 0.11f;
    private float timeUntilNextTick = 0f;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        timeSinceEnable = Time.time;
        gracePeriod = true;
        changeAlpha(gameObject, 0.1f);
    }

    private void OnDisable()
    {
        gracePeriod = false;
    }

    private void changeAlpha(GameObject obj, float value)
    {
        obj.GetComponent<LineRenderer>().material.SetFloat("Vector1_Alpha", value);
    }

    private void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.parent.position, transform.parent.forward, out hit, 15f, 20993))
        {
            var distance = (int)Mathf.Round(hit.distance);
            Vector3[] positions = new Vector3[distance+1];
            lineRenderer.positionCount = distance+1;
            for (int i = 0; i < distance+1; i++)
            {
                positions[i] = new Vector3(0f, 0f, i);
            }
            lineRenderer.SetPositions(positions);
        }

        if(gracePeriod && Time.time - timeSinceEnable > 1f)
        {
            changeAlpha(gameObject, 1f);
            gracePeriod = false;
        }

        if(gracePeriod){ return; }
        if(hit.collider != null && hit.collider.gameObject.TryGetComponent(out Health collisionObjectHealthComponent))
        {
            if(Time.time > timeUntilNextTick)
            {
                if(whitelistedTargets.Count != 0)
                {
                    if(whitelistedTargets.Exists(target => hit.collider.gameObject.CompareTag(target.tag)))
                    {
                        collisionObjectHealthComponent.TakeDamage(damage);
                    }
                }
                else
                {
                    collisionObjectHealthComponent.TakeDamage(damage);
                }
                timeUntilNextTick = Time.time + tickInterval;
            }
        }
    }
}
