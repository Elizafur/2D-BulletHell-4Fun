using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Prime31;

public class PlayerHealth : MonoBehaviour
{
    [ValidateInput("IsValid")]
    public float HP = 100f;     
        private bool IsValid(float value){ return value > 0; }

    public bool invulnerable = false;

    [Required, AssetsOnly]
    public GameObject hitEffect;

    [Required, AssetsOnly]
    public GameObject deathEffect;
    private CharacterController2D character;
    public float deathWaitTime = 0.5f;

    private PlayerAbilityHandler playerAbility;

    void Start()
    {
        character = GetComponent<CharacterController2D>();
        character.onTriggerEnterEvent += onTriggerEnterEvent;

        playerAbility = GetComponent<PlayerAbilityHandler>();
    }

    public bool takeDamage(float val)
    {
        if (invulnerable)
            return false;

        PlayHitEffect();
        HP -= val;

        if (HP <= 0)
            StartCoroutine(die());
            
        return true;
    }

    void PlayHitEffect()
    {
        GameObject o = Instantiate(hitEffect, transform);
        o.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        o.transform.position -= new Vector3(0,5f,0);    //offset 
        ParticleSystem ps = o.GetComponent<ParticleSystem>();
        ps.Play();
    }

    void PlayDeathEffect()
    {
        GameObject o = Instantiate(deathEffect, transform);
        o.transform.position -= new Vector3(0,5f,0);
        ParticleSystem ps = o.GetComponent<ParticleSystem>();
        ps.Play();
    }

    void onTriggerEnterEvent( Collider2D other )
	{
		if (other.gameObject.tag == "EnemyBullet" && !playerAbility.Pulsing)
        {
            
            takeDamage(other.gameObject.GetComponent<BulletHandler>().Damage);
            Destroy(other.gameObject);
            
        }
	}


    private IEnumerator die()
    {
        PlayDeathEffect();
        var x = gameObject.GetComponent<MovementHandler>();
        if (x != null)  x.enabled = false;
        var y = gameObject.GetComponent<SpriteRenderer>();
        if (y != null)  y.enabled = false;

        var z = gameObject.GetComponentInChildren<SpriteRenderer>();
        if (z != null) z.enabled = false;

        yield return new WaitForSeconds(deathWaitTime);
        Destroy(gameObject);
    }
}
