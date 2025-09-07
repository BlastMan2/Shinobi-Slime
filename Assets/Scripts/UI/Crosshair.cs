using UnityEngine;

public class Crosshair : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Cursor.visible = false; // Hides the cursor
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Converts the mouse position to world coordinates
        transform.position = mouseCursorPos; // Sets the position of the crosshair to the mouse position
    }
}
