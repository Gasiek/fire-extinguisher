using UnityEngine;

public class InputManager : MonoBehaviour
{
    private XRIDefaultInputActions _inputActions;
    private XRDeviceSimulatorControls _simulatorControls;
    public System.Action OnRestartRequested;

    private void Awake()
    {
        _inputActions = new XRIDefaultInputActions();
        _simulatorControls = new XRDeviceSimulatorControls();
    }

    private void OnEnable()
    {
        _simulatorControls.InputControls.Enable();
        _inputActions.XRILeftHandInteraction.Enable();

        _inputActions.XRILeftHandInteraction.XButtonPress.performed += ctx => 
        { 
            if (ctx.performed) OnRestartRequested?.Invoke();
        };
        _simulatorControls.InputControls.PrimaryButton.performed += ctx => 
        { 
            if (ctx.performed) OnRestartRequested?.Invoke();
        };
    }

    private void OnDisable()
    {
        _simulatorControls.InputControls.Disable();
        _inputActions.XRILeftHandInteraction.Disable();
    }
}