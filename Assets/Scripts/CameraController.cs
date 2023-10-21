using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform VRHeadset;
    private Camera camera;
    private Vector3 posOffset;
    private Vector3 VRHeadsetOffset;
    private bool VRHeadsetPosChanged = false;
    private bool settingUp = true;
    private Vector3 initialVRHeadsetPos;
    private Vector3 goodInitialVRHeadsetPos;
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
                goodInitialVRHeadsetPos = VRHeadset.position;
                if (goodInitialVRHeadsetPos.y < 1.5f) goodInitialVRHeadsetPos.y = 1.5f;
                Debug.Log(posOffset);
            }
            else if (VRHeadsetPosChanged)
            {
                if (settingUp)
                {
                    Debug.Log("Setting up");
                    transform.position = VRHeadset.position + posOffset;
                    transform.rotation = VRHeadset.rotation;
                    VRHeadsetOffset = VRHeadset.position;
                }
                else
                {
                    Debug.Log("Not setting up");
                    transform.position = new Vector3(posOffset.x - VRHeadset.position.x + 2 * VRHeadsetOffset.x, goodInitialVRHeadsetPos.y, posOffset.z - VRHeadset.position.z);
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
