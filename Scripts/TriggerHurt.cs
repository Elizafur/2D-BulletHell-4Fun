using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class TriggerHurt : MonoBehaviour
{
    public float Damage = 100f;

    public bool DamagePlayer = false;
    private CharacterController2D character;
    
    void Awake()
    {
        character = GetComponentInParent<CharacterController2D>();
        character.onTriggerEnterEvent += OnTriggerEnter2D;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !DamagePlayer)
            return;

        if (other.tag == "PlayerBullet" || other.tag == "EnemyBullet")
        {
            Destroy(other.gameObject);
        }

        Health h = other.gameObject.GetComponent<Health>();
        if (h == null)  return;

        h.takeDamage(Damage);
        
    }
}
