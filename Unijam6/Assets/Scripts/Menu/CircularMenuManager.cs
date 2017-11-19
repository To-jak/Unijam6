using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularMenuManager : MonoBehaviour {

    public GameObject circularMenu;
    public GameObject player;

    private AudioSource source;
    public AudioClip swapMenu;
    private bool firsthit;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        circularMenu.SetActive(false);
        circularMenu.GetComponent<CircularMenu>().manager = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!firsthit && swapMenu != null)
            {
                source.PlayOneShot(swapMenu, 1F);
                firsthit = true;
            }
            Time.timeScale = 0;
            player.GetComponent<Player>().enabled = false;
            circularMenu.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            HideMenu();
        }
	}

    public void HideMenu()
    {
        firsthit = false;
        Time.timeScale = 1;
        player.GetComponent<Player>().enabled = true;
        circularMenu.SetActive(false);
    }
}
