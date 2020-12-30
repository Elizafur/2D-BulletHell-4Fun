using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullets : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "PlayerBullet" || other.collider.tag == "EnemyBullet")
            Destroy(other.gameObject);
        else if (other.collider.tag == "Enemy" || other.collider.tag == "Player")
        {
            other.otherRigidbody.velocity = -other.otherRigidbody.velocity;
        }
    }
}
