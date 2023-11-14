using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportManager : MonoBehaviour
{
    public GameObject reportPanel;
    public TMPro.TextMeshProUGUI timeUsedText;
    public TMPro.TextMeshProUGUI numberOfExtinguishersFinishedText;
    private int numberOfExtinguishersUsed;
    private DateTime startTime;

    private void Start()
    {
        startTime = DateTime.Now;
    }

    private void Update()
    {
        reportPanel.transform.LookAt(Camera.main.transform);
        reportPanel.transform.Rotate(0, 180, 0);
        reportPanel.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
    }

    private void GenerateReport()
    {
        timeUsedText.text = (DateTime.Now - startTime).ToString();
        numberOfExtinguishersFinishedText.text = numberOfExtinguishersUsed.ToString();
    }

    public void OnFireExtinguisherFinished()
    {
        numberOfExtinguishersUsed++;
    }
}
