using UnityEngine;
using UnityEngine.InputSystem.DualShock.LowLevel;

public class Collectible : MonoBehaviour
{
    public int collectibleID; // 1 Indexed
    public CollectibleController CollectibleController;
    private ParticleSystem ps;
    private ParticleSystem.EmissionModule em;
    private SpriteRenderer sr;
    public bool once = true;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        em = ps.emission;
        em.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && once)
        {
            // Notify the CollectibleController that this collectible has been collected
            if (CollectibleController != null)
            {
                CollectibleController.CollectibleCollected(collectibleID - 1);
            }
            var duration = ps.main.duration;
            em.enabled = true;
            ps.Play();

            once = false;

            // Play sound effect
            SoundManager.PlaySound(SoundType.COLLECTIBLE, 0.2f);
            Destroy(sr);

            Invoke(nameof(DestroyObj), duration);
        }
    }

    void DestroyObj()
    {
        Destroy(gameObject);
    }
}
