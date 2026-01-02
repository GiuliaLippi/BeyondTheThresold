using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementt : MonoBehaviour
{
    // -------- VARIABILI PUBBLICHE --------
    public float speed = 5f;             // velocità movimento
    public int maxHealth = 100;          // vita massima
    public int currentHealth;            // vita corrente
    public float attackCooldown = 0.5f;  // tempo tra attacchi
    public GameObject attackPrefab;      // prefab dell'attacco (es. hitbox)
    public Transform attackPoint;        // punto da cui parte l'attacco

    // -------- VARIABILI PRIVATE --------
    private Rigidbody2D rb;
    private Vector2 moveDirection = Vector2.right; // direzione iniziale
    private float lastAttackTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        HandleMovementInput();
        RotateSprite();
        HandleAttackInput();
    }

    void FixedUpdate()
    {
        // Movimento continuo: non si ferma mai
        rb.velocity = moveDirection * speed;
    }

    // ---------------- MOVIMENTO ----------------
    void HandleMovementInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D o frecce
        float moveY = Input.GetAxisRaw("Vertical");   // W/S o frecce

        Vector2 input = new Vector2(moveX, moveY);

        if (input != Vector2.zero)
        {
            moveDirection = input.normalized; // cambia direzione solo se premi un tasto
        }
    }

    void RotateSprite()
    {
        if (moveDirection != Vector2.zero)
        {
            // Ruota il player verso la direzione di movimento
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90); // -90 se lo sprite punta verso l'alto
        }
    }

    // ---------------- ATTACCO ----------------
    void HandleAttackInput()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        if (attackPrefab != null && attackPoint != null)
        {
            Instantiate(attackPrefab, attackPoint.position, Quaternion.identity);
        }
        Debug.Log("Player attacca!");
    }

    // ---------------- VITA ----------------
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player è morto!");
        gameObject.SetActive(false);
    }
}