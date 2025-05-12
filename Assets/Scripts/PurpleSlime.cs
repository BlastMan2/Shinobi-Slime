using UnityEngine;

public class PurpleSlime : MonoBehaviour
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
        if (playerOnPlatform && currentPlayer != null)
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

            EnablePlayerGlow(currentPlayer, slimeColor * 1.5f); // Apply glow
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == currentPlayer)
        {
            playerOnPlatform = false;
            currentPlayer = null;

            em.enabled = false;

            DisablePlayerGlow(collision.gameObject); // Remove glow
        }
    }

    private void ConfigureParticleSystem(ParticleSystem ps, Color slimeColor)
    {
        var main = ps.main;
        Color particleColor = slimeColor * 1.1f;
        particleColor.a = slimeColor.a;
        main.startColor = particleColor;

        sm.scale = new Vector3(transform.localScale.x, sm.scale.y, sm.scale.z);
    }

    // === Emission Control ===
    private void EnablePlayerGlow(GameObject player, Color emissionColor)
    {
        var renderer = player.GetComponent<SpriteRenderer>();
        if (renderer != null && renderer.material.HasProperty("_EmissionColor"))
        {
            renderer.material.EnableKeyword("_EMISSION");
            renderer.material.SetColor("_EmissionColor", emissionColor);
        }
    }

    private void DisablePlayerGlow(GameObject player)
    {
        var renderer = player.GetComponent<SpriteRenderer>();
        if (renderer != null && renderer.material.HasProperty("_EmissionColor"))
        {
            renderer.material.SetColor("_EmissionColor", Color.black);
            renderer.material.DisableKeyword("_EMISSION");
        }
    }
}
