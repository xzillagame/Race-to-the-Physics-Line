using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    PlayerInput playerInput;


    private void OnEnable()
    {
        playerInput = new PlayerInput();

        playerInput.GroundMovement.Move.performed += MoveInput;
        playerInput.GroundMovement.Move.canceled += MoveInput;

        playerInput.GroundMovement.Jump.performed += JumpInput;

        playerInput.GroundMovement.Enable();
    }

    private void JumpInput(InputAction.CallbackContext context)
    {
        if(context.action.WasPressedThisFrame())
        {
            playerMovement.JumpPressed();
        }
        else if(context.action.WasReleasedThisFrame())
        {
            playerMovement.JumpReleased();
        }

    }

    private void OnDisable()
    {
        playerInput.GroundMovement.Move.performed -= MoveInput;
        playerInput.GroundMovement.Move.canceled -= MoveInput;

        playerInput.GroundMovement.Jump.performed -= JumpInput;
    }

    private void MoveInput(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            playerMovement.MovePressed(context.ReadValue<Vector2>());
        }
        else if(context.canceled)
        {
            playerMovement.MoveReleased();
        }
    }


}
