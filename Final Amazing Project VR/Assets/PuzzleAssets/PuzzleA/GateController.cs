using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    // List of GateSwitch GameObjects (or rather, their scripts) to receive combination input from
    [SerializeField] private GateSwitchController[] switches = null;

    // Dictionary that tracks the state of all relevant switches; gate opens when all are toggled on/True
    private Dictionary<GateSwitchController, bool> switchStates;

    // Flag that indicates whether gate is currently open or closed
    private bool open = false;


    private void Awake()
    {
        switchStates = new Dictionary<GateSwitchController, bool>();
        foreach (GateSwitchController gsc in switches)
        {
            switchStates.Add(gsc, false);
            gsc.AddSwitchStateChangeListener(OnSwitchStateChange);
        }
    }


    private void ToggleGate(bool nowOpen)
    {
        if (open != nowOpen)
            StartCoroutine(MoveGate(nowOpen ? -5f : 5f));
        open = nowOpen;
    }


    private IEnumerator MoveGate(float deltaY)
    {
        Vector3 target = new Vector3(transform.position.x, transform.position.y + deltaY, transform.position.z);

        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 0.2f);
            yield return new WaitForSeconds(0.01f);
        }
    }


    private void OnSwitchStateChange(GateSwitchController gsc, bool newState)
    {
        switchStates[gsc] = newState;

        foreach (bool switchOn in switchStates.Values)
        {
            if (!switchOn)
            {
                ToggleGate(false);
                return;
            }
        }
        ToggleGate(true);
    }
}
