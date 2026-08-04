using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class player_input : MonoBehaviour
{

    public static event Action on_jump;
    public static float horizontal { get; private set; }
    public static bool jump_held { get; private set; }
    private void Update()
    {
        jump_held = Keyboard.current.spaceKey.isPressed;

        horizontal = 0;

        if (Keyboard.current.aKey.isPressed)
        {
            horizontal--;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            horizontal++;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            on_jump?.Invoke();
        }

    }

}
