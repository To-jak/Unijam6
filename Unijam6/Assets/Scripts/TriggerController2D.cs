using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController2D : Controller2D {

    public bool triggered = false;

    public void Update()
    {
        UpdateRaycastOrigins();
        collisions.Reset();
        
        VerticalCollisions();
        HorizontalCollisions(-1f);
        HorizontalCollisions(1f);
    }

    protected void HorizontalCollisions(float directionX)
    {
        float rayLength = skinWidth;

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

    protected void VerticalCollisions()
    {
        float directionY = 1;
        float rayLength = skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit)
            {
                rayLength = hit.distance;

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;

                ProcessCollision(hit.collider.gameObject, directionY * Vector3.up);
            }
        }
    }

    protected override void ProcessCollision(GameObject other, Vector3 dir)
    {
        switch (other.tag)
        {
            default:
                triggered = true;
                break;
        }
    }
}
