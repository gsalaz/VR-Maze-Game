using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrellHit : MonoBehaviour
{
    private AudioSource explode;

    private void Start()
    {
        explode = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball") {
            Debug.Log("explode");
            explode.Play();

            Destroy(gameObject, 3);

        }
    }
}
