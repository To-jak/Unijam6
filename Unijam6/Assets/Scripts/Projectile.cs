using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    public Vector3 direction;

    [SerializeField]
    public int damage;

    [SerializeField]
    public float velocity;

    public Controller2D controller2D;
    // Use this for initialization
    void Start () {
		controller2D = GetComponent<ProjectileController2D>();
    }
	
	// Update is called once per frame
	void Update () {

        controller2D.Move(direction * velocity);
        
	}
}
