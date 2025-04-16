using System;
using UnityEngine;
using UnityEngine.Animations;

public class FireExtinguisherController : MonoBehaviour, IExtinguisher
{
    [Header("VFX & Audio")]
    public ParticleSystem extinguishVFX;
    public AudioSource nozzleAudioSource;
    public AudioSource buttonAudioSource;

    [Header("Nozzle Setup")]
    public Transform rayOrigin;
    public Rigidbody nozzleRigidbody;
    public FixedJoint nozzleJoint;
    public Transform nozzlePositionHolder;

    [Header("Bolt Setup")]
    public ParentConstraint boltParentConstraint;
    public BoxCollider boltCollider;

    [Header("Settings")]
    public float amountExtinguishedPerSecond = 10f;
    public float extinguishingDuration = 18f;


    private ExtinguisherState _currentState;
    public float ExtinguishingTime { get; private set; }
    public bool IsBoltRemoved { get; private set; }
    public DateTime PickupTime { get; private set; }

    
    private void Awake()
    {
        ExtinguishingTime = extinguishingDuration;
        ChangeState(new IdleState(this));
    }

    private void Update()
    {
        _currentState?.Update();

        if (_currentState is ActivatedState)
        {
            ExtinguishingTime -= Time.deltaTime;
            HandleExtinguishing();
        }
    }

    public void ChangeState(ExtinguisherState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void PickUp()
    {
        if (_currentState is IdleState)
        {
            PickupTime = DateTime.Now;
            ChangeState(new PickedUpState(this));
        }
    }

    public void RemoveBolt()
    {
        boltParentConstraint.constraintActive = false;
        if (!IsBoltRemoved)
        {
            IsBoltRemoved = true;
            EventAggregator.Instance.PublishExtinguisherStarted();
        }
        if (_currentState is PickedUpState)
        {
            ChangeState(new ReadyState(this));
        }
    }
    
    public void StartExtinguishing()
    {
        nozzleAudioSource.Play();
        extinguishVFX.Play();
    }

    public void StopExtinguishing()
    {
        nozzleAudioSource.Stop();
        extinguishVFX.Stop();
    }

    public void Activate()
    {
        if (!IsBoltRemoved)
        {
            buttonAudioSource.Play();
            return;
        }

        if (!(_currentState is ActivatedState))
        {
            ChangeState(new ActivatedState(this));
        }
    }

    public void Deactivate()
    {
        if (_currentState is ActivatedState)
        {
            ChangeState(new ReadyState(this));
        }
    }
    
    private void HandleExtinguishing()
    {
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out RaycastHit hit, 3f))
        {
            if (hit.transform.CompareTag("Fire"))
            {
                if (hit.transform.TryGetComponent<FireController>(out var fire))
                {
                    fire.ApplyExtinguisherEffect(0.1f * Time.deltaTime * amountExtinguishedPerSecond);
                }
            }
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
