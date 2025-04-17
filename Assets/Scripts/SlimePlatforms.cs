using UnityEngine;

public class SlimePlatforms : MonoBehaviour
{
    [SerializeField] public string slimeType;
    [SerializeField] private string[] slimeTypes = { "Blue", "Black", "Purple", "Yellow", "Red", "Orange" };
    [SerializeField] private float impulseForce = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the "player" tag
        if (collision.gameObject.CompareTag("player") && slimeType == "Blue")
        {
            Debug.Log("Hit!");
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Apply a vertical impulsive force
                rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);
            }
        }
    }
}
