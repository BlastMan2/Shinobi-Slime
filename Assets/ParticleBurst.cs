using UnityEngine;

public class ParticleBurst : StateMachineBehaviour
{
    public AudioClip burstSound;
    public float freezeDuration = 0.5f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Play particle burst
        ParticleSystem ps = animator.GetComponentInChildren<ParticleSystem>(true);
        PlayerMovement playerMovement = animator.GetComponentInChildren<PlayerMovement>();
        AudioSource audioSource = animator.GetComponentInChildren<AudioSource>();

        if (ps != null)
        {
            var em = ps.emission;
            em.enabled = true;
            var sm = ps.shape;
            sm.enabled = true;
            ps.Play();
        }

        audioSource.PlayOneShot(burstSound);
        playerMovement.Sleep(freezeDuration);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ParticleSystem ps = animator.GetComponentInChildren<ParticleSystem>(true);
        if (ps != null)
        {
            var em = ps.emission;
            em.enabled = false;
            var sm = ps.shape;
            sm.enabled = false;
            ps.Stop();
        }
    }


}
