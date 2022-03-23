using UnityEngine;

public class KeyboardMovement : MonoBehaviour
{
    // Transform component of main (first-person) camera
    [SerializeField] private Transform cam = null;

    // Player movement speed (Unity units per second)
    [Range(0.1f, 10f)] [SerializeField] private float moveSpeed = 5f;

    // Camera rotation speed along x-axis
    [Range(0.1f, 10f)] [SerializeField] private float camSpeedX = 4f;

    // Camera rotation speed along y-axis
    [Range(0.1f, 10f)] [SerializeField] private float camSpeedY = 4f;

    // Attached Rigidbody component
    private Rigidbody rb;

    // Working player velocity vector
    private Vector3 velocity;

    // Working camera displacement vector
    private Vector3 camDelta;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        velocity = new Vector3();
        camDelta = new Vector3();
    }


    private void Update()
    {
        // Get change in camera rotation from mouse movement
        camDelta.x = -Input.GetAxis("Mouse Y") * camSpeedY;
        camDelta.y = Input.GetAxis("Mouse X") * camSpeedX;

        // Update camera rotation
        cam.eulerAngles += camDelta;

        // Reset working velocity
        velocity = Vector3.zero;

        // Get direction of change in player position from WASD or arrow key presses
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            velocity += cam.transform.forward;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            velocity -= cam.transform.right;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            velocity -= cam.transform.forward;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            velocity += cam.transform.right;

        // Normalize velocity in case of diagonal movement
        if (velocity.sqrMagnitude > 1)
            velocity = velocity.normalized;

        // Apply speed to direction
        velocity *= moveSpeed;
    }

    
    private void FixedUpdate()
    {
        // Update player position
        rb.position += velocity * Time.deltaTime;
    }
}
