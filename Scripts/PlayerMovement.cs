using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{
    public Controls PlayerControls;
    private CharacterController2D Character;
    private float MoveSpeed = 5f;

    void Awake()
    {
        PlayerControls = new Controls();
    }

    private void OnEnable()
    {
        PlayerControls.Enable();
    }

    private void OnDisable()
    {
        PlayerControls.Disable();
    }


    void Start()
    {
        Character = GetComponent<CharacterController2D>();
    }

    void Update()
    {
        Vector2 input = PlayerControls.Player.Move.ReadValue<Vector2>();

        Vector2 moveDir = input * MoveSpeed;

        Character.move(moveDir * Time.deltaTime);
        
    }
}
