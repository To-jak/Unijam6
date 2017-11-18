using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    
    int maxHealthUnits = 5;                     // combien de coeurs de vie ?
    int healthPointsPerUnit = 20;               // 1 coeur = combien de points de vie dans la barre de vie ?
    int maxHealthPoints;
    int currentHealthPoints;

    HealthBar healthBar;
    HeartBar heartBar;

    void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
        heartBar = GameObject.FindGameObjectWithTag("HeartBar").GetComponent<HeartBar>();

        healthBar.gameObject.SetActive(false);

        Init();
    }

    public void Init()
    {
        maxHealthPoints = maxHealthUnits * healthPointsPerUnit;
        currentHealthPoints = maxHealthPoints;

        healthBar.Init();
        UpdateHealthDisplay();
    }

    public int TakeDamage(int damage)
    {
        damage = damage - damage % healthPointsPerUnit;                 // on ne garde que la partie entière de damage au cas où, pour toujours avoir un nombre entier de coeurs

        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0, maxHealthPoints);

        Debug.Log("Took " + damage + " damage");
        Debug.Log("Current health " + currentHealthPoints);

        UpdateHealthDisplay();

        if (currentHealthPoints == 0)
        {
            Die();
            return 0;
        }
        return currentHealthPoints;
    }

    public void AddHealthUnits(int healthUnits)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints + healthUnits * healthPointsPerUnit, 0, maxHealthPoints);
        UpdateHealthDisplay();
    }

    public void AddHealthPoints(int healthPoints)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints + healthPoints, 0, maxHealthPoints);
        UpdateHealthDisplay();
    }

    void UpdateHealthDisplay()
    {
        healthBar.UpdateDisplay((float)currentHealthPoints/(float)maxHealthPoints);
        heartBar.UpdateDisplay(currentHealthPoints/healthPointsPerUnit);
    }

    void Die()
    {
        GameManager.instance.PlayerDead();
    }
}
