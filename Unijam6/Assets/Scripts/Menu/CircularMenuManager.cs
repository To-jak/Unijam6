using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularMenuManager : MonoBehaviour {

    GameObject CircularMenu;
    public GameObject player;

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
            Time.timeScale = 0;
            player.GetComponent<Player>().enabled = false;
            CircularMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            player.GetComponent<Player>().enabled = true;
            CircularMenu.SetActive(false);
        }
	}
}
