using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FireController[] fireControllers;
    [SerializeField] private FireExtinguisherController[] fireExtinguishers;
    [SerializeField] private ReportManager reportManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private SpawnManager spawnManager;

    private int _firesExtinguishedCount;

    private void Awake()
    {
        spawnManager.SpawnFireExtinguishers();
    }

    private void OnEnable()
    {
        EventAggregator.Instance.FireExtinguished += OnFireExtinguished;
        inputManager.OnRestartRequested += RestartGame;
    }

    private void OnDisable()
    {
        EventAggregator.Instance.FireExtinguished -= OnFireExtinguished;
        inputManager.OnRestartRequested -= RestartGame;
    }

    private void OnFireExtinguished()
    {
        _firesExtinguishedCount++;
        if (_firesExtinguishedCount >= fireControllers.Length)
        {
            reportManager.GenerateReport();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}