using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Health : MonoBehaviour
{
    [ValidateInput("IsValid")]
    public float HP = 100f;     
        private bool IsValid(float value){ return value > 0; }

    public bool invulnerable = false;

    [Required, AssetsOnly]
    public GameObject hitEffect;

    [Required, AssetsOnly]
    public GameObject deathEffect;

    public float deathWaitTime = 0.5f;

    public Vector3 positionOffsetHit;
    public Vector3 scaleOffsetHit;

    public Vector3 positionOffsetDeath;
    public Vector3 scaleOffsetDeath;

    void Start()
    {
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
        if (scaleOffsetHit != null)
            o.transform.localScale += scaleOffsetHit;

        if (positionOffsetHit != null)
            o.transform.localPosition += positionOffsetHit;

        ParticleSystem ps = o.GetComponent<ParticleSystem>();
        ps.Play();
    }

    void PlayDeathEffect()
    {
        GameObject o = Instantiate(deathEffect, transform);
        if (scaleOffsetDeath != null)
            o.transform.localScale += scaleOffsetDeath;

        if (positionOffsetDeath != null)
            o.transform.localPosition += positionOffsetDeath;

        ParticleSystem ps = o.GetComponent<ParticleSystem>();
        ps.Play();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        // Debug-draw all contact points and normals
        foreach (ContactPoint2D contact in other.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.red);
        }

        if (other.gameObject.tag == "PlayerBullet")
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
