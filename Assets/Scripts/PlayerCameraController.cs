using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerCameraController : MonoBehaviour
{
    private const float NORMAL_GRAVITY_DUTCH_ANGLE = 0f;
    private const float FLIPPED_GRAVITY_DUTCH_ANGLE = 180f;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [SerializeField] private float normalGravityCameraAngle = 24f;
    [SerializeField] private float flippedGravityCameraAngle = -24f;

    private PlayerMovement playerMovement;


    private void OnEnable()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.OnGravityChanged += ChangeCameraState;
    }

    private void OnDisable()
    {
        playerMovement.OnGravityChanged -= ChangeCameraState;
    }

    private void Start()
    {
        virtualCamera.transform.rotation = Quaternion.Euler(normalGravityCameraAngle, 0f, 0f);
        virtualCamera.m_Lens.Dutch = NORMAL_GRAVITY_DUTCH_ANGLE;
    }

    private void ChangeCameraState(PlayerMovement.GravityDirection gravityDirection)
    {
        switch (gravityDirection) 
        {
            case PlayerMovement.GravityDirection.GravityDown:
                SetCameraSettingsForNormalGravity();
                break;
            case PlayerMovement.GravityDirection.GravityUp:
                SetCameraSettingsForFlippedGravity();
                break;
        }
    }


    private void SetCameraSettingsForNormalGravity()
    {
        virtualCamera.transform.rotation = Quaternion.Euler(normalGravityCameraAngle, 0f, 0f);
        virtualCamera.m_Lens.Dutch = NORMAL_GRAVITY_DUTCH_ANGLE;
    }

    private void SetCameraSettingsForFlippedGravity()
    {
        virtualCamera.transform.rotation = Quaternion.Euler(flippedGravityCameraAngle, 0f, 0f);
        virtualCamera.m_Lens.Dutch = FLIPPED_GRAVITY_DUTCH_ANGLE;
    }


}
