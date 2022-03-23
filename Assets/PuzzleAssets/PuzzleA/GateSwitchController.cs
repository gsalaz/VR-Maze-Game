using System;
using UnityEngine;

public class GateSwitchController : Interactable
{
    // Rotate-able part of gate switch
    [SerializeField] private Transform pivot = null;

    // Rotation speed in degrees per second
    [SerializeField] private float rotateSpeed = 90f;

    // Material that represents a True/on/correct input
    [SerializeField] private Material onMat = null;

    // Material that represents a False/off/incorrect input
    [SerializeField] private Material offMat = null;

    // Flag for whether this switch is located on the east or west wall of the room
    [SerializeField] private bool isOnEastWall = false;

    // Switch input configuration
    [SerializeField] private bool[] config = new bool[4];

    // References to Quad MeshRenderer components on each switch face
    [SerializeField] private MeshRenderer[] faces = new MeshRenderer[4];

    // References to other GateSwitchControllers that may trigger this one to rotate
    [SerializeField] private GateSwitchController[] links = null;

    // Event to be raised when switch setting changes and gate should be informed (should also be called once at start)
    private Action<GateSwitchController, bool> switchStateChange;

    // Event to be raised when switch setting changes and connected switches should be informed
    private Action linkStateChange;

    // Working target rotation, as a Quaternion and not a Vector
    private Quaternion target;

    // Flag that prevents attempting to rotate switch while rotation is already in progress
    private bool canRotate;

    // Index of switch state, redetermined from config on rotation
    private int configIndex;


    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < config.Length; i++)
            faces[i].material = config[i] ? onMat : offMat;

        foreach (GateSwitchController link in links)
            link.AddLinkStateChangeListener(OnLinkStateChange);

        target = pivot.rotation;
        canRotate = true;

        configIndex = isOnEastWall ? 3 : 1;

        switchStateChange?.Invoke(this, config[configIndex]);
    }

    private void Update()
    {
        pivot.rotation = Quaternion.RotateTowards(pivot.rotation, target, rotateSpeed * Time.deltaTime);
        if (pivot.rotation == target)
            canRotate = true;
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
        if (interactable == gameObject && canRotate)
        {
            canRotate = false;
            target *= Quaternion.AngleAxis(-90f, Vector3.up);

            configIndex = (configIndex + 1) % config.Length;
            switchStateChange?.Invoke(this, config[configIndex]);
            linkStateChange?.Invoke();
        }
    }


    private void OnLinkStateChange()
    {
        canRotate = false;
        target *= Quaternion.AngleAxis(-90f, Vector3.up);

        configIndex = (configIndex + 1) % config.Length;
        switchStateChange?.Invoke(this, config[configIndex]);
    }


    public void AddSwitchStateChangeListener(Action<GateSwitchController, bool> response)
    {
        switchStateChange += response;
    }


    public void AddLinkStateChangeListener(Action response)
    {
        linkStateChange += response;
    }
}
