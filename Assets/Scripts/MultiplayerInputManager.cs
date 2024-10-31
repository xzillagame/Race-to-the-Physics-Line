using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerInputManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement player1;
    [SerializeField] private PlayerMovement player2;

    private PlayerInput multiplayerInput;

    private void OnEnable()
    {
        multiplayerInput = new PlayerInput();


        #region Player 1 Add Event Listenrs

        multiplayerInput.Player1Movement.Move.performed += Player1MoveInput;
        multiplayerInput.Player1Movement.Move.canceled += Player1MoveInput;

        multiplayerInput.Player1Movement.Jump.performed += Player1JumpInput;

        #endregion

        #region Player 2 Add Event Listeners

        multiplayerInput.Player2Movement.Move.performed += Player2MoveInput;
        multiplayerInput.Player2Movement.Move.canceled += Player2MoveInput;

        multiplayerInput.Player2Movement.Jump.performed += Player2JumpInput;

        #endregion

        multiplayerInput.Enable();

    }

    private void OnDisable()
    {
        #region Player 1 Remove Event Listenrs

        multiplayerInput.Player1Movement.Move.performed -= Player1MoveInput;
        multiplayerInput.Player1Movement.Move.canceled -= Player1MoveInput;

        multiplayerInput.Player1Movement.Jump.performed -= Player1JumpInput;

        #endregion

        #region Player 1 Remove Event Listeners

        multiplayerInput.Player2Movement.Move.performed -= Player2MoveInput;
        multiplayerInput.Player2Movement.Move.canceled -= Player2MoveInput;

        multiplayerInput.Player2Movement.Jump.performed -= Player2JumpInput;

        #endregion
    }


    #region Player 1 Functions

    private void Player1MoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            player1.MovePressed(context.ReadValue<Vector2>());
        }
        else if (context.canceled)
        {
            player1.MoveReleased();
        }
    }

    private void Player1JumpInput(InputAction.CallbackContext inputContext)
    {
        if (inputContext.action.WasPressedThisFrame())
        {
            player1.JumpPressed();
        }
        else if (inputContext.action.WasReleasedThisFrame())
        {
            player1.JumpReleased();
        }
    }

    #endregion

    #region Player 2 Functions

    private void Player2MoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            player2.MovePressed(context.ReadValue<Vector2>());
        }
        else if (context.canceled)
        {
            player2.MoveReleased();
        }
    }

    private void Player2JumpInput(InputAction.CallbackContext inputContext)
    {
        if (inputContext.action.WasPressedThisFrame())
        {
            player2.JumpPressed();
        }
        else if (inputContext.action.WasReleasedThisFrame())
        {
            player2.JumpReleased();
        }
    }

    #endregion



}
