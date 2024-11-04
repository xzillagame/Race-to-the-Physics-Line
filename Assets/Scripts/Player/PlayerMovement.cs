using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    public enum GravityDirection
    {
        GravityDown,
        GravityUp,
    }


    [Header("Object References")]
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private CapsuleCollider playerCollider;
    [SerializeField] private PlayerStatCostants playerStatCostants;
    [SerializeField] private PlayerObjectReferenceHolder playerObjectReferenceHolder;


    [field: Space(10)]
    [field: Header("Movement Values")]
    [field: SerializeField] public float MovementSpeed { get; set; } = 5f;
    [field: SerializeField] public float JumpStrength { get; set; } = 15f;
    [SerializeField, Range(0, 1)] private float jumpVelocityCancelScaler = 0.25f;
    [SerializeField] private float groundRaycastDistance = .05f;
    [SerializeField] private float gravityStrength = -25f;

    [Space(10)]
    [SerializeField] private LayerMask groundableLayer;
    bool isGrounded = true;

    Transform playerTransform;
    Transform cameraTransform;


    #endregion

    #region Actions

    public event UnityAction<GravityDirection> OnGravityChanged;

    #endregion


    [ContextMenu("Upside gravity")]
    public void FlipGravity()
    {
        gravityStrength = Mathf.Abs(gravityStrength);
        playerTransform.rotation = Quaternion.AngleAxis(180f, Vector3.right);
        OnGravityChanged?.Invoke(GravityDirection.GravityUp);
    }

    [ContextMenu("Normal Gravity")]
    public void NormalizeGravity()
    {
        gravityStrength = Mathf.Abs(gravityStrength) * -1;
        playerTransform.rotation = Quaternion.AngleAxis(0, Vector3.right);
        OnGravityChanged?.Invoke(GravityDirection.GravityDown);
    }



    public void JumpPressed()
    {
        if (isGrounded)
        {
            playerRigidbody.AddForce(playerTransform.up * JumpStrength, ForceMode.VelocityChange);
        }
    }

    public void JumpReleased()
    {
        if (!isGrounded && playerRigidbody.velocity.y * (-Mathf.Sign(gravityStrength)) > 0f)
        {
            Vector3 currentVelocity = playerRigidbody.velocity;
            currentVelocity.y *= jumpVelocityCancelScaler;
            playerRigidbody.velocity = currentVelocity;
        }
    }

    public void MovePressed(Vector2 inputDirection)
    {


        Vector3 horizontalDirectionRelativeToCamera = cameraTransform.right * inputDirection.x;
        horizontalDirectionRelativeToCamera.y = 0f;

        Vector3 forwardDirectionRelativeToCamera = cameraTransform.forward * inputDirection.y;
        horizontalDirectionRelativeToCamera.y = 0f;

        Vector3 finalCalculatedMovement = (horizontalDirectionRelativeToCamera + forwardDirectionRelativeToCamera).normalized * MovementSpeed;

        finalCalculatedMovement.y = playerRigidbody.velocity.y;
        playerRigidbody.velocity = finalCalculatedMovement;

    }

    public void MoveReleased()
    {
        Vector3 stopVector = Vector3.zero;

        stopVector.y = playerRigidbody.velocity.y;
        playerRigidbody.velocity = stopVector;
    }

    private void OnEnable()
    {
        playerObjectReferenceHolder = GetComponent<PlayerObjectReferenceHolder>(); //!S.S
        playerTransform = transform;
    }

    private void Start()
    {
        cameraTransform = playerObjectReferenceHolder.CameraTransform;
        MovementSpeed = playerStatCostants.StartingMovementSpeed;
        JumpStrength = playerStatCostants.StartingJumpStrength;
        gravityStrength = playerStatCostants.StartingGravityStrength;
    }

    private void FixedUpdate()
    {
        Vector3 currentVelocity = playerRigidbody.velocity;

        Ray groundCheckRay = new Ray(playerTransform.position, playerTransform.up * -1);                             //Added the *LocalScale to account for when the player is being resized //!S.S
        isGrounded = Physics.Raycast(groundCheckRay, groundRaycastDistance + (playerCollider.height * playerTransform.localScale.x / 2f), groundableLayer);

        if (!isGrounded)
        {
            currentVelocity.y += gravityStrength * Time.fixedDeltaTime;


            if (Mathf.Abs(currentVelocity.y) > Mathf.Abs(gravityStrength))
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


    //Adding this to get the gravity for power-ups to uses //!S.S
    public float GetGravityStrength()
    {
        return gravityStrength;
    }
}
