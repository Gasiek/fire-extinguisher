using UnityEngine;

public class FireController : MonoBehaviour, IFire
{
    [SerializeField] private ParticleSystem[] fireParticleSystems;
    [SerializeField] private float[] fireStartIntensities;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private FireSettings fireSettings;

    private float _currentIntensity = 1.0f;
    private float _timeLastWatered;
    private bool _isLit = true;

    private void Start()
    {
        fireStartIntensities = new float[fireParticleSystems.Length];
        for (int i = 0; i < fireParticleSystems.Length; i++)
        {
            fireStartIntensities[i] = fireParticleSystems[i].emission.rateOverTime.constant;
        }
        audioSource.volume = fireSettings.initialVolume;
    }

    private void Update()
    {
        if (_isLit && _currentIntensity < 1.0f && (Time.time - _timeLastWatered) >= fireSettings.regenerationDelay)
        {
            _currentIntensity += fireSettings.regenerationRate * Time.deltaTime;
            UpdateFireAppearance();
        }
    }

    public void ApplyExtinguisherEffect(float amount)
    {
        _timeLastWatered = Time.time;
        _currentIntensity -= amount;
        if (_currentIntensity <= 0 && _isLit)
        {
            ExtinguishFire();
        }
        UpdateFireAppearance();
    }

    private void UpdateFireAppearance()
    {
        audioSource.volume = fireSettings.initialVolume * _currentIntensity;
        for (int i = 0; i < fireParticleSystems.Length; i++)
        {
            var emission = fireParticleSystems[i].emission;
            emission.rateOverTime = _currentIntensity * fireStartIntensities[i];
        }
    }

    private void ExtinguishFire()
    {
        audioSource.Stop();
        foreach (var ps in fireParticleSystems)
        {
            ps.Stop();
        }
        _isLit = false;
        EventAggregator.Instance.PublishFireExtinguished();
    }

    public void Restart()
    {
        audioSource.Play();
        _currentIntensity = 1.0f;
        foreach (var ps in fireParticleSystems)
        {
            ps.Play();
        }
        _isLit = true;
    }

    public void TryExtinguish(float amount)
    {
        if (_isLit)
        {
            ApplyExtinguisherEffect(amount);
        }
    }
}
