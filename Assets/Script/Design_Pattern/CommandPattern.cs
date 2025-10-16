using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandPattern : MonoBehaviour
{

    public interface Command
    {
        public void Execute() { }
    }

    private Dictionary<KeyCode, Command> _button;

    private void Awake()
    {
        _button = new Dictionary<KeyCode, Command>();
    }

    private void Start()
    {
        // 버튼 입력과 행동 바인딩
        _button.Add(KeyCode.Space, new JumpCommand());
        _button.Add(KeyCode.A, new AttackCommand());
        _button.Add(KeyCode.LeftControl, new CrouchCommand());
        _button.Add(KeyCode.LeftShift, new RunCommand());
    }

    private void Update()
    {
        HandleInput();
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space)) _button[KeyCode.Space].Execute();
        else if (Input.GetKeyDown(KeyCode.A)) _button[KeyCode.A].Execute();
        else if (Input.GetKeyDown(KeyCode.LeftControl)) _button[KeyCode.LeftControl].Execute();
        else if (Input.GetKeyDown(KeyCode.LeftShift)) _button[KeyCode.LeftShift].Execute();
    }

    public class JumpCommand : Command
    {
        public void Execute()
        {
            Debug.Log("Jump");
        }
    }

    public class AttackCommand : Command
    {
        public void Execute()
        {
            Debug.Log("Attack");
        }
    }

    public class CrouchCommand : Command
    {
        public void Execute()
        {
            Debug.Log("Crouch");
        }
    }

    public class RunCommand : Command
    {
        public void Execute()
        {
            Debug.Log("Run");
        }
    }

}

