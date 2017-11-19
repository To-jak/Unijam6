﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : TriggerObject {

    public Transform closedPosition;
    public Transform openPosition;
    public Transform door;
    public float openingDuration;

    bool isMoving;

    public override void Trigger(bool triggered)
    {
        if (!isMoving)
        {
            Vector3 newPosition = (triggered) ? openPosition.position : closedPosition.position;
            isMoving = true;
            StartCoroutine("MoveTo", newPosition);
            this.triggered = triggered;
        }
    }

    IEnumerator MoveTo (Vector3 newPosition)
    {
        float t = 0f;
        while (t < openingDuration)
        {
            door.position = Vector3.Lerp(door.position, newPosition, Mathf.Sin(t / (Mathf.PI / 2)));
            t += Time.deltaTime;
            yield return null;
        }
        isMoving = false;
    }
}
