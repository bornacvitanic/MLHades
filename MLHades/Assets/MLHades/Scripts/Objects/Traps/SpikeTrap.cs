using UnityEngine;

public class SpikeTrap : Trap
{
    [SerializeField] private GameObject spikes;
    [SerializeField] private Animator animator;

    public override void Activate(GameObject triggerGameObject)
    {
        animator.SetTrigger("Activate");
    }
}
