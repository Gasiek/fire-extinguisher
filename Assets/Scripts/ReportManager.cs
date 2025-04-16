using System;
using UnityEngine;
using TMPro;

public class ReportManager : MonoBehaviour
{
    public GameObject reportPanel;
    public TextMeshProUGUI timeUsedToExtinguishText;
    public TextMeshProUGUI timeUsedToActivateExtinguisherText;
    public TextMeshProUGUI timeUsedToFindExtinguisherText;
    public TextMeshProUGUI numberOfExtinguishersFinishedText;

    private float _timeUsedToActivateExtinguisher;
    private float _timeUsedToFindExtinguisher;
    private int _numberOfExtinguishersUsed = 0;
    private float _gameStartTime;

    private void OnEnable()
    {
        EventAggregator.Instance.ExtinguisherPickedUp += OnExtinguisherPickedUp;
        EventAggregator.Instance.ExtinguisherActivated += OnExtinguisherActivated;
        EventAggregator.Instance.ExtinguisherStarted += OnExtinguisherStarted;
    }

    private void OnDisable()
    {
        EventAggregator.Instance.ExtinguisherPickedUp -= OnExtinguisherPickedUp;
        EventAggregator.Instance.ExtinguisherActivated -= OnExtinguisherActivated;
        EventAggregator.Instance.ExtinguisherStarted -= OnExtinguisherStarted;
    }
    
    private void Start()
    {
        _gameStartTime = Time.time;
        reportPanel.SetActive(false);
    }
    
    public void GenerateReport()
    {
        numberOfExtinguishersFinishedText.text = _numberOfExtinguishersUsed.ToString();
        timeUsedToExtinguishText.text = (Time.time - _gameStartTime).ToString("F1") + "s";
        timeUsedToActivateExtinguisherText.text = _timeUsedToActivateExtinguisher.ToString("F1") + "s";
        timeUsedToFindExtinguisherText.text = _timeUsedToFindExtinguisher.ToString("F1") + "s";
        reportPanel.SetActive(true);
    }

    private void OnExtinguisherStarted()
    {
        _numberOfExtinguishersUsed++;
    }

    private void OnExtinguisherActivated(DateTime pickUpTime)
    {
        if (_timeUsedToActivateExtinguisher == 0)
        {
            _timeUsedToActivateExtinguisher = (float)(DateTime.Now - pickUpTime).TotalSeconds;
        }
    }

    private void OnExtinguisherPickedUp()
    {
        if (_timeUsedToFindExtinguisher == 0)
        {
            _timeUsedToFindExtinguisher = Time.time - _gameStartTime;
        }
    }
}