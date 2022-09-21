using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFight : MonoBehaviour
{
    public LayerMask playerLayers;
    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    void OnCollisionEnter2D(Collision2D player)
    {
        if (player.gameObject.name == "Player")
        {
            if (Time.time >= nextAttackTime)
            {
                player.gameObject.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
}
