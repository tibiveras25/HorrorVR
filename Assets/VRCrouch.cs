using UnityEngine;
using UnityEngine.InputSystem;

public class VRCrouch : MonoBehaviour
{
    [Header("Setup")]
    public Transform cameraOffset;
    public CharacterController characterController;
    public InputActionProperty crouchAction;

    [Header("The Heights You Want")]
    public float standingEyeHeight = 1.3f; // This is the 1.3 height you like
    public float crouchEyeHeight = 0.7f;   // This is the 0.7 height you like

    [Header("Physical Body Settings")]
    public float standingBodyHeight = 2.0f;
    public float crouchBodyHeight = 1.0f;

    private bool isCrouching = false;

    void Start()
    {
        if (crouchAction.action != null) crouchAction.action.Enable();

        // --- THIS IS THE FIX ---
        // Force the game to start at 1.3 immediately, 
        // ignoring your sitting height (0.73).
        isCrouching = false;
        ApplyHeight(standingEyeHeight, standingBodyHeight);
    }

    void Update()
    {
        if (crouchAction.action.WasPressedThisFrame())
        {
            isCrouching = !isCrouching;
            if (isCrouching)
                ApplyHeight(crouchEyeHeight, crouchBodyHeight);
            else
                ApplyHeight(standingEyeHeight, standingBodyHeight);
        }
    }

    void ApplyHeight(float eyeHeight, float bodyHeight)
    {
        cameraOffset.localPosition = new Vector3(0, eyeHeight, 0);
        characterController.height = bodyHeight;
        characterController.center = new Vector3(0, bodyHeight / 2f, 0);
    }
}