using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private FlightController controller;
    private Rigidbody rig;
    public float rotationSpeed = 100f;
    public float movementSpeed = 10f;
    public float maxRotationSpeed = 100f;
    public float maxMovementSpeed = 10f; 
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        controller = new FlightController();
    }
    public void OnLeftStick(InputValue inputValue)
    {
        Vector2 leftStickInput = inputValue.Get<Vector2>();
        float horizontal = leftStickInput.x;
        float vertical = leftStickInput.y;
        float magnitude = leftStickInput.magnitude;
        movementSpeed = magnitude * maxMovementSpeed;
        Vector3 verticalVelocity = new Vector3(rig.velocity.x, vertical * movementSpeed, rig.velocity.z);
        rig.velocity = verticalVelocity;
        transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);
    }
    public void OnRightStick(InputValue inputValue)
    {
        Vector2 leftStickInput = inputValue.Get<Vector2>();
        float horizontal = leftStickInput.x;
        float vertical = leftStickInput.y;
        float magnitude = leftStickInput.magnitude;
        movementSpeed = magnitude * maxMovementSpeed;
        Vector3 verticalVelocity = new Vector3(vertical * -movementSpeed, rig.velocity.y, horizontal * movementSpeed);
        rig.velocity = verticalVelocity;
    }
}
