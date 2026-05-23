using UnityEngine;

public interface IInteractable
{
    public enum InteractionIcon
    {
        Talk,
        Take,
        Look,
    }

    void Interact();
    string GetInteractionText();
    InteractionIcon GetInteractionIcon();
}
