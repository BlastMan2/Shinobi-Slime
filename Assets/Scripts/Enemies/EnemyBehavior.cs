using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

public class EnemyBehavior : MonoBehaviour
{
    // Basic enemy stats
    public int damage;
    public int enemyHealth;
    public float initialScale; // The initial scale of the enemy

    // Cooldowns
    private float damageCooldownTimer = 0f;
    public float damageCooldownDuration = 1f;

    // Status
    public bool isInvincible;
    public bool canFly;

    // Movement
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;

    // Player Info
    public HealthBar playerHealth;
    public Transform playerTransform;
    public PlayerMovement playerMovement;
    public PlayerData playerData;

    // Chase Player
    public bool canChasePlayer; // If the enemy can chase the player
    [SerializeField] private bool isChasing;
    public float chaseDistance; // How close the player must be before the monster is able to chase the player

    public void Start()
    {
        initialScale = transform.localScale.x;
    }

    public void Update()
    {
        if (isChasing && canChasePlayer)
        {
            if (transform.position.x > playerTransform.position.x)
            {
                transform.localScale = new Vector3(1 * initialScale, 1 * initialScale, 1); // Flip the enemy
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
            if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(-1 * initialScale, 1 * initialScale, 1); // Flip the enemy
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance) {
                isChasing = true;
            }
            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[0].position) < 0.2f)
                {
                    // If the enemy is at the first patrol point, move to the second one
                    transform.localScale = new Vector3(1 * initialScale, 1 * initialScale, 1); // Flip the enemy
                    patrolDestination = 1;
                }
            }
            if (patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[1].position) < 0.2f)
                {
                    transform.localScale = new Vector3(-1 * initialScale, 1 * initialScale, 1); // Flip the enemy
                    patrolDestination = 0;
                }
            }
        }

        // Decrement cooldown timer
        if (damageCooldownTimer > 0f)
        {
            damageCooldownTimer -= Time.deltaTime;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && damageCooldownTimer <= 0f)
        {
            damageCooldownTimer = damageCooldownDuration;
            playerMovement.knockbackCounter = playerData.knockbackTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                playerMovement.KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                playerMovement.KnockFromRight = false;
            }
            playerHealth.TakeDamage(damage);
            playerTransform.GetComponent<Animator>().SetTrigger("isDamaged");

            Debug.Log("Enemy collided with player and dealt damage.");
        }
    }
}
