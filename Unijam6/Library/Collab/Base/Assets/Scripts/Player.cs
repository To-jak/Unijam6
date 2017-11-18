using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController2D))]
public class Player : MonoBehaviour
{
    public bool saut = false;
    public bool courseGauche = false;
    public bool courseDroite = false;
    public bool repos = false;

    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 6;

    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    PlayerController2D controller;

    Health health;

    float hurtCooldown = 0.5f;
    float hurtTimer;

    void Start()
    {
        controller = GetComponent<PlayerController2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        health = GetComponent<Health>();
    }

    void Update()
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //      Gestion joueur                                                                                  //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // changer sprite, valeurs de vitesse/saut, healthbar/hearts
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //      Déplacement                                                                                     //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        float targetVelocityX;

        hurtTimer += Time.deltaTime;

        if (hurtTimer >= hurtCooldown)
        {
            SetRepos();
            if (controller.collisions.above || controller.collisions.below)
            {
                velocity.y = 0;
            }

            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                SetCourseDroite();
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                SetCourseGauche();
            }

            if (Input.GetButtonDown("Jump") && controller.collisions.below)
            {
                velocity.y = jumpVelocity;
                SetSaut();
            }

            targetVelocityX = input.x * moveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        } else
        {
            targetVelocityX = 0f;
        }

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Hurt(int damage, Vector3 dir)
    {
        if (hurtTimer >= hurtCooldown)
        {
            int currentHealth = health.TakeDamage(damage);
            hurtTimer = 0f;
            if (currentHealth > 0)
            {
                velocity = dir * jumpVelocity;
            }
        }
    }

    public void SetSaut()
    {
        saut = true;
        courseDroite = false;
        courseGauche = false;
        repos = false;
    }
    public void SetCourseDroite()
    {
        saut = false;
        courseDroite = true;
        courseGauche = false;
        repos = false;
    }
    public void SetCourseGauche()
    {
        saut = false;
        courseDroite = false;
        courseGauche = true;
        repos = false;
    }

    public void SetRepos()
    {
        saut = false;
        courseDroite = false;
        courseGauche = false;
        repos = true;
    }
}

