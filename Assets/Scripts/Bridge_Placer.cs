using UnityEngine;
using System.Collections;

/**
 * This script is my solution to using an Animator to place bridges. 
 */
public class Bridge_Placer : MonoBehaviour
{
    public int bridgeCount; // Number of bridges placed
    public bool bridgeActive; // Is the bridge active?
    private float timeBetweenPlacements = 0.2f; // Time between each bridge placement
    private SpriteRenderer sp;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    public void SwitchMethod() // Function to be used by the Switch Object
    {
        PlaceBridge();
    }

    private void PlaceBridge()
    {
        StartCoroutine(PlaceBridgeCoroutine());
    }

    private IEnumerator PlaceBridgeCoroutine()
    {
        bridgeActive = true;
        gameObject.SetActive(true);

        int currentWidth = Mathf.RoundToInt(sp.size.x);

        while (currentWidth < bridgeCount)
        {
            currentWidth++;
            sp.size = new Vector2(currentWidth, sp.size.y);
            SoundManager.PlaySound(SoundType.BRIDGE_PLACE, 0.2f);
            yield return new WaitForSeconds(timeBetweenPlacements);
        }
    }
}
