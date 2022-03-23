using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(Collider))]
public class FollowCamera : MonoBehaviour
{
    // Transform component of GameObject to focus on and follow
    [SerializeField] private Transform target = null;

    // Offset position from which the camera will follow its target
    [SerializeField] private Vector3 offset = Vector3.zero;

    // Angle from which the camera will view its target
    [SerializeField] private Vector3 angle = Vector3.zero;

    // Time taken to reach target
    [SerializeField] private float smoothTime = 0.5f;

    // Current working velocity, modified by SmoothDamp()
    private Vector3 velocity;

    // Array of positions from which target can be viewed, each at 90 degree intervals around target
    private Vector3[] viewPoints;

    // Array of angles from which target can be viewed, each at 90 degree intervals around target
    private Vector3[] viewAngles;

    // Selected index of viewPoints
    private int viewIndex;


    private void Start ()
    {
        velocity = Vector3.zero;

        viewPoints = new Vector3[4];
        viewAngles = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            viewPoints[i] = Quaternion.AngleAxis(90 * i, Vector3.up) * offset;
            viewAngles[i] = new Vector3(angle.x, angle.y + 90 * i, angle.z);
        }

        viewIndex = 0;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            viewIndex = (viewIndex + (viewPoints.Length - 1)) % viewPoints.Length;
            transform.position = target.position + viewPoints[viewIndex];
            transform.eulerAngles = viewAngles[viewIndex];
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            viewIndex = (viewIndex + 1) % viewPoints.Length;
            transform.position = target.position + viewPoints[viewIndex];
            transform.eulerAngles = viewAngles[viewIndex];
        }
    }


    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + viewPoints[viewIndex], ref velocity, smoothTime);
    }
}
