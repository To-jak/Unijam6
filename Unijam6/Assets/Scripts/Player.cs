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
    public float moveSpeed = 6;

    public LayerMask healthBarCollisionMask;
    public LayerMask heartBarCollisionMask;

    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    PlayerController2D controller;

    Health health;

    float hurtCooldown = 0.5f;
    float hurtTimer;

    private float actualScale;

    Animator anim;
    private bool lancer;

    public AudioClip jumpBarre;
    private AudioSource source;

    private bool dead = false;

    private bool stepPlayed = false;
    private bool step1 = true;
    private int deltaLast = 0;
    private int deltasound = 5;
    public AudioClip step1Normal;
    public AudioClip step2Normal;
    public AudioClip step1Heart;
    public AudioClip step2Heart;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        controller = GetComponent<PlayerController2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        health = GetComponent<Health>();

        anim = GetComponent<Animator>();
        actualScale = transform.localScale.x;
    }

    public void Init()
    {
        SwitchState(PlayerState.HealthBar);
    }

    void Update()
    {
        lancer = GetComponent<Health>().heartBar.throwing;
        dead = GetComponent<Health>().isDead;
        if (anim.GetBool("HeartLancer"))
        {
            SetHeartRepos();
        }
        if (anim.GetBool("NormalMort") || anim.GetBool("HeartMort"))
        {
            dead = false;
            GetComponent<Health>().isDead = false;
        }
        else
        {
            dead = GetComponent<Health>().isDead;
            if (dead)
            {
                if (playerState == PlayerState.HealthBar)
                {
                    SetMort();
                }
                else
                {
                    SetHeartMort();
                }
            }
        }

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
            if (controller.collisions.below)
            {
                if (playerState == PlayerState.HealthBar)
                {
                    SetRepos();
                }
                else
                {
                    if (lancer)
                    {
                        SetHeartLancerRepos();
                    }
                    else
                    {
                        SetHeartRepos();
                    }
                }
            }
            

            if (controller.collisions.above || controller.collisions.below)
            {
                velocity.y = 0;
            }

            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                if (controller.collisions.below)
                {
                    SoundCourse();
                }
                if (playerState == PlayerState.HealthBar)
                {
                    SetCourse();
                }
                else
                {
                    if (lancer)
                    {
                        SetHeartLancerCourse();
                    }
                    else
                    {
                        SetHeartCourse();
                    }
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
                
                if (playerState == PlayerState.HealthBar)
                {
                    SetSaut();
                    velocity.y = jumpVelocity;
                    source.PlayOneShot(jumpBarre, 1F);
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
                playerState = PlayerState.HealthBar;
                controller.collisionMask = healthBarCollisionMask;
                anim.SetBool("NormalRepos", true);
                break;
            case (PlayerState.HeartBar):
                playerState = PlayerState.HeartBar;
                controller.collisionMask = heartBarCollisionMask;
                anim.SetBool("HeartRepos", true);
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
        anim.SetBool("NormalMort", false);
    }
    public void SetCourse()
    {
        anim.SetBool("NormalSaut", false);
        anim.SetBool("NormalCourse", true);
        anim.SetBool("NormalRepos", false);
        anim.SetBool("NormalMort", false);
    }

    public void SetRepos()
    {
        anim.SetBool("NormalSaut", false);
        anim.SetBool("NormalCourse", false);
        anim.SetBool("NormalRepos", true);
        anim.SetBool("NormalMort", false);
    }
    public void SetMort()
    {
        anim.SetBool("NormalSaut", false);
        anim.SetBool("NormalCourse", false);
        anim.SetBool("NormalRepos", false);
        anim.SetBool("NormalMort", true);
    }

    public void SetHeartCourse()
    {
        anim.SetBool("HeartCourse", true);
        anim.SetBool("HeartRepos", false);
        anim.SetBool("HeartLancerRepos", false);
        anim.SetBool("HeartLancerCourse", false);
        anim.SetBool("HeartMort", false);
    }

    public void SetHeartRepos()
    {
        anim.SetBool("HeartCourse", false);
        anim.SetBool("HeartRepos", true);
        anim.SetBool("HeartLancerRepos", false);
        anim.SetBool("HeartLancerCourse", false);
        anim.SetBool("HeartMort", false);
    }
    public void SetHeartSaut()
    {
        anim.SetBool("HeartSaut", true);
        anim.SetBool("HeartMort", false);
    }

    public void SetHeartLancerRepos() {

        anim.SetBool("HeartCourse", false);
        anim.SetBool("HeartRepos", false);
        anim.SetBool("HeartLancerRepos", true);
        anim.SetBool("HeartLancerCourse", false);
        anim.SetBool("HeartMort", false);
    }

    public void SetHeartLancerCourse()
    {
        anim.SetBool("HeartRepos", false);
        anim.SetBool("HeartCourse", false);
        anim.SetBool("HeartLancerRepos", false);
        anim.SetBool("HeartLancerCourse", true);
        anim.SetBool("HeartMort", false);
    }

    public void SetHeartMort()
    {
        anim.SetBool("HeartRepos", false);
        anim.SetBool("HeartCourse", false);
        anim.SetBool("HeartLancerRepos", false);
        anim.SetBool("HeartLancerCourse", false);
        anim.SetBool("HeartMort", true);
    }

    public void ResetBool()
    {
        anim.SetBool("HeartLancerRepos", false);
        anim.SetBool("HeartLancerCourse", false);
        anim.SetBool("HeartCourse", false);
        anim.SetBool("HeartRepos", false);

        anim.SetBool("NormalSaut", false);
        anim.SetBool("NormalCourse", false);
        anim.SetBool("NormalRepos", false);
        anim.SetBool("NormalMort", false);

    }

    public void SoundCourse()
    {
        if (playerState == PlayerState.HealthBar)
        {
            if (stepPlayed)
            {
                deltaLast++;
                if (deltaLast > deltasound)
                {
                    deltaLast = 0;
                    stepPlayed = false;
                }
            }
            else
            {
                if (step1)
                {
                    step1 = false;
                    stepPlayed = true;
                    source.PlayOneShot(step1Normal, 0.5F);
                }
                else
                {
                    step1 = true;
                    stepPlayed = true;
                    source.PlayOneShot(step2Normal, 0.5F);
                }
            }
        }
        else
        {

            if (stepPlayed)
            {
                deltaLast++;
                if (deltaLast > deltasound)
                {
                    deltaLast = 0;
                    stepPlayed = false;
                }
            }
            else
            {
                if (step1)
                {
                    step1 = false;
                    stepPlayed = true;
                    source.PlayOneShot(step1Heart, 0.5F);
                }
                else
                {
                    step1 = true;
                    stepPlayed = true;
                    source.PlayOneShot(step2Heart, 0.5F);
                }
            }
        }
    }

}

