using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BarrellHit : MonoBehaviour
{
    public AudioClip explode;
    public GameObject plusone;

    public static int numHit = 0;

    private void Start()
    {
        numHit = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball") {

            AudioSource.PlayClipAtPoint(explode, transform.position);

            GameObject clone;
            clone = Instantiate(plusone, transform.position + new Vector3(0,1,0), Quaternion.identity);
            Destroy(clone, 2);

            Scoring.scoreEvent.Invoke();
            numHit += 1;
            Destroy(gameObject);
            
        }
    }
}
