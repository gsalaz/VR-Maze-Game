using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    private AudioSource bounce;

    // Start is called before the first frame update
    void Start()
    {
        bounce = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        bounce.Play();
    }
}
