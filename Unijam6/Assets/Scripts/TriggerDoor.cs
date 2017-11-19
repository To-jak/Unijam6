using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : TriggerObject {

    public Transform closedPosition;
    public Transform openPosition;
    public Transform door;
    public float openingDuration;

    bool isMoving;

    private AudioSource source;
    public AudioClip doorSound;

    void Awake()
    {

        source = GetComponent<AudioSource>();

    }

    public override void Trigger(bool triggered)
    {
        if (!isMoving)
        {
            Vector3 newPosition = (triggered) ? openPosition.position : closedPosition.position;
            isMoving = true;
            if (doorSound != null)
                source.PlayOneShot(doorSound, 1F);
            StartCoroutine("MoveTo", newPosition);
            this.triggered = triggered;
        }
    }

    IEnumerator MoveTo (Vector3 newPosition)
    {
        float t = 0f;
        while (t < openingDuration)
        {
            door.position = Vector3.Lerp(door.position, newPosition, Mathf.Sin(t / openingDuration * (Mathf.PI / 2)));
            t += Time.deltaTime;
            yield return null;
        }
        isMoving = false;
    }
}
