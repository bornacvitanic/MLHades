using UnityEngine;

namespace Roguelike
{
    public class Interact : MonoBehaviour
    {
        [SerializeField] private GameObjectReference interactable;
        [SerializeField] private GameEvent interactableEvent;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out IInteract interactCheck))
            {
                interactable.AddReference(other.gameObject);
                interactableEvent.Raise();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject != null && other.TryGetComponent(out IInteract interactCheck))
            {
                interactable.RemoveReference();
                interactableEvent.Raise();
            }
        }

        public void InteractWithObject()
        {
            if(interactable.GetReference() != null && interactable.GetReference().TryGetComponent(out IInteract interactComponent))
            {
                interactComponent.Interact(gameObject.transform.parent.gameObject);
                interactable.RemoveReference();
                interactableEvent.Raise();
            }
        }
    }

    public interface IInteract
    {
        void Interact(GameObject origin);
    }
}
