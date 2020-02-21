using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float forceAmount;
    private Rigidbody rb;
    public bool cubeIsOnTheGround = true;
    private float horizontalAxisValue, verticalAxisValue;
    float horizontalSpeed = 2.0f;
	float verticalSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    	float h = Input.GetAxis("Mouse X") * horizontalSpeed;
        float v = Input.GetAxis("Mouse Y") * verticalSpeed;

        horizontalAxisValue = Input.GetAxis("Horizontal");
        verticalAxisValue = Input.GetAxis("Vertical");
        transform.Rotate(v*-1, h, 0);

        if(Input.GetButtonDown("Jump") && cubeIsOnTheGround) {
        	rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        	cubeIsOnTheGround = false;
        }

        int layerMask = 1 << 8;
		layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(horizontalAxisValue * forceAmount, 0, verticalAxisValue * forceAmount);
    }

    private void OnCollisionEnter(Collision collision) {
    	if(collision.gameObject.tag == "Floor") {
    		cubeIsOnTheGround = true;
    	}
    }
}
