using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private float keyHorizontal;
    [SerializeField] private float keyVertical;
    [SerializeField] private bool jumpPressed;
    [SerializeField] private bool attackPressed;

    public float KeyHorizontal
    {
        get
        {
            return keyHorizontal;
        }
    }
    public float KeyVertical
    {
        get
        {
            return keyVertical;
        }
    }
    public bool JumpPressed
    {
        get
        {
            return jumpPressed;
        }
    }
    public bool AttackPressed
    {
        get
        {
            return attackPressed;
        }
    }

    void Update()
    {
        this.InputHorizontalAndVertical();
        this.InputJump();
        this.InputATK();
    }

    private void InputHorizontalAndVertical()
    {
        keyHorizontal = Input.GetAxisRaw("Horizontal");
        keyVertical = Input.GetAxisRaw("Vertical");
    }
    private void InputJump()
    {
        jumpPressed = Input.GetKey(KeyCode.Space);
    }
    private void InputATK()
    {
        attackPressed = Input.GetMouseButton(0);
    }

}
