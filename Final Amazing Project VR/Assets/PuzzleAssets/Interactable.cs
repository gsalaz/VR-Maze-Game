using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Reference to source of user input
    protected InputManager inputManager;


    protected virtual void Start()
    {
        // Assign InputManager reference and subscribe to relevant events
        inputManager = InputManager.instance;
        inputManager.AddHoverStartListener(OnHoverStart);
        inputManager.AddHoverEndListener(OnHoverEnd);
        inputManager.AddInteractListener(OnInteract);

        // If the tag was forgotten about, set it
        if (tag != "Interactable")
            tag = "Interactable";
    }


    protected virtual void OnHoverStart(GameObject interactable)
    {
        if (interactable == gameObject)
        {
            Debug.Log("Began hovering over " + gameObject.name);
        }
    }


    protected virtual void OnHoverEnd(GameObject interactable)
    {
        if (interactable == gameObject)
        {
            Debug.Log("Stopped hovering over " + gameObject.name);
        }
    }


    protected virtual void OnInteract(GameObject interactable)
    {
        if (interactable == gameObject)
        {
            Debug.Log("Clicked on " + gameObject.name);
        }
    }


    public void DestroySelf()
    {
        inputManager.RemoveHoverStartListener(OnHoverStart);
        inputManager.RemoveHoverEndListener(OnHoverEnd);
        inputManager.RemoveInteractListener(OnInteract);
        transform.parent.DetachChildren();
        Destroy(gameObject);
    }
}
