using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityHandler : MonoBehaviour
{
    private PlayerMovement _PlayerMovement;
    private Controls Controls;

    public GameObject PulseWave;
    private Animator PulseAnim;

    private Collider2D PulseCollider;

    private AnimateCameraShake camShake;

    public float PulseTime = 2f;
    public float PulseCD = 10f;
    public float PulseCDTimer = 0f;

    public bool Pulsing = false;

    // Start is called before the first frame update
    void Start()
    {
        _PlayerMovement = GetComponent<PlayerMovement>();
        Controls = _PlayerMovement.PlayerControls;
        PulseAnim = PulseWave.GetComponent<Animator>();
        camShake = PulseWave.GetComponent<AnimateCameraShake>();
        PulseCollider = PulseWave.GetComponent<Collider2D>();
        PulseCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float abilityOne = Controls.Player.AbilityOne.ReadValue<float>();
        float abilityTwo = Controls.Player.AbilityTwo.ReadValue<float>();
        if (PulseCDTimer > 0)
        {
            PulseCDTimer -= Time.deltaTime;
        }


        if (abilityOne > 0)
        {
            if (!Pulsing && PulseCDTimer <= 0) 
            {
                Pulsing = true;
                StartCoroutine(PulseOn());
                PulseCDTimer = 10f;
                camShake.TriggerShake(PulseTime);
            }
        }

        if (abilityTwo > 0)
        {

        }
    }

    IEnumerator PulseOn()
    {
        PulseCollider.enabled = true;

        PulseAnim.Play("on", 0);
        ParticleSystem ps = PulseWave.GetComponentInChildren<ParticleSystem>();
        ps.Play();
        yield return new WaitForSeconds(PulseTime);
        PulseAnim.Play("off", 0);

        PulseCollider.enabled = false;
        Pulsing = false;
    }
}
