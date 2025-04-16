using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private FireExtinguisherController[] fireExtinguishers;

    public void SpawnFireExtinguishers()
    {
        foreach (var extinguisher in fireExtinguishers)
        {
            extinguisher.gameObject.SetActive(true);
        }
    }
}