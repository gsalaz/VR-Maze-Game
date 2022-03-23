using UnityEngine;

[RequireComponent(typeof(FlameFade))]
public class TorchController : Interactable
{
    // Reference to grab Transform component of player (to know where to move to when picked up)
    [SerializeField] private Transform playerGrab = null;

    // Reference to this GameObject's FlameFade script
    private FlameFade fadeScript;


    protected override void Start()
    {
        base.Start();

        fadeScript = GetComponent<FlameFade>();
    }


    protected override void OnHoverStart(GameObject interactable)
    {
        return;
    }


    protected override void OnHoverEnd(GameObject interactable)
    {
        return;
    }


    protected override void OnInteract(GameObject interactable)
    {
        if (interactable == gameObject && playerGrab.childCount == 0)
        {
            transform.SetParent(playerGrab);
            transform.localPosition = Vector3.zero;

            fadeScript.BeginFade();
        }
    }
}
