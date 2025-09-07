using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Cinemachine.CinemachineFreeLookModifier;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject HPSegment;
    [SerializeField] private GameObject HealthUIControl;
    [SerializeField] private int maxHealth = 5;
    [SerializeField] public int currentHealth = 5;

    [SerializeField] private List<GameObject> HealthPoints = new List<GameObject>();
    public Slider slider;

    public void Start()
    {
        SetMaxHealth();
    }

    // Updates the player's health. Negative values = heal. Positive values = damage.
    private void UpdatePlayerHealth (int modifier)
    {
        if (modifier > 0)
        {
            TakeDamage(modifier);
        }
        else
        {
            //RegainHP(modifier);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        Debug.Log("Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
        else
        {
            for (int i = maxHealth; i > -1; i--)
            {
                if (currentHealth < i)
                {
                    HealthSegment segment = HealthPoints[i - 1].GetComponent<HealthSegment>();
                    if (segment.isActive)
                    {
                        segment.DisableSegment(new Color(0.5f, 0, 0, 1f));
                    }
                }
            }
        }
    }

    private void SetMaxHealth()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            var segment = Instantiate(HPSegment);
            segment.transform.SetParent(HealthUIControl.transform, false);
            HealthPoints.Add(segment);
        }
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health) {
        slider.value = health;
    }
}
