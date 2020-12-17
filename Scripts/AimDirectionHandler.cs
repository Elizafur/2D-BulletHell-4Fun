using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class AimDirectionHandler : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Controls controls;
    public Transform pivot;
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        controls = playerMovement.PlayerControls;
    }
    void Update()
    {
        Vector2 input = controls.Player.Look.ReadValue<Vector2>();
        input = input.normalized;
        float ang = Vector2.Angle(input, transform.right);
        Vector3 cross = Vector3.Cross(input, transform.right);
        
        if (cross.z > 0)
            ang = 360 - ang;
        print (ang);

        transform.RotateAround(transform.position, new Vector3(0,0,1), ang);
    }
}
