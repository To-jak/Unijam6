using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController2D))]
public class Player : MonoBehaviour
{
    public enum PlayerState { HealthBar, HeartBar };

    public PlayerState playerState = PlayerState.HealthBar;
    private bool courseDroite = true;

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

    Animator anim;

    void Start()
    {
        controller = GetComponent<PlayerController2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        health = GetComponent<Health>();

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchState();
        }

        float targetVelocityX;

        hurtTimer += Time.deltaTime;

        if (hurtTimer >= hurtCooldown)
        {
            if (playerState == PlayerState.HealthBar)
            {
                if (courseDroite)
                {
                    SetReposDroite();
                }
                else
                {
                    SetReposGauche();
                }
            }
            else
            {
                if (courseDroite)
                {
                    SetHeartReposDroite();
                }
                else
                {
                    SetHeartReposGauche();
                }
            }
            

            if (controller.collisions.above || controller.collisions.below)
            {
                velocity.y = 0;
            }

            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                if (playerState == PlayerState.HealthBar)
                {
                    SetCourseDroite();
                }
                else
                {
                    SetHeartCourseDroite();
                }
                courseDroite = true;
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                if (playerState == PlayerState.HealthBar)
                {
                    SetCourseGauche();
                }
                else
                {
                    SetHeartCourseGauche();
                }
                courseDroite = false;
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

    public void SwitchState()
    {
        ResetBool();
        switch (playerState)
        {
            case (PlayerState.HealthBar):
                playerState = PlayerState.HeartBar;
                anim.SetBool("HeartReposDroite", true);
                break;
            case (PlayerState.HeartBar):
                playerState = PlayerState.HealthBar;
                anim.SetBool("NormalReposDroite", true);
                break;
        }
        ResetBool();
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
        anim.SetBool("NormalSaut", false);
        anim.SetBool("NormalCourseDroite", false);
        anim.SetBool("NormalCourseGauche", false);
        anim.SetBool("NormalReposDroite", false);
        anim.SetBool("NormalReposGauche", false);
    }
    public void SetCourseDroite()
    {
        anim.SetBool("NormalSaut", false);
        anim.SetBool("NormalCourseDroite", true);
        anim.SetBool("NormalCourseGauche", false);
        anim.SetBool("NormalReposDroite", false);
        anim.SetBool("NormalReposGauche", false);
    }
    public void SetCourseGauche()
    {
        anim.SetBool("NormalSaut", false);
        anim.SetBool("NormalCourseDroite", false);
        anim.SetBool("NormalCourseGauche", true);
        anim.SetBool("NormalReposDroite", false);
        anim.SetBool("NormalReposGauche", false);
    }

    public void SetReposDroite()
    {
        anim.SetBool("NormalSaut", false);
        anim.SetBool("NormalCourseDroite", false);
        anim.SetBool("NormalCourseGauche", false);
        anim.SetBool("NormalReposDroite", true);
        anim.SetBool("NormalReposGauche", false);
    }
    public void SetReposGauche()
    {
        anim.SetBool("NormalSaut", false);
        anim.SetBool("NormalCourseDroite", false);
        anim.SetBool("NormalCourseGauche", false);
        anim.SetBool("NormalReposDroite", false);
        anim.SetBool("NormalReposGauche", true);
    }

    public void SetHeartCourseDroite()
    {
        anim.SetBool("HeartSaut", false);
        anim.SetBool("HeartCourseDroite", true);
        anim.SetBool("HeartCourseGauche", false);
        anim.SetBool("HeartReposDroite", false);
        anim.SetBool("HeartReposGauche", false);
    }
    public void SetHeartCourseGauche()
    {
        anim.SetBool("HeartSaut", false);
        anim.SetBool("HeartCourseDroite", false);
        anim.SetBool("HeartCourseGauche", true);
        anim.SetBool("HeartReposDroite", false);
        anim.SetBool("HeartReposGauche", false);
    }

    public void SetHeartReposDroite()
    {
        anim.SetBool("HeartSaut", false);
        anim.SetBool("HeartCourseDroite", false);
        anim.SetBool("HeartCourseGauche", false);
        anim.SetBool("HeartReposDroite", true);
        anim.SetBool("HeartReposGauche", false);
    }
    public void SetHeartReposGauche()
    {
        anim.SetBool("HeartSaut", false);
        anim.SetBool("HeartCourseDroite", false);
        anim.SetBool("HeartCourseGauche", false);
        anim.SetBool("HeartReposDroite", false);
        anim.SetBool("HeartReposGauche", true);
    }

    public void ResetBool()
    {
        anim.SetBool("HeartSaut", false);
        anim.SetBool("HeartCourseDroite", false);
        anim.SetBool("HeartCourseGauche", false);
        anim.SetBool("HeartReposDroite", false);
        anim.SetBool("HeartReposGauche", false);

        anim.SetBool("NormalSaut", false);
        anim.SetBool("NormalCourseDroite", false);
        anim.SetBool("NormalCourseGauche", false);
        anim.SetBool("NormalReposDroite", false);
        anim.SetBool("NormalReposGauche", false);

    }
}

