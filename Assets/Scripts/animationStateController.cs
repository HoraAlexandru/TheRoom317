using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;

    int isJumpingHash;
    int moveXHash;
    int moveZHash;


    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float rotateSpeed = 75f;
    public float jumpForce = 5f;

    Rigidbody rb;
    bool isGrounded = true;

    void Start()
    {
        PrintInstruction();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        moveXHash = Animator.StringToHash("MoveX");
        moveZHash = Animator.StringToHash("MoveZ");
    }

    void Update()
    
    {
        MovePlayer();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void PrintInstruction()
    {
        Debug.Log("Welcome to the game");
        Debug.Log("Move using wasd");
        Debug.Log("Don't bump into objects");
    }


    void MovePlayer()
{
    bool forwardPressed = Input.GetKey("w");
    bool backPressed = Input.GetKey("s");
    bool runPressed = Input.GetKey("left shift");
    bool rightPressed = Input.GetKey("d");
    bool leftPressed = Input.GetKey("a");
    bool jumpPressed = Input.GetKeyDown("space");

    // Calculeaza MoveX/MoveZ pentru Blend Tree
    float moveX = (rightPressed ? 1f : 0f) - (leftPressed ? 1f : 0f);
    float moveZ = (forwardPressed ? 1f : 0f) - (backPressed ? 1f : 0f);
    animator.SetFloat(moveXHash, moveX, 0.1f, Time.deltaTime);
    animator.SetFloat(moveZHash, moveZ, 0.1f, Time.deltaTime);

    animator.SetBool(isWalkingHash, forwardPressed || backPressed || rightPressed || leftPressed);
    animator.SetBool(isRunningHash, (forwardPressed || backPressed) && runPressed);

    float speed = runPressed ? runSpeed : walkSpeed;

    // Strafe stanga/dreapta - fara rotatie, doar translatie
    if (rightPressed)
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    if (leftPressed)
        transform.Translate(Vector3.left * speed * Time.deltaTime);

    // Inainte / inapoi
    if (forwardPressed)
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    if (backPressed)
        transform.Translate(Vector3.back * walkSpeed * Time.deltaTime);

    // Jump
    if (jumpPressed && isGrounded)
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
        animator.SetTrigger(isJumpingHash);
    }
}
  
}