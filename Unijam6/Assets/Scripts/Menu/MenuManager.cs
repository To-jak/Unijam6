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

    public void GoToScene(int i)
    {
        SceneManager.LoadScene(i);
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
