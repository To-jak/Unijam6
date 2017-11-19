using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    
    int maxHealthUnits = 5;                     // combien de coeurs de vie ?
    int healthPointsPerUnit = 20;               // 1 coeur = combien de points de vie dans la barre de vie ?
    int maxHealthPoints;
    int currentHealthPoints;

    public HealthBar healthBar;
    public HeartBar heartBar;

    private AudioSource source;
    public AudioClip mort;
    public AudioClip hitNormal;
    public AudioClip hitHeart;

    public bool isDead = false;

    Animator anim;

    void Awake()
    {
        Init();
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    public void Init()
    {
        maxHealthPoints = maxHealthUnits * healthPointsPerUnit;
        currentHealthPoints = maxHealthPoints;

        healthBar.Init();
        UpdateHealthDisplay();

        healthBar.gameObject.SetActive(true);
        heartBar.gameObject.SetActive(false);
    }

    public int TakeDamage(int damage)
    {
        if (currentHealthPoints > 0)
        {
            damage = damage - damage % healthPointsPerUnit;                 // on ne garde que la partie entière de damage au cas où, pour toujours avoir un nombre entier de coeurs

            currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0, maxHealthPoints);

            Debug.Log("Took " + damage + " damage");
            Debug.Log("Current health " + currentHealthPoints);

            if (GetComponent<Player>().playerState == Player.PlayerState.HealthBar)
            {
                if (hitNormal != null)
                    source.PlayOneShot(hitNormal, 1F);
            }
            else
            {
                if (hitHeart != null)
                    source.PlayOneShot(hitHeart, 1F);
            }

            UpdateHealthDisplay();

            if (currentHealthPoints == 0)
            {
                isDead = true;
                if (mort != null)
                    source.PlayOneShot(mort, 1F);
                Invoke("Die", 1);
                return 0;
            }
        }
        return currentHealthPoints;
    }

    public void AddHealthUnits(int healthUnits)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints + healthUnits * healthPointsPerUnit, 0, maxHealthPoints);
        UpdateHealthDisplay();
    }

    public void RemoveHealthUnits(int healthUnits)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - healthUnits * healthPointsPerUnit, 0, maxHealthPoints);
        UpdateHealthDisplay();
        if (currentHealthPoints == 0)
        {
            isDead = true;
            if (mort != null)
                source.PlayOneShot(mort, 1F);
            Invoke("Die", 1);
        }
    }

    void UpdateHealthDisplay()
    {
        healthBar.UpdateDisplay((float)currentHealthPoints/(float)maxHealthPoints);
        heartBar.UpdateDisplay(currentHealthPoints/healthPointsPerUnit);
    }

    void Die()
    {
        
        if (GameManager.instance != null)
            GameManager.instance.PlayerDead();
    }
}
