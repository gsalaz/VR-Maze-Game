using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    //Postion of player
    public Transform player;
    //postion of different spawn points
    public Transform spawn1, spawn2, spawn3;
    //To hold puzzle that was just completed
    public static int puzzleNumComplete;

    // Start is called before the first frame update
    public void Start()
    {
        if (puzzleNumComplete == 1)//after puzzle 1
        {
            player.position = spawn1.position;
        }
        if (puzzleNumComplete == 2)//after puzzle 2
        {
            player.position = spawn2.position;
        }
        if (puzzleNumComplete == 3)//after puzzle 3
        {
            player.position = spawn3.position;
        }
    }
}
