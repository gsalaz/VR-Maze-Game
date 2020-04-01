using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lives : MonoBehaviour
{
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;
    private int lives;

    public GameObject gameOverScreen;
    public GameObject scoreScreen;

    public BackgroundMusic backgroundMusic;
    public UnityEvent endGame;
    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        backgroundMusic = FindObjectOfType<BackgroundMusic>();
    }

    public int getLives()
    {
        return lives;
    }

    public void lossLife() {
        if (lives == 3)
        {
            life1.SetActive(false);
        }
        else if (lives == 2)
        {
            life2.SetActive(false);
        }
        else
        {
            life3.SetActive(false);
            gameOverScreen.SetActive(true);
            scoreScreen.SetActive(false);
            StartCoroutine(PauseGame(3F));
        }
            
        lives -= 1;

    }

    public IEnumerator PauseGame(float pauseTime)//pause game
    {
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + pauseTime;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1f;

        Scoring.score = 0;

        backgroundMusic.bg.volume = 0.5F;
        endGame.Invoke();
    }

}
