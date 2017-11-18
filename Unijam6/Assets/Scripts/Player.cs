using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{

    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 6;

    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    Controller2D controller;

    int maxHealthUnits = 5;                     // combien de coeurs de vie ?
    int healthPointsPerUnit = 20;               // 1 coeur = combien de points de vie dans la barre de vie ?
    int maxHealthPoints;
    int currentHealthPoints;

    void Start()
    {
        controller = GetComponent<Controller2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        maxHealthPoints = maxHealthUnits * healthPointsPerUnit;
        currentHealthPoints = maxHealthPoints;
    }

    void Update()
    {

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void TakeDamage (int damage)
    {
        Debug.Log("Took " + damage + " damage");

        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0, maxHealthPoints);

        if (currentHealthPoints == 0)
            Die();
    }

    void Die ()
    {
        GameManager.instance.PlayerDead();
    }
}

