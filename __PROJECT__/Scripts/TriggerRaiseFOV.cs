using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRaiseFOV : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerTriggerCollider")
            Camera.main.fieldOfView = 125f;
    }

}
