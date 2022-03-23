using System;
using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    // Global event raised when Teleporter is used, to which all Teleporters in scene respond to
    public static Action<Transform, Teleporter, Teleporter> teleport;

    // Reference to matching teleporter exit
    [SerializeField] private Teleporter exit = null;

    // Material to switch to if teleporter is used
    [SerializeField] private Material altMat = null;

    // Reference to this GameObject's MeshRenderer component
    private MeshRenderer meshrend;

    // Default material
    private Material regMat;

    // Difference between player's position and teleporter's position
    private Vector3 offset = new Vector3(0f, 1f, 0f);

    // Integer representing layer Default
    private const int defaultLayer = 0;

    // Integer representing layer Arriving
    private const int arrivingLayer = 9;

    // Flag for indicating whether trigger is ready to register collisions
    private bool ready;

    // Cooldown between trigger collision registering
    private float triggerCD = 0.1f;


    private void Start()
    {
        teleport += OnTeleport;
        meshrend = GetComponent<MeshRenderer>();
        regMat = meshrend.material;
        ready = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (ready)
        {
            if (other.tag == "Player")
            {
                if (other.gameObject.layer == defaultLayer)
                {
                    teleport?.Invoke(other.transform, this, exit);
                    meshrend.material = altMat;
                    exit.GetComponent<MeshRenderer>().material = altMat;
                }
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (ready)
        {
            if (other.tag == "Player")
            {
                if (other.gameObject.layer == arrivingLayer)
                {
                    other.gameObject.layer = defaultLayer;
                }
            }
        }
    }


    private void OnTeleport(Transform player, Teleporter entrance, Teleporter exit)
    {
        StartCoroutine(StartCooldown(triggerCD));

        player.gameObject.layer = arrivingLayer;
        player.position = exit.transform.position + offset;

        if (this != entrance || this != exit)
            meshrend.material = regMat;
    }


    private IEnumerator StartCooldown(float cd)
    {
        ready = false;

        float tick = 0.017f;
        float elapsed = 0f;
        while (elapsed <= cd)
        {
            elapsed += tick;
            yield return new WaitForSeconds(tick);
        }

        ready = true;
    }
}
