using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void End()
    {
        SceneManager.LoadScene("End");
    }

    public void LoadLevel(int i)
    {
        string level = "Level" + i.ToString();
        SceneManager.LoadScene(level);


    }
}
