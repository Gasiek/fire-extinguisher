using UnityEngine;

[CreateAssetMenu(fileName = "FireSettings", menuName = "ScriptableObjects/FireSettings", order = 1)]
public class FireSettings : ScriptableObject
{
    public float regenerationDelay = 2.5f;
    public float regenerationRate = 0.1f;
    public float initialVolume = 1.0f;
}