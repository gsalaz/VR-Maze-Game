using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float forceAmount;
    public Transform camera;
    private Rigidbody rb;
    float cam_h, cam_v;
    private float horizontalAxisValue, verticalAxisValue;

    float horizontalSpeed = 3.0f;
	float verticalSpeed = 3.0f;

    public bool cubeIsOnTheGround = true;

    public Rigidbody barrel;
    public Rigidbody ball;
    private AudioSource whoosh;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        whoosh = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    	cam_h += Input.GetAxis("Mouse X") * horizontalSpeed;
        cam_v += Input.GetAxis("Mouse Y") * verticalSpeed;

        horizontalAxisValue = Input.GetAxis("Horizontal");
        verticalAxisValue = Input.GetAxis("Vertical");

        if(Input.GetButtonDown("Jump") && cubeIsOnTheGround) {
            Debug.Log("here");
        	rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        	cubeIsOnTheGround = false;
        }
        
        if(Input.GetMouseButtonDown(1)) {
            RaycastHit hit;
            if (Physics.Raycast(camera.position, camera.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                Rigidbody clone;
                clone = Instantiate(barrel, hit.point, Quaternion.identity);
            }
            else {
                Debug.Log("Moved");
                Rigidbody clone;
                clone = Instantiate(barrel, hit.point + new Vector3(0,0,5), Quaternion.identity);
            }

        }

        if (Input.GetMouseButtonDown(0)) {

            Rigidbody clone;
            clone = Instantiate(ball, camera.position, Quaternion.identity);
            clone.AddForce(camera.forward * 500);
            whoosh.Play();

            Destroy(clone.gameObject, 5);
            Destroy(clone, 5);
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
    		cubeIsOnTheGround = true;
    	}
    }
}
