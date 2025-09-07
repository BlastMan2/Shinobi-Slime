using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HealthSegment : MonoBehaviour
{
    [SerializeField] public bool isActive;
    private Animator animator;

    private void Start()
    {
        isActive = true;
        animator = GetComponent<Animator>();
    }

    public void EnableSegment(Color enabledColor) {
        isActive = true;
        animator.SetTrigger("RegainHP");
    }
    public void DisableSegment(Color disabledColor)
    {
        isActive = false;
        animator.SetTrigger("LoseHP");
    }
}
