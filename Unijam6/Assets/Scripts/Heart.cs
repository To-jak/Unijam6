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

    void Start () {
        controller = GetComponent<Controller2D>();
    }
	
	void Update () {
        if (state == HeartState.inWorld)
        {
            if (controller.collisions.above || controller.collisions.below)
            {
                Debug.Log("alalalla");
                velocity.y = 0;
            }

            float targetVelocityX = 0;

            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, 0.2f);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    public void ChangeState(HeartState state)
    {
        this.state = state;
    }

    IEnumerator MoveToPos (Vector3 newPos)
    {
        // Pour revenir dans la barre
        yield return null;
    }
}
