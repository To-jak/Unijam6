using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController2D : Controller2D {

    public AudioClip coeurHitWall;
    private AudioSource source;
    private bool firstHit = false;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public override void Move(Vector3 velocity)
    {
        UpdateRaycastOrigins();
        collisions.Reset();
        
        HorizontalCollisions(ref velocity);
        VerticalCollisions(ref velocity);

        transform.Translate(velocity);
    }

    protected override void ProcessCollision(GameObject other, Vector3 dir)
    {
        if (!firstHit)
        {
            source.PlayOneShot(coeurHitWall, 1F);
            firstHit = true;
            //Debug.Log("Hit");
        }
    }
}
