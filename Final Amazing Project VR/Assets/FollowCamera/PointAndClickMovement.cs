using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PointAndClickMovement : MonoBehaviour
{
    // Renaming of integers used in Input.GetMouseButton()
    enum MouseButton { LEFT, RIGHT, MIDDLE }

    // Reference to camera from which raycasts will originate
    [SerializeField] private Camera raycastCamera = null;

    // Mouse button used to select movement destination (right by default)
    [SerializeField] private MouseButton moveButton = MouseButton.RIGHT;

    // Movement speed, in Unity units per second
    [SerializeField] private float moveSpeed = 0f;

    // Rotation speed (I don't know whether this is in degrees, radians, or Unity units)
    [SerializeField] private float rotateSpeed = 0f;

    // Optional prefab to spawn at destination for visual clarity
    [SerializeField] private GameObject marker = null;

    // Maximum permissible raycast length used by ScreenPointToRay()
    private const float MAX_RAYCAST_DIST = 20f;

    // Reference to this GameObject's Rigidbody component
    private Rigidbody rb;

    // Manipulable container for currently desired position
    private Vector3 targetPos;

    // Manipulable container for currently desired rotation
    private Vector3 targetDir;

    // Quaternion representation of desired rotation, determined from targetDir
    private Quaternion targetRotation;

    // Flag to signal when movement is in progress (new destination can be selected while moving)
    private bool moving;

    // Instance of targetMarker (only one will be tracked at a time)
    private GameObject markerInstance = null;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        targetPos = rb.position;
        targetDir = rb.rotation.eulerAngles;
        moving = false;

        if (marker)
        {
            markerInstance = Instantiate(marker);
            markerInstance.SetActive(false);
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown((int)moveButton))
        {
            // Raycast from camera in direction of click and move to point on collider hit
            Ray ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, MAX_RAYCAST_DIST))
            {
                // Set destination to raycast collision point, excluding y value
                targetPos.x = hit.point.x;
                targetPos.z = hit.point.z;

                // Determine direction and necessary rotation to face destination
                targetDir = (targetPos - rb.position).normalized;
                targetRotation = Quaternion.LookRotation(targetDir);

                // Set moving flag to permit calculations in FixedUpdate()
                moving = true;

                // Move and activate destination marker, if provided
                if (markerInstance)
                {
                    markerInstance.transform.position = new Vector3(targetPos.x, 0f, targetPos.z);
                    if (!markerInstance.activeSelf)
                        markerInstance.SetActive(true);
                }
            }
        }
    }


    private void FixedUpdate()
    {
        if (moving)
        {
            // Move over time until destination is reached
            rb.position = Vector3.MoveTowards(rb.position, targetPos, moveSpeed * Time.deltaTime);

            // Rotate over time until facing destination
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            // Reset moving to false and hide destination marker only when both position and rotation have reached target
            if (rb.position == targetPos && rb.rotation == targetRotation)
            {
                moving = false;
                markerInstance.SetActive(false);
            }
        }
    }
}
