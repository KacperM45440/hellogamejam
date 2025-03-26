using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float angleToWorld = -90f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotationSpeed = 5f;
    
    [SerializeField] private CharacterController characterControllerRef;
    [SerializeField] private Animator playerBodyAnim;
    [SerializeField] private Transform gameCursor;
    [SerializeField] private GameObject visiblePlayer;
    [SerializeField] private GameObject skeletonPlayer;

    [Range(0, 1)] private int movementEnabled;
    private ModelTakePhoto modelTakePhoto;
    private Vector3 movement = Vector3.zero;

    //Should be deleted after we deal with proper level/scene loading
    private void Awake()
    {
        DontDestroyOnLoad(transform.parent.gameObject);
    }

    //Should be deleted after cave level stops being the first level in the game
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            modelTakePhoto.enabled = false;
            MovePlayerTo(3f, transform.position + transform.forward * 2.2f);
        }
    }

    //Should be moved to the New Input System whenever, for the sake of multi-platform
    private void Update()
    {
        Movement();
        AnimationSystem();
        GravitySystem();
        GameCursorSystem();
    }

    private void Movement() 
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
        
        Vector3 direction = Quaternion.AngleAxis(angleToWorld, Vector3.up) * Vector3.ClampMagnitude(movement, 1f) * movementEnabled;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //probably expensive, is there a better way?
        Quaternion rotation = Quaternion.LookRotation(new Vector3(mousePosition.x, transform.position.y, mousePosition.z) - transform.position, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        
        characterControllerRef.Move(speed * Time.deltaTime * direction);
    }

    private void AnimationSystem() 
    {
        if (IsMovementEnabled()) 
        { 
            return; 
        }

        Vector3 direction = Quaternion.AngleAxis(transform.localEulerAngles.y, Vector3.up) * Vector3.ClampMagnitude(Vector3.Scale(movement, new Vector3(-1, 1f, 1f)), 2f) * characterControllerRef.velocity.normalized.magnitude;
        playerBodyAnim.SetFloat("Z", Mathf.Lerp(playerBodyAnim.GetFloat("Z"), direction.z, Time.deltaTime * 10f));
        playerBodyAnim.SetFloat("X", Mathf.Lerp(playerBodyAnim.GetFloat("X"), direction.x, Time.deltaTime * 10f));
    }

    private void GameCursorSystem() 
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //probably expensive, is there a better way?
        gameCursor.forward = (new Vector3(mousePosition.x, transform.position.y, mousePosition.z) - transform.position).normalized;
        gameCursor.position = new Vector3(mousePosition.x, transform.position.y + 8f, mousePosition.z);
    }

    private void GravitySystem()
    {
        if (characterControllerRef.isGrounded)
        {
            movement.y = -0.1f;
        }
        else 
        {
            movement.y += Physics.gravity.y * Time.deltaTime;
        }
    }

    public bool IsMovementEnabled()
    {
        return movementEnabled == 1;
    }    

    public void EnableMovement()
    {
        movementEnabled = 1;
    }

    public void FreezeMovement()
    {
        movementEnabled = 0;
    }

    public void DoAction(string actionName, float durration)
    {
        StartCoroutine(DoActionEnum(actionName, durration));
    }

    public void MovePlayerTo(float duration, Vector3 targetPosition)
    {
        StartCoroutine(MovePlayerToEnum(duration, targetPosition));
    }

    private IEnumerator DoActionEnum(string actionName, float duration)
    {
        FreezeMovement();
        playerBodyAnim.SetTrigger(actionName);
        yield return new WaitForSeconds(duration);
        EnableMovement();
    }

    private IEnumerator MovePlayerToEnum(float duration, Vector3 targetPosition) 
    {
        FreezeMovement();
        float elapsedTime = 0f;
        Vector3 direction = targetPosition - transform.position;
        
        while (elapsedTime < duration) 
        {
            characterControllerRef.Move(speed * (Time.deltaTime / duration * direction));
            playerBodyAnim.SetFloat("Z", Mathf.Lerp(playerBodyAnim.GetFloat("Z"), direction.z, Time.deltaTime * 10f));
            playerBodyAnim.SetFloat("X", Mathf.Lerp(playerBodyAnim.GetFloat("X"), direction.x, Time.deltaTime * 10f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        EnableMovement();
        modelTakePhoto.enabled = true;
    }
}
