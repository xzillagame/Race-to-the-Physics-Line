using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    [Header("Object References")]
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private CapsuleCollider playerCollider;
    [SerializeField] private PlayerStatCostants playerStatCostants;


    [Space(10)]
    [Header("Movement Values")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpStrength = 15f;
    [SerializeField, Range(0, 1)] private float jumpVelocityCancelScaler = 0.25f;
    [SerializeField] private float groundRaycastDistance = .05f;
    [SerializeField] private float gravityStrength = -25f;

    [Space(10)]
    [SerializeField] private LayerMask groundableLayer;

    bool isGrounded = true;

    #endregion

    [ContextMenu("Upside gravity")]
    public void FlipGravity()
    {
        gravityStrength = Mathf.Abs(gravityStrength);
    }

    [ContextMenu("Normal Gravity")]
    public void NormalizeGravity()
    {
        gravityStrength = Mathf.Abs(gravityStrength) * -1;
    }



    public void JumpPressed()
    {
        if(isGrounded)
        {
            playerRigidbody.AddForce(Vector3.up * jumpStrength, ForceMode.VelocityChange);
        }
    }

    public void JumpReleased()
    {
        if(!isGrounded && playerRigidbody.velocity.y > 0f)
        {
            Vector3 currentVelocity = playerRigidbody.velocity;
            currentVelocity.y *= jumpVelocityCancelScaler;
            playerRigidbody.velocity = currentVelocity;
        }
    }

    public void MovePressed(Vector2 inputDirection)
    {
        Vector3 moveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y) * movementSpeed;

        moveDirection.y = playerRigidbody.velocity.y;
        playerRigidbody.velocity = moveDirection;
    }

    public void MoveReleased()
    {
        Vector3 stopVector = Vector3.zero;

        stopVector.y = playerRigidbody.velocity.y;
        playerRigidbody.velocity = stopVector;
    }

    private void Start()
    {
        movementSpeed = playerStatCostants.StartingMovementSpeed;
        jumpStrength = playerStatCostants.StartingJumpStrength;
        gravityStrength = playerStatCostants.StartingGravityStrength;
    }

    private void FixedUpdate()
    {
        Vector3 currentVelocity = playerRigidbody.velocity;

        Ray groundCheckRay = new Ray(transform.position, Vector3.down);                             //Added the *LocalScale to account for when the player is being resized
        isGrounded = Physics.Raycast(groundCheckRay, groundRaycastDistance + (playerCollider.height  * transform.localScale.x / 2f), groundableLayer);


        if (!isGrounded)
        {
            currentVelocity.y += gravityStrength * Time.fixedDeltaTime;


            if(Mathf.Abs(currentVelocity.y) > Mathf.Abs(gravityStrength))
            {
                currentVelocity.y = gravityStrength;
            }

        }
        else
        {
            currentVelocity.y = 0f;
        }


        playerRigidbody.velocity = currentVelocity;
    }
}
