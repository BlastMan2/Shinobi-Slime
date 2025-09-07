using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ShinobiSense : MonoBehaviour
{
    public GameObject slimeBall; // The SlimeBall to be used by Shinobi Sense
    public GameObject player; // Reference to the player GameObject
    private PlayerMovement playerMovement;
    public CinemachineCamera cmCamera; // Assign this in the inspector

    public bool slimeBallActive = false;

    [Header("Slime Movement Settings")]
    public float slimeMoveSpeed = 5f;
    public float slimeSmoothness = 10f;

    private Vector3 slimeVelocity = Vector3.zero;

    [Header("Shinobi Sense Lighting")]
    public Light2D mainLight; // Reference to the main light in the scene
    public Color shinobiSenseColor; // Shinobi Sense color (hex code)
    public float shinobiSenseIntensity = 1.5f; // Shinobi Sense intensity
    private float mainLightStartingIntensity; // Starting intensity of the main light
    private Color mainLightStartingColor; // Starting color of the main light

    [Header("Shinobi Sense UI")]
    public GameObject flashImage;
    private Animator flashAnimator;
    public GameObject shadowImage;

    [Header("Shinobi Sense Audio")]
    public AudioClip shinobiSenseAmbientSound; // Plays while Shinobi Sense is active
    private AudioSource audioSource; // Reference to the AudioSource component


    void Awake()
    {
        Cursor.visible = false;
        mainLightStartingIntensity = mainLight.intensity; // Store the starting intensity of the main light
        mainLightStartingColor = mainLight.color;
        slimeBall.SetActive(false); // Deactivate the SlimeBall at the start
        playerMovement = player.GetComponent<PlayerMovement>();
        flashAnimator = flashImage.GetComponent<Animator>();
        audioSource = slimeBall.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && playerMovement.LastOnGroundTime > 0f) // If K is pressed, and player is grounded
        {
            TriggerShinobiSense();
            Debug.Log("Activate Slime");
        }

        Vector2 mouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (slimeBallActive)
        {
            // Move toward the mouse with smoothing
            slimeBall.transform.position = Vector3.SmoothDamp(
                slimeBall.transform.position,
                mouseCursorPos,
                ref slimeVelocity,
                1f / slimeSmoothness,
                slimeMoveSpeed
            );
            slimeBall.transform.position = mouseCursorPos;
        }
        else
        {
            
        }
    }

    void TriggerShinobiSense()
    {
        flashAnimator.SetTrigger("flash"); // Trigger the flash animation
        if (!slimeBallActive)
            ActivateSlimeBall();
        else
            DeactivateSlimeBall();
    }

    void ActivateSlimeBall()
    {
        playerMovement.enabled = false; // Disable player movement
        //shadowImage.GetComponentInChildren<Image>().enabled = true; // Put a shadow overlay
        player.GetComponentInChildren<Light2D>().enabled = false; // Disable the player's light
        audioSource.Play(); // Play the ambient sound
        SoundManager.PlaySoundSpecificIndex(SoundType.SHINOBI_SENSE, 0.5f, 0); // Play sound effect
        mainLight.intensity = shinobiSenseIntensity;
        mainLight.color = shinobiSenseColor; // Change the light color to Shinobi Sense color
        slimeBall.SetActive(true);
        //slimeBall.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
        slimeBallActive = true;

        if (cmCamera != null)
        {
            cmCamera.Follow = slimeBall.transform;
        }
    }

    public void DeactivateSlimeBall()
    {
        playerMovement.enabled = true; // Re-enable player movement
        //shadowImage.GetComponentInChildren<Image>().enabled = false; // Get rid of shadow overlay
        player.GetComponentInChildren<Light2D>().enabled = true; // Disable the player's light
        SoundManager.PlaySoundSpecificIndex(SoundType.SHINOBI_SENSE, 0.5f, 1); // Play sound effect
        mainLight.intensity = mainLightStartingIntensity;
        mainLight.color = mainLightStartingColor; // Change the light color to Shinobi Sense color
        audioSource.Stop();
        slimeBall.transform.position = this.transform.position;
        slimeBall.SetActive(false);
        slimeBallActive = false;

        if (cmCamera != null)
        {
            cmCamera.Follow = player.transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DeactivateSlimeBall();
    }
}
