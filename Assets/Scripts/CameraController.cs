using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform VRHeadset;
    private Camera noHeadsetCamera;
    [SerializeField] private Transform noHeadsetCameraInitialPosition;
    private Vector3 posOffset;
    private bool VRHeadsetPosChanged = false;
    private Vector3 initialVRHeadsetPos;
    public bool VRMode { get; private set; } = true;
    private XRIDefaultInputActions inputActions;
    private XRDeviceSimulatorControls simulatorControls;

    private void Awake()
    {
        inputActions = new XRIDefaultInputActions();
        simulatorControls = new XRDeviceSimulatorControls();
    }

    void Start()
    {
        noHeadsetCamera = GetComponent<Camera>();
        initialVRHeadsetPos = VRHeadset.position;
    }

    private void OnEnable()
    {
        // simulatorControls.InputControls.Enable();
        // inputActions.XRILeftHandInteraction.Enable();
        // inputActions.XRILeftHandInteraction.YButtonPress.performed += TurnOffVRMode;
        // simulatorControls.InputControls.SecondaryButton.performed += TurnOffVRMode;
    }

    void Update()
    {
        if (!VRMode)
        {
            if (!VRHeadsetPosChanged && VRHeadset.position != initialVRHeadsetPos)
            {
                Debug.Log("VRHeadsetPosChanged");
                VRHeadsetPosChanged = true;
                posOffset = noHeadsetCameraInitialPosition.position - VRHeadset.position;
            }
            else if (VRHeadsetPosChanged)
            {
                {
                    Debug.Log("Not setting up");
                    transform.position = new Vector3(-posOffset.x - VRHeadset.position.x, noHeadsetCameraInitialPosition.position.y, posOffset.z - VRHeadset.position.z);
                    transform.eulerAngles = new Vector3(VRHeadset.eulerAngles.x, VRHeadset.eulerAngles.y + 180, VRHeadset.eulerAngles.z);
                }
            }
        }
    }

    public void TurnOffVRMode(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            VRMode = false;
            if (noHeadsetCamera != null)
            {
                noHeadsetCamera.enabled = true;
            }
        }
    }
}
