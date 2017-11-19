using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCamera : MonoBehaviour {

    public Transform target;
    public float minY, maxY;
    public Vector3 deadZoneOrigin;
    public float deadZoneHeight;
    public float deadZoneWidth;
    public float smoothing;
    Vector3 currentVelocity;

    private void Update()
    {
        Vector3 targetPos = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minY, maxY), transform.position.z);

        if (targetPos.x < transform.position.x + deadZoneOrigin.x - deadZoneWidth / 2f
            || targetPos.x > transform.position.x + deadZoneOrigin.x + deadZoneWidth / 2f
            || targetPos.y < transform.position.y + deadZoneOrigin.y - deadZoneHeight / 2f
            || targetPos.y > transform.position.y + deadZoneOrigin.y + deadZoneHeight / 2f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPos + deadZoneOrigin, ref currentVelocity, smoothing);
        }
    }
}
