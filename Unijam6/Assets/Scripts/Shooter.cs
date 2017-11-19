using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {


    [SerializeField]
    public float frequency;

    [SerializeField]
    public GameObject projectile;

    [SerializeField]
    Vector3 direction;

    [SerializeField]
    float rotation;

	// Use this for initialization
	void Start () {

        InvokeRepeating("Fire", 2.0f, frequency);

    }
	
	// Update is called once per frame
	void Update () {

      
	}

    void Fire()
    {
        projectile.GetComponent<Projectile>().direction = direction;
        projectile.GetComponent<Projectile>().rotation = rotation;
        GameObject proj = Instantiate(projectile,transform.position,transform.localRotation);
    }
}
