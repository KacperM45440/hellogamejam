using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float angleToWorld = -90f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotationSpeed = 5f;
    private CharacterController controller;
    private Vector3 movement = Vector3.zero;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Movement();
    }

    void Movement() {

        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
        Vector3 direction = Quaternion.AngleAxis(angleToWorld, Vector3.up) * Vector3.ClampMagnitude(movement, 1f);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rotation = Quaternion.LookRotation(new Vector3(mousePosition.x, transform.position.y, mousePosition.z) - transform.position, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        controller.Move(direction * speed * Time.deltaTime);

    }

}
