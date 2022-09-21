using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public CameraShake camShake;
    Animator animator;
    public int maxHealth = 100;
    int currentHealth;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        camShake.Shake();
        //animator.SetBool("IsDamage", true);
        //Invoke("setDamageToFalse", 0.8f);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Destroy(gameObject);
    }

    void setDamageToFalse()
    {
        animator.SetBool("IsDamage", false);
    }
}
