using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_three : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Attack Settings")]
    public GameObject projectilePrefab; // Prefab del proiettile
    public float detectionRange = 5f;   // Raggio in cui il nemico rileva il giocatore
    public float fireRate = 1f;         // Proiettili per secondo
    public float projectileSpeed = 5f;  // Velocità del proiettile
    public int damage = 10;

    private float fireCooldown = 0f;

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                // Se il cooldown è scaduto, spara
                if (fireCooldown <= 0f)
                {
                    ShootAtPlayer();
                    fireCooldown = 1f / fireRate; // reset cooldown
                }
            }
        }

        // Aggiorna il cooldown
        if (fireCooldown > 0f)
        {
            fireCooldown -= Time.deltaTime;
        }
    }

    void ShootAtPlayer()
    {
        if (projectilePrefab != null)
        {
            // Crea il proiettile
            GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // Calcola direzione verso il giocatore
            Vector2 direction = (player.position - transform.position).normalized;

            // Muovi il proiettile
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * projectileSpeed;
            }

            // Imposta il danno se lo script del proiettile lo prevede
            Projectile projectileScript = proj.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.damage = damage;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Mostra il raggio di rilevamento nel Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
