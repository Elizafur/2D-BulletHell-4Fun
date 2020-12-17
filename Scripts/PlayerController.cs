using System.Collections;
using System.Collections.Generic;
using Prime31;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
    private CharacterController2D Character;
    private bool Moving = false;

    private float MoveSpeed = .5f;

    // Start is called before the first frame update
    void Start()
    {
        Character = GetComponent<CharacterController2D>();
    }

    void Update()
    {
        if (!Moving)
        {
            StopCoroutine(Movement(new InputAction.CallbackContext(), new Vector2(0,0)));
        }
    } 

    public void Move(InputAction.CallbackContext context)
    {

        InputAction action = context.action;
        action.performed +=
            ctx =>
            {
                ButtonControl b = (ButtonControl)ctx.control;
                Vector2 dir = new Vector2();
                if (b.IsPressed())
                {
                    string s = ctx.control.ToString();
                    print(s);
                    dir += (s == "Key:/Keyboard/w") ? new Vector2(0, MoveSpeed) : new Vector2(0,0);
                    dir += (s == "Key:/Keyboard/a") ? new Vector2(-MoveSpeed, 0) : new Vector2(0,0);
                    dir += (s == "Key:/Keyboard/s") ? new Vector2(0, -MoveSpeed) : new Vector2(0,0);
                    dir += (s == "Key:/Keyboard/d") ? new Vector2(MoveSpeed, 0) : new Vector2(0,0);

                    StartCoroutine(Movement(ctx, dir));
                    /*switch (ctx.control.ToString())
                    {
                        case "Key:/Keyboard/w":
                            break;
                        case "Key:/Keyboard/a":
                            break;
                        case "Key:/Keyboard/s":
                            break;
                        case "Key:/Keyboard/d":
                            break;
                        default:
                            print($"Unknown button{ctx.control}");
                            break;
                    }*/
                }
            };
    }

    private IEnumerator Movement(InputAction.CallbackContext context, Vector2 dir)
    {
        while (((ButtonControl)context.control).isPressed)
        {
            Character.move(dir * Time.deltaTime);
            Moving = true;

            yield return new WaitForEndOfFrame();
        }

        Moving = false;
        yield return null;
    }
}
