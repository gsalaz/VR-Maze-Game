using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float forceAmount;
    public Transform camera;
    private Rigidbody rb;
    float cam_h, cam_v;
    private float horizontalAxisValue, verticalAxisValue;

    float horizontalSpeed = 3.0f;
	float verticalSpeed = 3.0f;
    int jumpCount;

    static public bool paused;
    public GameObject pausedScreen;

    public UnityEvent switchScene;

    RaycastHit hit;
    GameObject grabbed;
    public Transform grabbedPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpCount = 0;
        Time.timeScale = 1;
        paused = false;
        pausedScreen.SetActive(false); ;

    }

    // Update is called once per frame
    void Update()
    {
    	cam_h += Input.GetAxis("Mouse X") * horizontalSpeed;
        cam_v += Input.GetAxis("Mouse Y") * verticalSpeed;

        horizontalAxisValue = Input.GetAxis("Horizontal");
        verticalAxisValue = Input.GetAxis("Vertical");

        if(Input.GetButtonDown("Jump") && jumpCount < 2) {
            jumpCount += 1;
        	rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        }

        if (Input.GetMouseButtonDown(1) && !paused) { 
            if (Physics.Raycast(transform.position, transform.forward, out hit, 3)
            && hit.transform.GetComponent<Rigidbody>()){ 
                grabbed = hit.transform.gameObject;
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            grabbed = null;
        }

        if (grabbed) //moving object
            grabbed.GetComponent<Rigidbody>().velocity = 10*(grabbedPosition.position - grabbed.transform.position);

        if (Input.GetKeyDown(KeyCode.Escape)) {         
            if (!paused)
            {
                paused = true;
                Time.timeScale = 0;
                pausedScreen.SetActive(true);
            }
            else
            {
                paused = false;
                Time.timeScale = 1;
                pausedScreen.SetActive(false);
            }   
        }
    }

    private void FixedUpdate()
    {
        camera.eulerAngles = (new Vector3(cam_v * (-1), cam_h, 0));

        rb.AddForce(camera.right * horizontalAxisValue * forceAmount +
              camera.forward * verticalAxisValue * forceAmount);   
    }

    private void OnCollisionEnter(Collision collision) {
    	if(collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Object") {
            jumpCount = 0;
    	}
    }
}
