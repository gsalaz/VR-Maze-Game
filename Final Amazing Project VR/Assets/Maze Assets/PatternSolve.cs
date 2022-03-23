using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternSolve : MonoBehaviour
{

    //public static GameObject pA, pB, pC, pD, pE;
    int correctCount = 0;
    public delegate void OnFailure();
    public static event OnFailure ResetTime;
    public GameObject gate;
    public Light goalLight, succLight;
    float eveningBell = 1f;
    bool over;

    // Start is called before the first frame update
    void Start()
    {
        over = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsCorrect(int id) {
        //Debug.Log("Hi!");
        if (over) {
            return false;
        }else if (id == correctCount + 1) {
            //Debug.Log("Correct!");
            correctCount += 1;
            if (correctCount >= 5) {
                over = true;
                StartCoroutine(PuzzleFinish());
            }
            return true;
        }else {
            //Debug.Log("Wrong!");
            correctCount = 0;
            ResetTime();
            return false;
        }
    }

    IEnumerator PuzzleFinish() {
        while (eveningBell > 0) {
            eveningBell -= Time.deltaTime;
            yield return null;
        }
        gate.GetComponent<GateStuff>().AddPoint();
        succLight.color = Color.green;
        goalLight.color = Color.green;
        gameObject.SetActive(false);
    }
}
