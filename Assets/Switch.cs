using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    public UnityEvent onSwitchTriggered;
    private bool hasBeenActivated = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasBeenActivated && Input.GetKeyDown(KeyCode.E))
        {
            hasBeenActivated = true;
            onSwitchTriggered.Invoke();
            Debug.Log("Switch activated!");
        }
    }
}
