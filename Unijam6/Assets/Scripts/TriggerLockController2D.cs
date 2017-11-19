using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLockController2D : TriggerController2D {

    private AudioSource source;
    public AudioClip interrupteur;

    void Awake()
    {

        source = GetComponent<AudioSource>();

    }

    protected override void HorizontalCollisions(float directionX)
    {
        float rayLength = skinWidth + 0.5f; ;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit)
            {
                rayLength = hit.distance;

                collisions.left = directionX == -1;
                collisions.right = directionX == 1;

                ProcessCollision(hit.collider.gameObject, directionX * Vector3.right);
            }
        }
    }

    protected override void VerticalCollisions(float directionY)
    {
        float rayLength = skinWidth + 0.5f;

        int numberOfHits = 0;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit)
            {
                //rayLength = hit.distance;

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;

                numberOfHits++;

                ProcessCollision(hit.collider.gameObject, directionY * Vector3.up);
            }
        }
    }

    protected override void ProcessCollision(GameObject other, Vector3 dir)
    {
        if (!triggered)
        {
            triggered = true;
            other.layer = 0;
            other.GetComponent<Heart>().enabled = false;
            other.GetComponent<HeartController2D>().enabled = false;
            other.transform.position = transform.position;
            anim.SetBool("startAnimation", true);
            source.PlayOneShot(interrupteur, 1F);
        }
    }
}
