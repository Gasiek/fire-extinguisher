using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireExtinguisherController : MonoBehaviour
{
  [SerializeField] ParticleSystem ps;
  private float amountExtinguishedPerSecond = 10f;
  [SerializeField] private Transform riffle;
  [SerializeField] private FireController[] fireControllers = new FireController[0];

  public void Fire()
  {
    ps.Play();
  }

  public void Stop()
  {
    ps.Stop();
  }

  private void Update()
  {
    if (ps.isPlaying && Physics.Raycast(riffle.position, riffle.forward, out RaycastHit hit, 3) && hit.transform.CompareTag("Fire"))
    {
      int index = int.Parse(hit.transform.name.Substring(5));
      // if (fireControllers[index].IsLit()) fireControllers[index].TryExtinguish(0.1f * Time.deltaTime * amountExtinguishedPerSecond);
      fireControllers[index].TryExtinguish(0.1f * Time.deltaTime * amountExtinguishedPerSecond);
    }
  }
}
