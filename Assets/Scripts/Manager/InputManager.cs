using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private float keyHorizontal;
    [SerializeField] private float keyVertical;
    [SerializeField] private bool jumpPressed;
    [SerializeField] private bool attackPressed;
    [SerializeField] private bool skillPressed;
    [SerializeField] private bool dashPressed;
    [SerializeField] private bool pausePressed;

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
    public bool SkillPressed
    {
        get
        {
            return skillPressed;
        }
    }
    public bool DashPressed
    {
        get
        {
            return dashPressed;
        }
    }
    public bool PausePressed
    {
        get
        {
            return pausePressed;
        }
    }

    void Update()
    {
        this.InputHorizontalAndVertical();
        this.InputJump();
        this.InputATK();
        this.InputSkill();
        this.InputDash();
        this.InputPause();
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
    private void InputSkill()
    {
        skillPressed = Input.GetMouseButton(1);
    }
    private void InputDash()
    {
        dashPressed = Input.GetKey(KeyCode.M);
    }
    private void InputPause()
    {
        pausePressed = Input.GetKeyDown(KeyCode.Escape);
    }

}
