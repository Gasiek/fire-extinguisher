using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
  [SerializeField] ParticleSystem[] fireParticleSystems = new ParticleSystem[0];
  private float[] startIntensities = new float[0];
  private float currentIntensity = 1.0f;
  private float timeLastWatered = 0;
  private float regenerationDelay = 2.5f;
  private float regenerationRate = 0.1f;
  private bool isLit = true;
  private void Start()
  {
    startIntensities = new float[fireParticleSystems.Length];
    for (int i = 0; i < fireParticleSystems.Length; i++)
    {
      startIntensities[i] = fireParticleSystems[i].emission.rateOverTime.constant;
    }
  }

  private void Update()
  {
    if (isLit && currentIntensity < 1.0f && Time.time - timeLastWatered >= regenerationDelay)
    {
      currentIntensity += regenerationRate * Time.deltaTime;
      ChangeIntensity();
    }
  }

  public void TryExtinguish(float amount)
  {
    timeLastWatered = Time.time;
    ChangeIntensity();
    if (currentIntensity >= 0)
    {
      currentIntensity -= amount;
    }
    if (currentIntensity <= 0)
    {
      StopFire();
      isLit = false;
    }
  }

  private void ChangeIntensity()
  {
    for (int i = 0; i < fireParticleSystems.Length; i++)
    {
      var emission = fireParticleSystems[i].emission;
      emission.rateOverTime = currentIntensity * startIntensities[i];
    }
  }

  private void StopFire()
  {
    for (int i = 0; i < fireParticleSystems.Length; i++)
    {
      fireParticleSystems[i].Stop();
    }
  }

  public void Restart()
  {
    for (int i = 0; i < fireParticleSystems.Length; i++)
    {
      fireParticleSystems[i].Play();
    }
    isLit = true;
  }
}
