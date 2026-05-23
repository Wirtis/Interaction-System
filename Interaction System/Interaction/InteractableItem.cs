using UnityEngine;

public abstract class InteractableItem : Interactable
{
    public ItemData itemData;
    public abstract string GetItemDescription();
}
