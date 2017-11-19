using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularMenuManager : MonoBehaviour {

    GameObject CircularMenu;
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

        CircularMenu = transform.GetChild(0).gameObject;
        CircularMenu.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Tab))
        {
            if (!firsthit)
            {
                source.PlayOneShot(swapMenu, 1F);
                firsthit = true;
            }
            Time.timeScale = 0;
            player.GetComponent<Player>().enabled = false;
            CircularMenu.SetActive(true);
        }
        else
        {
            firsthit = false;
            Time.timeScale = 1;
            player.GetComponent<Player>().enabled = true;
            CircularMenu.SetActive(false);
        }
	}
}
