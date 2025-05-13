using UnityEngine;
using UnityEngine.UI;

public class CollectibleUIElement : MonoBehaviour
{
    [SerializeField] private bool hasBeenCollected = false;
    [SerializeField] private Image fill;
    public int collectibleID;

    private void Start() {
        hasBeenCollected = false;
        fill = GetComponentInChildren<Image>();
        fill.enabled = false;
    }
    public void MarkAsCollected()
    {
        hasBeenCollected = true;
        fill.enabled = true;
    }
}
