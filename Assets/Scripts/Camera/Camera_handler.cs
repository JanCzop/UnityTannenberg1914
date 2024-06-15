using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_handler : MonoBehaviour
{
    public float speed = 5.0f;  // Speed of the camera movement
    public float edge_scroll_speed = 5.0f; // Speed of the camera movement when using edge scrolling
    public float edge_boundary = 10.0f; // Distance from the edge of the screen to start edge scrolling
    public float min_x = -10.0f; // Minimum X boundary
    public float max_x = 10.0f;  // Maximum X boundary
    public float min_z = -10.0f; // Minimum Z boundary
    public float max_z = 10.0f;  // Maximum Z boundary

    void Update()
    {
        // Get input from the keyboard
        float move_x = Input.GetAxis("Horizontal");
        float move_z = Input.GetAxis("Vertical");

        // Get input from the mouse position for edge scrolling
        if (Input.mousePosition.x >= Screen.width - edge_boundary)
        {
            move_x = 1;
        }
        else if (Input.mousePosition.x <= edge_boundary)
        {
            move_x = -1;
        }

        if (Input.mousePosition.y >= Screen.height - edge_boundary)
        {
            move_z = 1;
        }
        else if (Input.mousePosition.y <= edge_boundary)
        {
            move_z = -1;
        }

        // Calculate the new position
        Vector3 new_position = transform.position + new Vector3(move_x, 0, move_z) * speed * Time.deltaTime;

        // Clamp the position to the defined boundaries
        new_position.x = Mathf.Clamp(new_position.x, min_x, max_x);
        new_position.z = Mathf.Clamp(new_position.z, min_z, max_z);

        // Apply the new position to the camera
        transform.position = new_position;
    }
}
