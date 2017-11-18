using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    GameObject player;
    Vector3 startPosition;

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            throw new System.Exception("More than one instance of GameManager");

        player = GameObject.FindGameObjectWithTag("Player");

        startPosition = GameObject.FindGameObjectWithTag("Start").transform.position;
    }

    void InitPlayer()
    {
        player.transform.position = startPosition;
        player.GetComponent<Health>().Init();
    }

    public void PlayerDead()
    {
        InitPlayer();
    }

    public void EndLevel()
    {
        string currentLevelName = SceneManager.GetActiveScene().name;
        int currentLevelIndex = currentLevelName[currentLevelName.Length - 1];
        
        try
        {
            MenuManager.instance.GoToLevel(currentLevelIndex + 1);
        } catch (System.Exception)
        {
            MenuManager.instance.GoToMenu();
        }
    }
}
