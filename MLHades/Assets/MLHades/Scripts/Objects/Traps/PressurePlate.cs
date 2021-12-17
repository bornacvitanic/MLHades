using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : TrapTrigger
{
    [SerializeField] private MeshRenderer defaultMeshRenderer;
    [SerializeField] private MeshRenderer pressedMeshRenderer;

    [SerializeField] private GameObject dustParticleSystem;

    private bool occupied = false;
    private bool activated = false;
    private bool timeEnded = true;

    public override void Trigger()
    {
        traps.ForEach(trap =>
        {
            trap.GetComponent<Trap>().Activate(gameObject);
        });
        Activate();
        timeEnded = false;
        Invoke("TimerEnded", 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.TryGetComponent(out RoguelikeAgent roguelikeComponent))
        {
            return;
        }
        occupied = true;
        if(!activated)
        {
            Trigger();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.gameObject.TryGetComponent(out RoguelikeAgent roguelikeComponent))
        {
            return;
        }

        occupied = false;
        if(activated)
        {
            Reactivate();
        }
    }

    private void Activate()
    {
        defaultMeshRenderer.enabled = false;
        pressedMeshRenderer.enabled = true;
        activated = true;

        if(dustParticleSystem!=null){
            var particleSystem = ObjectPooler.SharedInstance.Instantiate(dustParticleSystem);
            particleSystem.transform.position = transform.position;
        }
    }

    private void Reactivate()
    {
        if(!occupied && timeEnded)
        {
            defaultMeshRenderer.enabled = true;
            pressedMeshRenderer.enabled = false;
            activated = false;
        }
    }

    private void TimerEnded()
    {
        timeEnded = true;
        Reactivate();
    }
}
