using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void GoToWorld() {
        SceneManager.LoadScene("World");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ToPuzzleA()
    {
        SceneManager.LoadScene("PuzzleA");
    }
    public void ToPuzzleB()
    {
        SceneManager.LoadScene("PuzzleB");
    }
    public void ToPuzzleC()
    {
        SceneManager.LoadScene("PuzzleC");
    }

}
