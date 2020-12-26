using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EnemyAttackHandler : MonoBehaviour
{
    [ShowInInspector]
    public Transform target;

    [ShowInInspector]
    public Transform firePoint;

    [ShowInInspector, AssetsOnly]
    public GameObject bulletPrefab;

    private bool shooting = false;
    [ShowInInspector]
    public float shotTime = 1f;

    private Vector2 DirectionShot;

    void Start()
    {
        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
        }

        StartCoroutine(SpawnBullet());
    }

    void Update()
    {
        DirectionShot = LookAtPlayer();
 
        RaycastHit2D rh;
        if (rh=Physics2D.Raycast(transform.position, DirectionShot, 100f, ~LayerMask.GetMask("Enemy")))
        {
            if (rh.collider.tag == "Player")
            {
                if (!shooting)
                {
                    shooting = true;
                }
            }
            else
            {
                shooting = false;
            }
        }

        
    }

    //Returns direction to player.
    Vector2 LookAtPlayer()
    {
        Vector2 dir = target.transform.position - transform.position; 
        float ang = Vector2.Angle(dir, transform.right);
        Vector3 cross = Vector3.Cross(dir, transform.right);
        
        if (cross.z > 0)
            ang = 360 - ang;

        transform.RotateAround(transform.position, new Vector3(0,0,1), ang);

        return dir;
    }


    IEnumerator SpawnBullet()
    {
        for(;;) 
        {
            if (shooting)
            {
                GameObject shot = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                shot.GetComponent<BulletHandler>().Direction = DirectionShot;
            }

            yield return new WaitForSeconds(shotTime);
        }
    }
}
