using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController2D : Controller2D {

    public bool up, down, right, left;
    public bool oneShotTrigger;
    public bool triggered = false;

    protected Animator anim;

    public float cooldown = 0.5f;
    float timer;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        timer += Time.deltaTime;

        UpdateRaycastOrigins();
        collisions.Reset();
        
        if (down) VerticalCollisions(-1f);
        if (up) VerticalCollisions(1f);
        if (left) HorizontalCollisions(-1f);
        if (right) HorizontalCollisions(1f);
    }

    protected virtual void HorizontalCollisions(float directionX)
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
            /*else
            {
                if (!oneShotTrigger && timer >= cooldown)
                {
                    triggered = false;
                    anim.SetBool("restartAnimation", true);
                }
            }*/
        }
    }

    protected virtual void VerticalCollisions(float directionY)
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
        if (triggered && numberOfHits == 0 && !oneShotTrigger && timer >= cooldown)
        {
            triggered = false;
            anim.SetBool("restartAnimation", true);
        }
    }

    protected override void ProcessCollision(GameObject other, Vector3 dir)
    {
        if (!triggered)
        {
            triggered = true;
            timer = 0f;
            anim.SetBool("startAnimation", true);
        }
        
    }
}
