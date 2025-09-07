using UnityEngine;

public class PlayFootstep : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public void PlaySound()
    {
        // Only play footsteps if player is grounded and not dashing or wall jumping
        if (playerMovement.LastOnGroundTime > 0f && !playerMovement.IsDashing && !playerMovement.IsWallJumping)
        {
            SoundManager.PlaySound(SoundType.FOOTSTEP, 0.2f);
        }
    }

}
