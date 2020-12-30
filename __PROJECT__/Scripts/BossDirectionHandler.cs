using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BossDirectionHandler : MonoBehaviour
{
    [ShowInInspector]
    public Transform target;

    void Update()
    {
        LookAtPlayer();
    }

    //Returns direction to player.
    Vector2 LookAtPlayer()
    {
        Vector2 dir = target.transform.position - transform.position; 
        float ang = Vector2.Angle(dir, transform.right);
        Vector3 cross = Vector3.Cross(dir, transform.right);
        
        if (cross.z > 0)
            ang = 360 - ang;

        transform.RotateAround(transform.position, new Vector3(0,0,1), ang-90f);//offset by 90 degrees

        return dir;
    }

}
