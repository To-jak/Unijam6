using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Heart : MonoBehaviour{

    public enum HeartState { inBar, onPlayer, inWorld };

    public HeartState state = HeartState.inBar;

    float gravity = -10f;
    Vector3 velocity;
    float velocityXSmoothing;

    Controller2D controller;

    public bool catchable;

    void Start () {
        controller = GetComponent<Controller2D>();

        Invoke("SetCatchable", 1f);
    }
	
	void Update () {
        if (state == HeartState.inWorld)
        {
            if (controller.collisions.above || controller.collisions.below)
            {
                velocity.y = 0;
            }

            float targetVelocityX = 0;

            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? 0.4f : 2f);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    public void SetVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    public void SetState(HeartState state)
    {
        this.state = state;
    }

    void SetCatchable()
    {
        catchable = true;
    }
}
