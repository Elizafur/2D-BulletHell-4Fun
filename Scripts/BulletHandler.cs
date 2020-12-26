using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    public Vector2 Direction;
    public float Speed = 10f;
    private Rigidbody2D rb;

    public float Damage = 8f;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 deltaPosition = Direction.normalized * Speed;

        if (Direction == Vector2.zero)
        {
            rb.velocity = Speed * transform.right;
            return;
        }
        rb.velocity = deltaPosition;
        
    }

}
