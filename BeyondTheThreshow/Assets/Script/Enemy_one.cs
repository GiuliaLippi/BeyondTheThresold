using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_one : MonoBehaviour

{
    [Header("Patrol Points")]
    public Transform pointA;
    public Transform pointB;
    public Transform targetPoint; // Ora è pubblico
    public float speed = 2f;

    [Header("Damage")]
    public int damage = 10;

    void Start()
    {
        // Inizia andando verso il punto A
        targetPoint = pointA;
    }

    void Update()
    {
        // Movimento verso il punto target
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Se raggiunge il target, cambia direzione
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA;
        }
    }

    // Infligge danno al giocatore se lo tocca
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovementt playerHealth = collision.gameObject.GetComponent<PlayerMovementt>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
