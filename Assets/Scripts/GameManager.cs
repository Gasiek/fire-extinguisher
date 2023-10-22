using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
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
        inputActions.XRILeftHandInteraction.XButtonPress.performed += SpawnFireExtinguisher;
        simulatorControls.InputControls.PrimaryButton.performed += SpawnFireExtinguisher;
    }

    private void SpawnFireExtinguisher(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (activeFireExtinguisher != null)
            {
                Destroy(activeFireExtinguisher);
            }
            activeFireExtinguisher = Instantiate(fireExtinguisher, fireExtinguisherSpawnPoint.position, fireExtinguisherSpawnPoint.rotation);
            RestartFire();
        }
    }

    private void RestartFire()
    {
        foreach (var fireController in fireControllers)
        {
            fireController.Restart();
        }
    }
}
