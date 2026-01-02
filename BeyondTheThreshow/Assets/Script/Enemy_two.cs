using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_two : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform player;

    public float patrolSpeed = 2f;
    public float attackSpeed = 3f;
    public float detectionRange = 5f;
    public int damage = 10;

    public Transform targetPoint;

    void Start()
    {
        targetPoint = pointA; // Inizia verso A
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                // Insegue il giocatore
                MoveTowards(player.position, attackSpeed);
            }
            else
            {
                // Pattuglia tra A e B
                MoveTowards(targetPoint.position, patrolSpeed);

                if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
                {
                    targetPoint = targetPoint == pointA ? pointB : pointA;
                }
            }
        }
    }

    void MoveTowards(Vector2 target, float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovementt ph = collision.gameObject.GetComponent<PlayerMovementt>();
            if (ph != null)
                ph.TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
