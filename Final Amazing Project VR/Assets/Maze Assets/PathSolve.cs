using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSolve : MonoBehaviour
{

    public delegate void OnFailure();
    public static event OnFailure ResetTime;
    public GameObject gate;
    public Light goalLight, succLight;
    int[] pathway = {12, 22, 21, 31, 41, 42, 43, 33, 34, 35, 45, 55};
    int cur;
    float eveningBell = 1f;
    bool over;

    // Start is called before the first frame update
    void Start()
    {
        cur = 0;
        over = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsCorrect(int id) {
        if (over) {
            return false;
        }else if (pathway[cur] == id) {
            cur += 1;
            if (cur >= pathway.Length) {
                over = true;
                StartCoroutine(PuzzleFinish());
            }
            return true;
        }else {
            cur = 0;
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
