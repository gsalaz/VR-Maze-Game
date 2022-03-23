using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Renaming of integers used in Input.GetMouseButton()
    enum MouseButton { LEFT, RIGHT, MIDDLE }

    // Globally-available container for non-VR user input info
    public static InputManager instance;

    // Reference to camera from which raycasts will originate
    [SerializeField] private Camera raycastCamera = null;

    // Mouse button used for raycast-based interactions (left by default)
    [SerializeField] private MouseButton interactButton = MouseButton.LEFT;

    // Maximum permissible raycast length
    [SerializeField] private float interactRange = 2f;

    // Event to be raised when user begins hovering over an interactable object
    private Action<GameObject> hoverStart;

    // Event to be raised when user stops hovering over an interactable object
    private Action<GameObject> hoverEnd;

    // Event to be raised when user clicks on an interactable object
    private Action<GameObject> interact;

    // Normalized representation of viewport center
    private Vector3 center = new Vector3(0.5f, 0.5f, 0f);

    // Reference to interactable GameObject hovered over by player in previous frame
    private GameObject lastInteractable;


    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {
        Ray ray = raycastCamera.ViewportPointToRay(center);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactRange))
        {
            if (hit.collider.gameObject.tag == "Interactable")
            {
                if (lastInteractable == null || hit.collider.gameObject != lastInteractable)
                {
                    if (lastInteractable != null)
                        hoverEnd?.Invoke(lastInteractable);

                    lastInteractable = hit.collider.gameObject;
                    hoverStart?.Invoke(lastInteractable);
                }

                if (Input.GetMouseButtonDown((int)interactButton))
                    interact?.Invoke(lastInteractable);
            }
            else if (lastInteractable != null)
            {
                hoverEnd?.Invoke(lastInteractable);
                lastInteractable = null;
            }
        }
        else if (lastInteractable != null)
        {
            hoverEnd?.Invoke(lastInteractable);
            lastInteractable = null;
        }
    }


    public void AddHoverStartListener(Action<GameObject> response)
    {
        hoverStart += response;
    }


    public void AddHoverEndListener (Action<GameObject> response)
    {
        hoverEnd += response;
    }


    public void AddInteractListener(Action<GameObject> response)
    {
        interact += response;
    }


    public void RemoveHoverStartListener (Action<GameObject> response)
    {
        hoverStart -= response;
    }


    public void RemoveHoverEndListener (Action<GameObject> response)
    {
        hoverEnd += response;
    }


    public void RemoveInteractListener (Action<GameObject> response)
    {
        interact += response;
    }
}
