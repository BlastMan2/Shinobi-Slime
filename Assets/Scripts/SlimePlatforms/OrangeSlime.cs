using System.Collections.Generic;
using UnityEngine;

public class OrangeSlime : MonoBehaviour
{
    [SerializeField] private bool useParticles = false;
    [SerializeField] private float impulseForce = 10f;

    private ParticleSystem ps;
    private ParticleSystem.EmissionModule em;
    private ParticleSystem.ShapeModule sm;
    private Color slimeColor;

    private bool playerOnPlatform = false;
    private GameObject currentPlayer;

    // Track all OrangeSlime instances
    private static List<OrangeSlime> allSlimes = new List<OrangeSlime>();

    private void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>(true);
        em = ps.emission;
        em.enabled = useParticles;
        sm = ps.shape;
        slimeColor = GetComponent<Renderer>().material.color;
        ConfigureParticleSystem(ps, slimeColor);

        allSlimes.Add(this);
    }

    private void OnDestroy()
    {
        allSlimes.Remove(this);
    }

    private void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.CompareTag("Player"))
        {
            playerOnPlatform = true;
            currentPlayer = other;
            em.enabled = true;

            //EnablePlayerGlow(currentPlayer, slimeColor * 1.5f);
        }
        else if (other.CompareTag("ShinobiSense"))
        {
            // This slime was hit by the slimeBall
            foreach (OrangeSlime slime in allSlimes)
            {
                if (slime != this && slime.playerOnPlatform && slime.currentPlayer != null)
                {
                    Debug.Log("Teleporting player from one slime to another.");

                    // Teleport player to the top of THIS slime
                    Vector3 teleportPos = transform.position + Vector3.up * (transform.localScale.y / 2f + 0.5f);
                    slime.currentPlayer.transform.position = teleportPos;
                    break;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == currentPlayer)
        {
            playerOnPlatform = false;
            currentPlayer = null;

            em.enabled = false;

            //DisablePlayerGlow(collision.gameObject);
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
