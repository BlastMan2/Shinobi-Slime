using UnityEngine;


/** 
 * Blue Slime Platforms have the following functions:
 * - Provide a jump boost when the player jumps off of one.
 * - Emit particles when the player is on the platform.
 */
public class BlueSlime : MonoBehaviour
{
    [SerializeField] private bool useParticles = false;
    [SerializeField] private float impulseForce = 10f;

    private ParticleSystem ps;
    private ParticleSystem.EmissionModule em;
    private ParticleSystem.ShapeModule sm;
    private bool playerOnPlatform = false;
    private GameObject currentPlayer;
    private Color slimeColor;

    private void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>(true);
        em = ps.emission;
        em.enabled = useParticles;
        sm = ps.shape;
        slimeColor = GetComponent<Renderer>().material.color;
        ConfigureParticleSystem(ps, slimeColor);
    }

    private void Update()
    {
        if (playerOnPlatform && Input.GetKeyDown(KeyCode.Space) && currentPlayer != null)
        {
            Debug.Log("Player Jumped off slime!");
            Rigidbody2D rb = currentPlayer.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Debug.Log("Boosted Jump!");
                rb.AddForce(Vector2.up * impulseForce, ForceMode2D.Impulse); 
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;
            currentPlayer = collision.gameObject;
            em.enabled = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == currentPlayer)
        {
            playerOnPlatform = false;
            currentPlayer = null;

            em.enabled = false;
        }

    }

    // Set the color of the slime's particles
    private void ConfigureParticleSystem(ParticleSystem ps, Color slimeColor)
    {
        // Set Particle Color
        var main = ps.main;
        Color particleColor = slimeColor * 1.1f;
        particleColor.a = slimeColor.a;
        main.startColor = particleColor;

        // Set Particle Size
        sm.scale = new Vector3(transform.localScale.x, sm.scale.y, sm.scale.z);
    }
}
