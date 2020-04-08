using System.Collections;
using System.Collections.Generic;
using OVR;
using UnityEngine;

public class BallBounce : OVRGrabbable
{
    public SoundFXRef bounce;
    public SoundFXRef whoosh;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        bounce.PlaySoundAt(transform.position);
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        Rigidbody ballRigidbody = gameObject.GetComponent<Rigidbody>();
        ballRigidbody.isKinematic = false;
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
        whoosh.PlaySoundAt(transform.position);
        Destroy(gameObject, 5);
    }
}
