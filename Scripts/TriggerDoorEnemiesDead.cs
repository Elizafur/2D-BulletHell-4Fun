using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TriggerDoorEnemiesDead : MonoBehaviour
{

    [Required]
    public TriggerSpawnEnemies spawner;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawner.allEnemiesDead)
        {
            anim.Play("open", 0);
        }
    }
}
