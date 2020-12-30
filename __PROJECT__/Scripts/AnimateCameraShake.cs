using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCameraShake : MonoBehaviour
{
    // Transform of the GameObject you want to shake
    private Transform _Transform;

    // Desired duration of the shake effect
    private float shakeDuration = 0f;
    // A measure of magnitude for the shake. Tweak based on your preference
    public float shakeMagnitude = 0.7f;

    // A measure of how quickly the shake effect should evaporate
    public float dampingSpeed = 1.0f;

    // The initial position of the GameObject
    Vector3 initialPosition;

    void Awake()
    {
        if (_Transform == null)
        {
            _Transform = Camera.main.transform;
        }
    }

    void OnEnable()
    {
        initialPosition = _Transform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            _Transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            _Transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake(float _shakeDuration) 
    {
        shakeDuration = _shakeDuration;
    }
}
