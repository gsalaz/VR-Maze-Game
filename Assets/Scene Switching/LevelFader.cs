using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFader : MonoBehaviour
{
    //fade animation
    public Animator animator;
    //level to load 
    private int levelToLoad;

    //set spawn point when going back to maze
    public void SetSpawnPoint (int locNum) 
    {
        SpawnPoints.puzzleNumComplete = locNum;
    }

    //scene to fade into
    public void FadeToLevel (int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
