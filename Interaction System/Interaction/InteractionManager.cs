using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField]
    private float _interactionDistance;

    [SerializeField]
    private TMPro.TextMeshProUGUI _objectNameText;

    [SerializeField]
    private Camera _cam;

    private IInteractable target;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _interactionDistance))
        {
            target = hit.collider.GetComponent<IInteractable>();
            if (target != null)
            {
                if (target is InteractableItem item)
                {
                    _objectNameText.text = item.GetItemDescription();
                }
                else
                {
                    _objectNameText.text = "Interact";
                }
            }
        }
        else
        {
            target = null;
            _objectNameText.text = "";
        }
    }

    public void TryInteract()
    {
        target?.Interact();
    }
}
