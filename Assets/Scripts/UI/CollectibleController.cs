using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    [SerializeField] private CollectibleUIElement CollectiblePrefab;
    [SerializeField] private GameObject CollectibleUIControl;
    [SerializeField] private int collectiblesInLevel;

    [SerializeField] private List<GameObject> collectibles = new List<GameObject>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisplayCollectiblesInLevel();
    }

    private void DisplayCollectiblesInLevel()
    {
        for (int i = 0; i < collectiblesInLevel; i++) { 
            var collectible = Instantiate(CollectiblePrefab);
            collectible.transform.SetParent(CollectibleUIControl.transform, false);
            collectible.GetComponent<CollectibleUIElement>().collectibleID = i;
            collectibles.Add(collectible.gameObject);
        }
    }

    public void CollectibleCollected(int collectibleID)
    {
        Debug.Log("Index: " + collectibleID);
        CollectibleUIElement collectible = collectibles[Math.Clamp(collectibleID, 0, collectibles.Count)].GetComponent<CollectibleUIElement>();
        collectible.MarkAsCollected();
    }
}
