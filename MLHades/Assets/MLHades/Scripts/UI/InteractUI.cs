using UnityEngine;

public class InteractUI : MonoBehaviour
{
    [SerializeField] private GameObjectReference interactableReference;
    [SerializeField] private GameObject interactableUI;

    public void ToggleInteractUI()
    {
        if(interactableReference.GetReference() == null)
        {
            interactableUI.SetActive(false);
        }
        else
        {
            interactableUI.SetActive(true);
        }
    }
}
