using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform VRHeadset;
    private Camera camera;
    private Vector3 posOffset;
    private bool VRHeadsetPosChanged = false;
    private bool settingUp = true;
    private Vector3 initialVRHeadsetPos;
    public bool VRMode { get; private set; } = false;
    private XRIDefaultInputActions inputActions;
    private XRDeviceSimulatorControls simulatorControls;

    private void Awake()
    {
        inputActions = new XRIDefaultInputActions();
        simulatorControls = new XRDeviceSimulatorControls();
    }

    void Start()
    {
        Debug.Log(VRHeadset.position);
        camera = GetComponent<Camera>();
        initialVRHeadsetPos = VRHeadset.position;
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        simulatorControls.InputControls.Enable();
        inputActions.XRILeftHandInteraction.Enable();
        inputActions.XRILeftHandInteraction.XButtonPress.performed += TurnOnVRMode;
        inputActions.XRILeftHandInteraction.YButtonPress.performed += FinishSettingUpVRHeadset;
        simulatorControls.InputControls.PrimaryButton.performed += TurnOnVRMode;
        simulatorControls.InputControls.SecondaryButton.performed += FinishSettingUpVRHeadset;
    }

    void Update()
    {
        if (!VRMode)
        {
            if (!VRHeadsetPosChanged && VRHeadset.position != initialVRHeadsetPos)
            {
                Debug.Log("VRHeadsetPosChanged");
                VRHeadsetPosChanged = true;
                posOffset = transform.position - VRHeadset.position;
            }
            else if (VRHeadsetPosChanged)
            {
                if (settingUp)
                {
                    Debug.Log("Setting up");
                    transform.position = VRHeadset.position + posOffset;
                    transform.rotation = VRHeadset.rotation;
                }
                else
                {
                    Debug.Log("Not setting up");
                    transform.position = new Vector3(-VRHeadset.position.x, VRHeadset.position.y, -VRHeadset.position.z) + posOffset;
                    transform.eulerAngles = new Vector3(VRHeadset.eulerAngles.x, VRHeadset.eulerAngles.y + 180, VRHeadset.eulerAngles.z);
                }
            }
        }
    }

    public void FinishSettingUpVRHeadset(InputAction.CallbackContext context)
    {
        Debug.Log("FinishSettingUpVRHeadset");
        if (context.performed)
        {
            settingUp = false;
        }
    }

    public void TurnOnVRMode(InputAction.CallbackContext context)
    {
        Debug.Log("TurnOnVRMode");
        if (context.performed)
        {
            VRMode = true;
            if (camera != null)
            {
                camera.enabled = false;
            }
        }
    }
}
