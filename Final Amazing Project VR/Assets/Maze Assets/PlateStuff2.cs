using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateStuff2 : MonoBehaviour
{
    bool isPressed = false;
    float upY = 0.25f, downY = -0.2f;
    public int id;
    public GameObject puzzle;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.TransformVector(0, upY, 0).y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Unpress() {
        SetPos(false);
    }

    void SetPos(bool myState) {
        isPressed = myState;
        if (myState) {
            transform.position = new Vector3(transform.position.x, transform.TransformVector(0, downY, 0).y, transform.position.z);
        }else {
            transform.position = new Vector3(transform.position.x, transform.TransformVector(0, upY, 0).y, transform.position.z);
        }
    }

    void OnEnable() {
        PathSolve.ResetTime += Unpress;
    }

    void OnDisable() {
        PathSolve.ResetTime -= Unpress;
    }

    void OnCollisionEnter(Collision other) {
        //Debug.Log("Touch");
        if (other.gameObject.tag == "Player" && !isPressed && puzzle.GetComponent<PathSolve>().IsCorrect(id)) {
            SetPos(true);
        }
    }
}
