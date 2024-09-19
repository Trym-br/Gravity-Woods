using System;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animation _animator;
    private InputActions _inputActions;

    void Start()
    {
        _animator = GetComponent<Animation>();
        _inputActions = GetComponent<InputActions>();
    }

    public void PlayerAnimation()
    {
        if (_inputActions.Jump)
        {
            _animator.Play("Player_Jump");
        }

        if (_inputActions.Horizontal == 0)
        {
            _animator.Play("Player_Idle");
        }
        else
        {
            _animator.Play("Player_Walk");
        }
    }
}
