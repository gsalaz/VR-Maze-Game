using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateStuff : MonoBehaviour
{

    int gateCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoint() {
        gateCount += 1;
        if (gateCount >= 3) {
            OpenGate();
        }
    }

    void OpenGate() {
        gameObject.SetActive(false);
    }
}
