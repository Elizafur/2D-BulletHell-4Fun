using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;
using Prime31;

public class PlayerHitDirectionHandler : MonoBehaviour
{
    [Required]
    public FaceHandler face;

    private CharacterController2D character;
    private CharacterController2D.CharacterCollisionState2D collisionState;
    void Start()
    {
        character = GetComponent<CharacterController2D>();
        collisionState = character.collisionState;
    }

    void Update()
    {
        float r = Random.Range(0, 100);
        float noInterruptChance = 60;

        if (collisionState.left)
        {
            if (r > noInterruptChance)
                face.InterruptReplaceImage_DirectionHit(true);
        }
        else if (collisionState.right)
            if (r > noInterruptChance)
                face.InterruptReplaceImage_DirectionHit(false);
        {

        }
    }



}
