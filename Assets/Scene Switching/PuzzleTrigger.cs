using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleTrigger : MonoBehaviour
{
    //trigger to start puzzle room
    public UnityEvent puzzleSwitch;
    //if puzzle completed
    bool isComplete;

    void Start()
    {
        isComplete = false;
    }

    private void OnTriggerEnter (Collider other)
    {
        if (!(isComplete)) //if not completed
        {
            isComplete = true;
            puzzleSwitch.Invoke();
        }
    }


}
