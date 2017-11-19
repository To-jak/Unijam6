using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLockController2D : TriggerController2D {

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
        }
    }
}
