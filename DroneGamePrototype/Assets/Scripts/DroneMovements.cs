using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DroneMovements : MonoBehaviour
{
    // Variables for movement and speed
    public float moveSpeed = 10f;     // Forward/backward and strafing speed
    public float ascendSpeed = 5f;    // Speed for ascending/descending
    public float rotationSpeed = 100f; // Speed for rotating the drone

    private Vector2 moveInput;  // Left stick input (X, Y)
    private Vector2 rotateInput; // Right stick input (X for yaw rotation)
    private float ascendInput;  // R2 and L2 triggers input

    public InputActionAsset inputActions; // Reference to Input Action asset

    private InputAction moveAction;
    private InputAction rotateAction;
    private InputAction ascendAction;

    void Awake()
    {
        // Reference the action map and actions from InputActionAsset
        var gameplayActionMap = inputActions.FindActionMap("Gameplay");

        moveAction = gameplayActionMap.FindAction("Move");
        rotateAction = gameplayActionMap.FindAction("Rotate");
        ascendAction = gameplayActionMap.FindAction("AscendDescend");

        // Enable actions
        moveAction.Enable();
        rotateAction.Enable();
        ascendAction.Enable();
    }

    void Update()
    {
        // Get inputs from the controller
        moveInput = moveAction.ReadValue<Vector2>();      // X and Y values from left stick
        rotateInput = rotateAction.ReadValue<Vector2>();  // X value from right stick for yaw rotation
        ascendInput = ascendAction.ReadValue<float>();    // R2 and L2 values for ascending/descending

        // Call functions to move, rotate, and ascend/descend the drone
        MoveDrone();
        RotateDrone();
        AscendDescendDrone();
    }

    void MoveDrone()
    {
        // Move forward/backward and strafe left/right based on left stick input
        Vector3 move = transform.forward * moveInput.y + transform.right * moveInput.x;
        transform.position += move * moveSpeed * Time.deltaTime;
    }

    void RotateDrone()
    {
        // Rotate around the Y-axis (yaw) based on right stick input (rotateInput.x)
        transform.Rotate(0f, rotateInput.x * rotationSpeed * Time.deltaTime, 0f);
    }

    void AscendDescendDrone()
    {
        // Ascend (R2) or descend (L2) based on trigger input
        float ascend = ascendInput * ascendSpeed * Time.deltaTime;
        transform.position += Vector3.up * ascend;
    }

    void OnDisable()
    {
        // Disable actions when the object is disabled
        moveAction.Disable();
        rotateAction.Disable();
        ascendAction.Disable();
    }
}