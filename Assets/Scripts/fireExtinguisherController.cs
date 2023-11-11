using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireExtinguisherController : MonoBehaviour
{
  [SerializeField] private ParticleSystem ps;
  [SerializeField] private Transform riffle;
  [SerializeField] private Rigidbody nozzleRigidbody;
  [SerializeField] private FixedJoint nozzleJoint;
  [SerializeField] private Transform nozzlePositionHolder;
  [SerializeField] private AudioSource nozzleAudioSource;
  [SerializeField] private AudioSource buttonAudioSource;
  private float amountExtinguishedPerSecond = 10f;
  private Vector3 initialNozzleOffset;
  private Quaternion initialNozzleRotation;
  private float extinguishingTimeLeft = 18;
  private bool isExtinguishing = false;

  public void Fire()
  {
    buttonAudioSource.Play();
    if (extinguishingTimeLeft <= 0)
    {
      return;
    }
    isExtinguishing = true;
    nozzleAudioSource.Play();
    ps.Play();
  }

  public void Stop()
  {
    isExtinguishing = false;
    nozzleAudioSource.Stop();
    ps.Stop();
  }

  private void Update()
  {
    if (isExtinguishing)
    {
      extinguishingTimeLeft -= Time.deltaTime;
      if (extinguishingTimeLeft <= 0)
      {
        Stop();
      }
    }
    if (ps.isPlaying && Physics.Raycast(riffle.position, riffle.forward, out RaycastHit hit, 3) && hit.transform.CompareTag("Fire"))
    {
      hit.transform.GetComponent<FireController>().TryExtinguish(0.1f * Time.deltaTime * amountExtinguishedPerSecond);
    }
  }

  public void DetachNozzle()
  {
    nozzleJoint.connectedBody = null;
  }

  public void AttachNozzle(Rigidbody rb)
  {
    nozzleRigidbody.velocity = Vector3.zero;
    nozzleRigidbody.angularVelocity = Vector3.zero;
    nozzleRigidbody.isKinematic = true;
    nozzleRigidbody.transform.position = nozzlePositionHolder.position;
    nozzleRigidbody.transform.rotation = nozzlePositionHolder.rotation;
    nozzleRigidbody.isKinematic = false;
    nozzleJoint.connectedBody = rb;
  }
}
