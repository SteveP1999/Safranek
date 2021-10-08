using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float maxHealth = 100f;
    private float currentHealth = 50f;
    [SerializeField] private HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage(10f);
        }
    }


    void takeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.setHealtH(currentHealth);
    }
}
