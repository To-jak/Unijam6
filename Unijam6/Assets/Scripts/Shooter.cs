using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {


    [SerializeField]
    public float frequency;

    [SerializeField]
    public GameObject projectile;

	// Use this for initialization
	void Start () {

        InvokeRepeating("Fire", 2.0f, frequency);

    }
	
	// Update is called once per frame
	void Update () {

      
	}

    void Fire()
    {
        GameObject proj = Instantiate(projectile,transform);
    }
}
