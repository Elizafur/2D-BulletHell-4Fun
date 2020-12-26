using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityHandler : MonoBehaviour
{
    private PlayerMovement _PlayerMovement;
    private Controls Controls;

    public GameObject PulseWave;
    private Animator PulseAnim;

    private AnimateCameraShake camShake;

    public float PulseTime = 2f;

    private bool Pulsing = false;

    // Start is called before the first frame update
    void Start()
    {
        _PlayerMovement = GetComponent<PlayerMovement>();
        Controls = _PlayerMovement.PlayerControls;
        PulseAnim = PulseWave.GetComponent<Animator>();
        camShake = PulseWave.GetComponent<AnimateCameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        float abilityOne = Controls.Player.AbilityOne.ReadValue<float>();
        float abilityTwo = Controls.Player.AbilityTwo.ReadValue<float>();

        if (abilityOne > 0)
        {
            if (!Pulsing) 
            {
                Pulsing = true;
                StartCoroutine(PulseOn());
                camShake.TriggerShake(PulseTime);
            }
        }

        if (abilityTwo > 0)
        {

        }
    }

    IEnumerator PulseOn()
    {
        PulseAnim.Play("on", 0);
        ParticleSystem ps = PulseWave.GetComponentInChildren<ParticleSystem>();
        ps.Play();
        yield return new WaitForSeconds(PulseTime);
        PulseAnim.Play("off", 0);
        Pulsing = false;
    }
}
