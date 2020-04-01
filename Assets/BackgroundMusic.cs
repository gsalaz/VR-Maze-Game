using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource bg;
    public static BackgroundMusic instance;

    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            bg.volume = 0.5F;
        }
        else
        {
            Destroy(this.gameObject);
        }
            

    }

}
