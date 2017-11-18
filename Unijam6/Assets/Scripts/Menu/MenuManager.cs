using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public static MenuManager instance;

    public void Start()
    {
        if (instance == null)
            instance = this;
        else
            throw new System.Exception("More than one instance of MenuManager");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GoToLevelSelection()
	{
		SceneManager.LoadScene("LevelSelection");
	}

    public void GoToLevel(int i)
    {
        string level = "Level" + i.ToString();
        SceneManager.LoadScene(level);
    }

    public void Quit()
	{

		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
