using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : OVRGrabbable
{
    public AudioSource bounce;
    public AudioSource whoosh;

    // Start is called before the first frame update
    void Start()
    {
        bounce = GetComponent<AudioSource>();
        whoosh = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        bounce.Play();
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);

        if(grabPoint.Equals(OVRInput.Controller.LTouch)) {
            OVRInput.SetControllerVibration(0.25F, 0.25F, OVRInput.Controller.LTouch);
        }
        else
        {
            OVRInput.SetControllerVibration(0.25F, 0.25F, OVRInput.Controller.RTouch);
        }

    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        whoosh.Play();
        Destroy(gameObject, 5);
    }
}
