using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float translationalSpeed = 1.0f, rotationalSpeed = 1.0f;
	public Transform cameraTransform;
	float horizontalSpeed = 2.0f;
	float verticalSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * translationalSpeed;
        float rotation = Input.GetAxis("Horizontal") * rotationalSpeed;
        float h = Input.GetAxis("Mouse X") * horizontalSpeed;
        float v = Input.GetAxis("Mouse Y") * verticalSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        transform.Rotate(v*-1, h, 0);
        transform.Translate(0,0,translation);
        transform.Translate(rotation,0,0);
    }
}
