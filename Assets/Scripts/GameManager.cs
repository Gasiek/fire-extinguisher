using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CameraController cameraController;
    [SerializeField] private GameObject fireExtinguisher;
    [SerializeField] private Transform[] fireExtinguisherSpawnPoints;
    [SerializeField] private FireController[] fireControllers;
    private XRIDefaultInputActions inputActions;
    private XRDeviceSimulatorControls simulatorControls;
    private int numberOfFiresExtinguished = 0;
    void Awake()
    {
        inputActions = new XRIDefaultInputActions();
        simulatorControls = new XRDeviceSimulatorControls();
        SpawnFireExtinguishers();
    }

    private void OnEnable()
    {
        simulatorControls.InputControls.Enable();
        inputActions.XRILeftHandInteraction.Enable();
        inputActions.XRILeftHandInteraction.XButtonPress.performed += RestartGame;
        simulatorControls.InputControls.PrimaryButton.performed += RestartGame;
    }

    private void SpawnFireExtinguishers()
    {
        foreach (var spawnPoint in fireExtinguisherSpawnPoints)
        {
            Instantiate(fireExtinguisher, spawnPoint.position, spawnPoint.rotation);
        }
    }

    private void RestartGame(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // SpawnFireExtinguisher();
            // RestartFire();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnFireFinished()
    {

    }
}
