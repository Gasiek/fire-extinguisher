using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public CameraController cameraController;
    [SerializeField] private GameObject fireExtinguisher;
    [SerializeField] private Transform fireExtinguisherSpawnPoint;
    [SerializeField] private FireController[] fireControllers;
    private GameObject activeFireExtinguisher;
    private XRIDefaultInputActions inputActions;
    private XRDeviceSimulatorControls simulatorControls;
    void Awake()
    {
        inputActions = new XRIDefaultInputActions();
        simulatorControls = new XRDeviceSimulatorControls();
        activeFireExtinguisher = Instantiate(fireExtinguisher, fireExtinguisherSpawnPoint.position, fireExtinguisherSpawnPoint.rotation);
    }

    private void OnEnable()
    {
        simulatorControls.InputControls.Enable();
        inputActions.XRILeftHandInteraction.Enable();
        inputActions.XRILeftHandInteraction.XButtonPress.performed += RestartGame;
        simulatorControls.InputControls.PrimaryButton.performed += RestartGame;
    }

    private void RestartGame(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SpawnFireExtinguisher();
            RestartFire();
        }
    }

    private void SpawnFireExtinguisher()
    {
        if (activeFireExtinguisher != null)
        {
            Destroy(activeFireExtinguisher);
        }
        activeFireExtinguisher = Instantiate(fireExtinguisher, fireExtinguisherSpawnPoint.position, fireExtinguisherSpawnPoint.rotation);
    }

    private void RestartFire()
    {
        foreach (var fireController in fireControllers)
        {
            fireController.Restart();
        }
    }
}
