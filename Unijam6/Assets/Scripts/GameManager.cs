using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    GameObject player;
    Vector3 startPosition;

    public AudioClip repop;
    public AudioClip endSound;
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            throw new System.Exception("More than one instance of GameManager");

        player = GameObject.FindGameObjectWithTag("Player");

        startPosition = GameObject.FindGameObjectWithTag("Start").transform.position;
        player.transform.position = startPosition;
    }

    void InitPlayer()
    {
        player.GetComponent<Player>().enabled = true;
        player.GetComponent<Player>().Init();
        player.GetComponent<Health>().Init();
        player.transform.position = startPosition;
    }

    public void PlayerDead()
    {
        player.GetComponent<Player>().enabled = false;

        GameObject[] hearts = GameObject.FindGameObjectsWithTag("Heart");
        foreach (GameObject heart in hearts)
        {
            Destroy(heart, 1f);
        }

        TriggerLockController2D[] triggerLocks = FindObjectsOfType(typeof(TriggerLockController2D)) as TriggerLockController2D[];
        foreach (TriggerLockController2D triggerLock in triggerLocks)
        {
            GameObject newHeart = triggerLock.key;
            triggerLock.key = null;
            Destroy(newHeart);
            triggerLock.Clear();
        }

        TriggerObject[] triggers = FindObjectsOfType(typeof(TriggerObject)) as TriggerObject[];
        foreach (TriggerObject trigger in triggers)
        {
            trigger.Trigger(false);
        }

        if (repop != null)
            source.PlayOneShot(repop, 1F);
        Invoke("InitPlayer", 1f);
    }

    public void EndLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = (currentLevelIndex == SceneManager.sceneCountInBuildSettings - 1) ? 0 : currentLevelIndex + 1;
        if (endSound != null)
            source.PlayOneShot(endSound, 1F);
        MenuManager.instance.GoToScene(nextIndex);
    }
}
