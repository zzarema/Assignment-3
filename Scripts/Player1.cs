
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public float velocity = 5f;
    public float sprintAdittion = 3.5f;
    public float jumpForce = 18f;
    public float jumpTime = 0.85f;
    public float gravity = 9.8f;

    float jumpElapsedTime = 0;

    bool isJumping = false;
    bool isSprinting = false;
    bool isCrouching = false;

    // Inputs
    float inputHorizontal;
    float inputVertical;
    bool inputJump;
    bool inputSprint;

    Animator animator;
    CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (animator == null)
            Debug.LogWarning("Animator component missing.");
    }

    void Update()
    {
        // Input checkers
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        inputJump = Input.GetButtonDown("Jump");
        inputSprint = Input.GetButton("Fire3"); // Typically "Shift" for sprint

        // Jump and crouch handling
        if (inputJump && cc.isGrounded)
            isJumping = true;

        // Set animator states based on conditions
        if (animator != null)
        {
            animator.SetBool("air", !cc.isGrounded);
            animator.SetBool("run", cc.velocity.magnitude > 0.1f);
            animator.SetBool("sprint", isSprinting);
        }

        HeadHittingDetect();
    }

    private void FixedUpdate()
    {
        // Adjust speed for sprinting
        float velocityAddition = 0;
        if (isSprinting)
            velocityAddition = sprintAdittion;

        // Movement calculation
        float directionX = inputHorizontal * (velocity + velocityAddition) * Time.deltaTime;
        float directionZ = inputVertical * (velocity + velocityAddition) * Time.deltaTime;
        float directionY = 0;

        if (isJumping)
        {
            directionY = Mathf.SmoothStep(jumpForce, jumpForce * 0.30f, jumpElapsedTime / jumpTime) * Time.deltaTime;
            jumpElapsedTime += Time.deltaTime;
            if (jumpElapsedTime >= jumpTime)
            {
                isJumping = false;
                jumpElapsedTime = 0;
            }
        }

        // Apply gravity
        directionY -= gravity * Time.deltaTime;

        // Get directions from the camera's forward and right axes
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0; // Keep the direction on the horizontal plane
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * inputVertical + right * inputHorizontal;

        // Rotate the player to move in the direction of the input
        if (moveDirection.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.15f);
        }

        // Move the player
        Vector3 verticalMovement = Vector3.up * directionY;
        cc.Move((moveDirection * (velocity + velocityAddition) * Time.deltaTime) + verticalMovement);
    }

    void HeadHittingDetect()
    {
        float headHitDistance = 1.1f;
        Vector3 ccCenter = transform.TransformPoint(cc.center);
        float hitCalc = cc.height / 2f * headHitDistance;

        if (Physics.Raycast(ccCenter, Vector3.up, hitCalc))
        {
            jumpElapsedTime = 0;
            isJumping = false;
        }
    }
}
