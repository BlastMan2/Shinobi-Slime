using UnityEngine;

/**
 * Red Slime Platforms have the following functions:
 * - Produce a constant particle emission.
 * - When the player collides with the platform, cause them to lose health and respawn to their last checkpoint.
 */
public class YellowSlime : MonoBehaviour
{
    [SerializeField] private bool useParticles = true;
    [SerializeField] private AudioClip slimeSound;
    private AudioSource audioSource;

    private ParticleSystem ps;
    private ParticleSystem.EmissionModule em;
    private ParticleSystem.ShapeModule sm;
    private bool playerOnPlatform = false;
    private GameObject currentPlayer;
    private Color slimeColor;

    public float moveSpeed;
    public int startingPoint;
    public Transform[] points;

    private int i; // index of array

    private void Start()
    {
        // Center the AudioSource on the X and Y axis within the transform
        ps = GetComponentInChildren<ParticleSystem>(true);
        em = ps.emission;
        em.enabled = useParticles;
        sm = ps.shape;
        slimeColor = GetComponent<Renderer>().material.color;
        ConfigureParticleSystem(ps, slimeColor);

        transform.position = points[startingPoint].position;
    }

    public void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, moveSpeed * Time.deltaTime);
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;
            currentPlayer = collision.gameObject;
            currentPlayer.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == currentPlayer)
        {
            playerOnPlatform = false;
            currentPlayer.transform.SetParent(null);
            currentPlayer = null;
        }
    }*/

    // Set the color of the slime's particles
    private void ConfigureParticleSystem(ParticleSystem ps, Color slimeColor)
    {
        // Set Particle Color
        var main = ps.main;
        Color particleColor = slimeColor * 1.1f;
        particleColor.a = slimeColor.a;
        main.startColor = particleColor;

        // Set Particle System Size
        sm.scale = new Vector3(transform.localScale.x, sm.scale.y, sm.scale.z);
    }
}
