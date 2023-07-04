using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int plane_limit; // How far the player can travel

    public float yaw, pitch;
    private Rigidbody rb;
    public float movement_speed, sensitivity;
    public GameObject my_camera;
    public bool should_be_in_control;

    // Start is called before the first frame update
    void Start()
    {
        Mind.plane = 0;
        Cursor.lockState = CursorLockMode.Locked;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Mind.plane += 1;
            if (Mind.plane == plane_limit + 1) // Loops back after reaching the set plane 
            {
                Mind.plane = 0;
            }
        }

        if (should_be_in_control)
        {
            Cursor.lockState = CursorLockMode.Locked;
            pitch -= Input.GetAxisRaw("Mouse Y") * sensitivity;
            pitch = Mathf.Clamp(pitch, -80, 50);
            yaw += Input.GetAxisRaw("Mouse X") * sensitivity;
            my_camera.transform.localRotation = Quaternion.Euler(pitch, yaw, 0);
        }  
    }

    void FixedUpdate()
    {
        if (should_be_in_control) // The condition that would stop you
        {
            Vector2 axis = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * movement_speed;
            Vector3 forward = new Vector3(-my_camera.transform.right.z, 0, my_camera.transform.right.x);
            Vector3 wishDirection = (forward * axis.x + my_camera.transform.right * axis.y + Vector3.up * rb.velocity.y);
            rb.velocity = wishDirection;
        }
    }
}
