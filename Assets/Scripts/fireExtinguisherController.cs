using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class fireExtinguisherController : MonoBehaviour
{
  [SerializeField] private ParticleSystem ps;
  [SerializeField] private Transform riffle;
  [SerializeField] private Rigidbody nozzleRigidbody;
  [SerializeField] private FixedJoint nozzleJoint;
  [SerializeField] private Transform nozzlePositionHolder;
  [SerializeField] private AudioSource nozzleAudioSource;
  [SerializeField] private AudioSource buttonAudioSource;
  [SerializeField] private ParentConstraint boltParentConstraint;
  [SerializeField] private BoxCollider boltCollider;
  [SerializeField] private ReportManager reportManager;
  private float amountExtinguishedPerSecond = 10f;
  private float extinguishingTimeLeft = 18;
  private bool isExtinguishing = false;
  private bool boltRemoved = false;

  public void Fire()
  {
    buttonAudioSource.Play();
    if (!boltRemoved)
    {
      return;
    }
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
      if (extinguishingTimeLeft < 0)
      {
        reportManager.OnFireExtinguisherFinished();
        Stop();
      }
    }
    if (isExtinguishing && Physics.Raycast(riffle.position, riffle.forward, out RaycastHit hit, 3) && hit.transform.CompareTag("Fire"))
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

  public void DetachBolt()
  {
    boltParentConstraint.constraintActive = false;
    boltRemoved = true;
  }

  public void ReleaseBolt()
  {
    boltCollider.isTrigger = false;
  }
}
