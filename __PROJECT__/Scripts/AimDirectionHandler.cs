using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;

public class AimDirectionHandler : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Controls controls;

    public bool useController = false;

    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        controls = playerMovement.PlayerControls;

        var x = Gamepad.current;
        if (x != null)
            useController = true;
    }
    void Update()
    {
        Vector2 input = Util.GenerateMousePosition(controls);
        
        if (useController)
        {
            input = controls.Player.Look.ReadValue<Vector2>().normalized;
        }

        float ang = Vector2.Angle(input, transform.right);
        Vector3 cross = Vector3.Cross(input, transform.right);

        if (cross.z > 0)
            ang = 360 - ang;

        transform.RotateAround(transform.position, new Vector3(0,0,1), ang);
    }
}
