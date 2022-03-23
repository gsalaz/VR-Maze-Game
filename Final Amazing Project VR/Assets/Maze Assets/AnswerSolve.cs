using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnswerSolve : MonoBehaviour
{

    public delegate void OnFailure();
    public static event OnFailure ResetTime;
    public GameObject gate;
    public Light goalLight, succLight;
    bool[] plateStates = {false, false, false, false}, goalStates = {true, false, true, true};
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
        if (over) {
            return false;
        }else if (id != 5) {
            plateStates[id - 1] = true;
            return true;
        }else {
            if (Enumerable.SequenceEqual(plateStates, goalStates)) {
                //Debug.Log("YAAAAAAAAAY");
                over = true;
                StartCoroutine(PuzzleFinish());
                return true;
            }else {
                /*Debug.Log("Boo you suck");
                foreach(var item in plateStates)
                {
                    Debug.Log(item.ToString());
                }
                Debug.Log("Should be:");
                foreach(var item in goalStates)
                {
                    Debug.Log(item.ToString());
                }*/
                plateStates = new bool[4];
                ResetTime();
                return false;
            }
        }
    }

    /*void PuzzleFinish() {
        //Debug.Log("Tada!");
        //gate.SetActive(false);
        gate.GetComponent<GateStuff>().AddPoint();
        succLight.color = Color.green;
        goalLight.color = Color.green;
        gameObject.SetActive(false);
    }*/

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
