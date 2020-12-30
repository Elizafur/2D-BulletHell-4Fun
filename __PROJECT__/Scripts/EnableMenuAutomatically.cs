using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EnableMenuAutomatically : MonoBehaviour
{
    [Required]
    public GameObject canvas;

    void Update()
    {
        if (!canvas.activeSelf)
            canvas.SetActive(true);
    }
}
