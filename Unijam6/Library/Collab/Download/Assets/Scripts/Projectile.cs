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

    [SerializeField]
    public float rotation;

    public ParticleSystem impactAnimation;
    bool hasImpacted;

    private ProjectileController2D controller2D;
    // Use this for initialization
    void Start () {
		controller2D = GetComponent<ProjectileController2D>();
        transform.Rotate(0, 0, rotation);
    }
	
	// Update is called once per frame
	void Update () {

        controller2D.Move(direction * velocity);
	}

    public void Impact()
    {
        if (!hasImpacted)
        {
            if (impactAnimation != null)
            {
                hasImpacted = true;
                ParticleSystem newImpact = Instantiate(impactAnimation, transform.position, transform.rotation);
                Destroy(newImpact, 3f);
            }
        }
    }
}
