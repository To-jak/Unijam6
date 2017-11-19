using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLockController2D : TriggerController2D {

    protected override void ProcessCollision(GameObject other, Vector3 dir)
    {
        triggered = true;
        GetComponent<BoxCollider2D>().enabled = false;
        other.GetComponent<Heart>().enabled = false;
        other.transform.position = transform.position;
    }
}
