using TMPro;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    public GameObject instr;

    public void Start()
    {
        instr.SetActive(true);
    }

    public void Exit()
    {
        instr.SetActive(false);
    }
    public void Show() {
        instr.SetActive(true);
    }


}
