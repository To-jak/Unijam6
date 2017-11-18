using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = GameObject.FindGameObjectWithTag("Start").transform.position;
    }
}
