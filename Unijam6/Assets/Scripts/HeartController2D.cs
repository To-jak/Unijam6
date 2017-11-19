using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController2D : Controller2D {

    public virtual void Move(Vector3 velocity)
    {
        UpdateRaycastOrigins();
        collisions.Reset();
        
        HorizontalCollisions(ref velocity);
        VerticalCollisions(ref velocity);

        transform.Translate(velocity);
    }
}
