using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class Scoring : MonoBehaviour
{
    public static int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endScoreText;

    public delegate void ScoreEvents();
    public static ScoreEvents scoreEvent;

    void Start()
    {
        scoreText.text = "Score: " + BarrellHit.numHit;
        endScoreText.text = "You scored " + BarrellHit.numHit + " points!";
        scoreEvent += UpScore;
    }

    // Start is called before the first frame updat

    public void UpScore()
    {
        scoreText.text = "Score: " + BarrellHit.numHit;
        endScoreText.text = "You scored " + BarrellHit.numHit + " points!";
    }

    void Update()
    {
        scoreText.text = "Score: " + BarrellHit.numHit;
        endScoreText.text = "You scored " + BarrellHit.numHit + " points!";
    }


}
