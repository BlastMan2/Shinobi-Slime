using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject linkedObject; // The object to be linked to the switch
    private bool hasBeenActivated;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }
}