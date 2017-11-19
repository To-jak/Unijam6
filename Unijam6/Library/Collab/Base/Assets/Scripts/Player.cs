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

    private float actualScale;
    private bool lancer;

    Animator anim;

    void Start()
    {
        controller = GetComponent<PlayerController2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        health = GetComponent<Health>();

        anim = GetComponent<Animator>();
        actualScale = transform.localScale.x;
    }

    void Update()
    {
        lancer = GetComponent<Health>().heartBar.throwing;
        if (courseDroite)
        {
            transform.localScale = new Vector3(actualScale, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-actualScale, transform.localScale.y, transform.localScale.z);
        }

        float targetVelocityX;

        hurtTimer += Time.deltaTime;

        if (hurtTimer >= hurtCooldown)
        {
            if (playerState == PlayerState.HealthBar)
            {
                SetRepos();
            }
            else
            {
                SetHeartRepos();
            }
            

            if (controller.collisions.above || controller.collisions.below)
            {
                velocity.y = 0;
            }

            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                if (playerState == PlayerState.HealthBar)
                {
                    SetCourse();
                }
                else
                {
                    SetHeartCourse();
                }
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    courseDroite = true;
                }
                else
                {
                    courseDroite = false;
                }
            }
            

            if (Input.GetButtonDown("Jump") && controller.collisions.below)
            {
                velocity.y = jumpVelocity;
                if (playerState == PlayerState.HealthBar)
                {
                    SetSaut();
                }
                else
                {
                    SetHeartSaut();
                }
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

    public void SwitchState(PlayerState state)
    {
        ResetBool();
        switch (state)
        {
            case (PlayerState.HealthBar):
                playerState = PlayerState.HeartBar;
                anim.SetBool("HeartRepos", true);
                break;
            case (PlayerState.HeartBar):
                playerState = PlayerState.HealthBar;
                anim.SetBool("NormalRepos", true);
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
        anim.SetBool("NormalSaut", true);
        anim.SetBool("NormalCourse", false);
        anim.SetBool("NormalRepos", false);
    }
    public void SetCourse()
    {
        anim.SetBool("NormalSaut", false);
        anim.SetBool("NormalCourse", true);
        anim.SetBool("NormalRepos", false);
    }

    public void SetRepos()
    {
        anim.SetBool("NormalSaut", false);
        anim.SetBool("NormalCourse", false);
        anim.SetBool("NormalRepos", true);
    }

    public void SetHeartCourse()
    {
        anim.SetBool("HeartSaut", false);
        anim.SetBool("HeartCourse", true);
        anim.SetBool("HeartRepos", false);
    }

    public void SetHeartRepos()
    {
        anim.SetBool("HeartSaut", false);
        anim.SetBool("HeartCourse", false);
        anim.SetBool("HeartRepos", true);
    }

    public void SetHeartSaut() {
        anim.SetBool("HeartSaut", true);
        anim.SetBool("HeartCourse", false);
        anim.SetBool("HeartRepos", false);
    }

    public void ResetBool()
    {
        anim.SetBool("HeartSaut", false);
        anim.SetBool("HeartCourse", false);
        anim.SetBool("HeartRepos", false);

        anim.SetBool("NormalSaut", false);
        anim.SetBool("NormalCourse", false);
        anim.SetBool("NormalRepos", false);

    }
}

