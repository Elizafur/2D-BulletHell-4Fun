using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class Util
{
    public static Vector2 GenerateMousePosition(Controls controls)
    {
        float distanceFromCamera = 500f;
        Vector3 mousePoint = controls.Player.Look.ReadValue<Vector2>();
        
        mousePoint.z = distanceFromCamera;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
