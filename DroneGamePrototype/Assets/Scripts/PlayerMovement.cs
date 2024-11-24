using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float rayDistance = 10f;
    public float angleOffset = 5f;
    private FlightController controller;
    private Rigidbody rig;
    public float rotationSpeed = 40;
    public float movementSpeed = 10f;
    public float maxRotationSpeed = 100f;
    public float maxMovementSpeed = 10f;
    public float speedMultiplier = 5f;
    public float Speed = 10f;
    // Start is called before the first frame update
    float leftHorizontal = 0;
    float leftVertical = 0;
    float leftMagnitude = 0;
    float rightHorizontal = 0;
    float rightVertical = 0;
    float rightMagnitude = 0;
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        controller = new FlightController();
        Physics.gravity = new Vector3(0, -100f, 0); // Double the gravity
    }
    public void OnLeftStick(InputValue inputValue)
    {
        Vector2 leftStickInput = inputValue.Get<Vector2>();
        leftHorizontal = leftStickInput.x;
        leftVertical = leftStickInput.y;
        leftMagnitude = leftStickInput.magnitude;
    }
    public void OnRightStick(InputValue inputValue)
    {
        Vector2 rightStickInput = inputValue.Get<Vector2>();
        rightHorizontal = rightStickInput.x;
        rightVertical = rightStickInput.y;
        rightMagnitude = rightStickInput.magnitude;
        movementSpeed = rightMagnitude * maxMovementSpeed;
    }
    void Update()
    {
        Vector3 rotationVelocity = new Vector3(-rightHorizontal * rotationSpeed * rightMagnitude,
        leftHorizontal * rotationSpeed * leftMagnitude, -rightVertical * rotationSpeed * rightMagnitude);
        rig.angularVelocity = transform.TransformDirection(rotationVelocity);

        movementSpeed = leftMagnitude * maxMovementSpeed * Speed;
        Vector3 newVelocity = new Vector3(0, leftVertical * movementSpeed, leftVertical * 0.1f);
        if (leftVertical > 0)
        {
            Vector3 worldVelocity = transform.TransformDirection(newVelocity);
            worldVelocity *= speedMultiplier;
            rig.AddForce(worldVelocity, ForceMode.VelocityChange);
        }
    }
}
