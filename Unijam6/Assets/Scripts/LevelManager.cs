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
        switch (i)
        {
            case 1:
                SceneManager.LoadScene("Level1");
                break;
            case 2:
                SceneManager.LoadScene("Level2");
                break;
            case 3:
                SceneManager.LoadScene("Level3");
                break;
            case 4:
                SceneManager.LoadScene("Level4");
                break;
            case 5:
                SceneManager.LoadScene("Level5");
                break;
            case 6:
                SceneManager.LoadScene("Level6");
                break;
            case 7:
                SceneManager.LoadScene("Level7");
                break;
            case 8:
                SceneManager.LoadScene("Level8");
                break;
        }


    }
}
