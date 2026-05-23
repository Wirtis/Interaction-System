using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionController : MonoBehaviour
{
    public InputSystem_Actions inputAction;
    private InputAction _interact;
    public InteractionManager interactionManager;

    void Awake()
    {
        inputAction = new InputSystem_Actions();
    }

    void OnEnable()
    {
        _interact = inputAction.Player.Interact;
        _interact.Enable();

        _interact.performed += _ => interactionManager.TryInteract();
    }

    void OnDisable()
    {
        _interact.Disable();
    }
}
