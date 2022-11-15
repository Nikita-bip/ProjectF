using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform movePoint;
    [SerializeField] private LayerMask whatStopMovement;
    [SerializeField] private SpriteRenderer _characterSprite;

    private Animator _animatorController;
    private MoveState _moveState = MoveState.State;
    private Vector3 _input;
    private Rigidbody2D _rigidbody2D;

    enum MoveState
    {
        State,
        Move,
        Up,
        Down
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animatorController = GetComponent<Animator>();
        movePoint.parent = null;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.5f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f),
                        .2f, whatStopMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

                    if (_input.x != 0)
                    {
                        _characterSprite.flipX = !(_input.x > 0);
                    }
                }
                _animatorController.Play("Movement");
            }

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f),
                        .2f, whatStopMovement))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
        }

        if (_rigidbody2D.velocity == Vector2.zero)
        {
            State();
        }
    }

    private void State()
    {
        _moveState = MoveState.State;
        _animatorController.Play("State");
    }
}
