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
    public LayerMask backgroundMask;

    [SerializeField] private Animator playerBodyAnim;
    [HideInInspector] public bool freezeMovement = false;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!freezeMovement)
        {
            Movement();
        }
        else
        {
            movement = Vector3.zero;
        }
        AnimationSystem();
        GravitySystem();
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

    void AnimationSystem() {
        Vector3 direction = Quaternion.AngleAxis(transform.localEulerAngles.y, Vector3.up) * Vector3.ClampMagnitude(Vector3.Scale(movement, new Vector3(-1, 1f, 1f)), 2f) * controller.velocity.normalized.magnitude;

        playerBodyAnim.SetFloat("Z", Mathf.Lerp(playerBodyAnim.GetFloat("Z"), direction.z, Time.deltaTime * 10f));
        playerBodyAnim.SetFloat("X", Mathf.Lerp(playerBodyAnim.GetFloat("X"), direction.x, Time.deltaTime * 10f));
    }

    void GravitySystem()
    {
        if (controller.isGrounded)
        {
            movement.y = -0.1f;
        }
        else {
            movement.y += Physics.gravity.y * Time.deltaTime;
        }
    }

    public void DoAction(string actionName, float durration) {
        StartCoroutine(DoActionEnum(actionName, durration));
    }

    private IEnumerator DoActionEnum(string actionName, float durration) {
        freezeMovement = true;
        playerBodyAnim.SetTrigger(actionName);
        yield return new WaitForSeconds(durration);
        freezeMovement = false;
    }
}
