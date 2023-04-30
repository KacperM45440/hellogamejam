using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float angleToWorld = -90f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotationSpeed = 5f;
    public CharacterController controller;
    public Vector3 movement = Vector3.zero;

    [SerializeField] public Animator playerBodyAnim;
    [HideInInspector] public bool freezeMovement = false;
    [SerializeField] private Transform gameCursor;
        

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        DontDestroyOnLoad(transform.parent.gameObject);
    }

    void Start()
    {
        PlayerReference.Instance.playerMovement = this;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            MovePlayerTo(3f, transform.position + transform.forward * 2f);
        }
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
        GameCursorSystem();
    }

    void Movement() {

        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
        Vector3 direction = Quaternion.AngleAxis(angleToWorld, Vector3.up) * Vector3.ClampMagnitude(movement, 1f);
       
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rotation = Quaternion.LookRotation(new Vector3(mousePosition.x, transform.position.y, mousePosition.z) - transform.position, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        controller.Move(speed * Time.deltaTime * direction);

    }

    void AnimationSystem() {
        if (freezeMovement) { return; }
        Vector3 direction = Quaternion.AngleAxis(transform.localEulerAngles.y, Vector3.up) * Vector3.ClampMagnitude(Vector3.Scale(movement, new Vector3(-1, 1f, 1f)), 2f) * controller.velocity.normalized.magnitude;
        playerBodyAnim.SetFloat("Z", Mathf.Lerp(playerBodyAnim.GetFloat("Z"), direction.z, Time.deltaTime * 10f));
        playerBodyAnim.SetFloat("X", Mathf.Lerp(playerBodyAnim.GetFloat("X"), direction.x, Time.deltaTime * 10f));
    }

    void GameCursorSystem() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameCursor.forward = (new Vector3(mousePosition.x, transform.position.y, mousePosition.z) - transform.position).normalized;
        gameCursor.position = new Vector3(mousePosition.x, transform.position.y + 8f, mousePosition.z);
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

    public void DoAction(string actionName, float durration) 
    {
        StartCoroutine(DoActionEnum(actionName, durration));
    }

    private IEnumerator DoActionEnum(string actionName, float duration) {
        freezeMovement = true;
        playerBodyAnim.SetTrigger(actionName);
        yield return new WaitForSeconds(duration);
        freezeMovement = false;
    }

    public void StandUp()
    {
        StartCoroutine(StandUpEnum());
    }
    IEnumerator StandUpEnum()
    {
        freezeMovement = true;
        playerBodyAnim.Play("Up");
        yield return new WaitForSeconds(2f);
        freezeMovement = false;
    }

    public void MovePlayerTo(float duration, Vector3 targetPosition) {
        StartCoroutine(MovePlayerToEnum(duration, targetPosition));
    }

    IEnumerator MovePlayerToEnum(float duration, Vector3 targetPosition) {
        freezeMovement = true;
        float elapsedTime = 0f;
        Vector3 direction = targetPosition - transform.position;
        while (elapsedTime < duration) {
            controller.Move(speed * (Time.deltaTime / duration * direction));
            playerBodyAnim.SetFloat("Z", Mathf.Lerp(playerBodyAnim.GetFloat("Z"), direction.z, Time.deltaTime * 10f));
            playerBodyAnim.SetFloat("X", Mathf.Lerp(playerBodyAnim.GetFloat("X"), direction.x, Time.deltaTime * 10f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        freezeMovement = false;
    }
}
