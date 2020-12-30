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
    public float MoveSpeed = 5f;
    public float dashSpeed = 30f;

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

        bool dash = PlayerControls.Player.Dash.triggered;
        if (dash)
        {
            Vector2 moveDir = input * dashSpeed;

            Character.move(moveDir * Time.deltaTime);
        }
        else
        {
            Vector2 moveDir = input * MoveSpeed;

            Character.move(moveDir * Time.deltaTime);
        }
    }
}
