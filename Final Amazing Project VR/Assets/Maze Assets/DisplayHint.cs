using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayHint : MonoBehaviour
{
    public GameObject disA, disB, disC, disD, disE;
    float updateClock;

    // Start is called before the first frame update
    void Start()
    {
        disA.SetActive(false);
        disB.SetActive(false);
        disC.SetActive(false);
        disD.SetActive(false);
        disE.SetActive(false);
        updateClock = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        updateClock += Time.deltaTime;
        if (updateClock >= 7f) {
            updateClock = 0f;
            disA.SetActive(true);
        }else if (updateClock >= 1f && updateClock < 2f) {
            disA.SetActive(false);
            disB.SetActive(true);
        }else if (updateClock >= 2f && updateClock < 3f) {
            disB.SetActive(false);
            disC.SetActive(true);
        }else if (updateClock >= 3f && updateClock < 4f) {
            disC.SetActive(false);
            disD.SetActive(true);
        }else if (updateClock >= 4f && updateClock < 5f) {
            disD.SetActive(false);
            disE.SetActive(true);
        }else if (updateClock >= 5f) {
            disE.SetActive(false);
        }
    }
}
