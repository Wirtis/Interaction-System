using UnityEngine;

public class InteractableAlchemy : Interactable
{
    public override void Interact()
    {
        Debug.Log("Alchemy");
        UIManager.Instance.ShowAdditional(WindowType.AlchemyTable);
        UIManager.Instance.Show(WindowType.Inventory);
        UIManager.Instance.SetUIActionMap();
    }
}
