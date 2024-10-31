using UnityEngine;

public class PlayerObjectReferenceHolder : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;

    public Transform CameraTransform { get; private set; }

    private void OnEnable()
    {
        CameraTransform = playerCamera.transform;
    }


}
