using UnityEngine;

public class SlimeBallCollisionHandler : MonoBehaviour
{
    public ShinobiSense shinobiSense;
    public LayerMask collisionLayers; // Set this in inspector to include tilemap layer

    private Collider2D col;
    private bool collisionEnabled = false;

    void Awake()
    {
        shinobiSense = GetComponentInParent<ShinobiSense>();
        col = GetComponent<Collider2D>();
    }

    void OnEnable()
    {
        collisionEnabled = false;
        if (col != null)
            col.enabled = false;

        Invoke(nameof(EnableCollision), 0.5f);
    }

    void EnableCollision()
    {
        collisionEnabled = true;
        if (col != null)
            col.enabled = true;
    }

    void Update()
    {
        if (!collisionEnabled) return;

        // Do a manual collision check using OverlapCircle at the slimeBall's position
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.1f, collisionLayers);

        if (hit != null && shinobiSense != null)
        {
            shinobiSense.DeactivateSlimeBall();
        }
    }

/*    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collisionEnabled) return;
        if (shinobiSense != null)
        {
            shinobiSense.DeactivateSlimeBall();
        }
    }*/
    void OnCollisionStay2D(Collision2D collision)
    {
        if (!collisionEnabled) return;
        if (shinobiSense != null)
        {
            shinobiSense.DeactivateSlimeBall();
        }
    }

/*    void OnTriggerEnter2D(Collider2D other)
    {
        if (!collisionEnabled) return;
        if (shinobiSense != null)
        {
            shinobiSense.DeactivateSlimeBall();
        }
    }*/
}
