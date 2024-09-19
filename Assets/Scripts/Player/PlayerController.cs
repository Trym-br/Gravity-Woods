using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 7f;
 
    public bool playerIsGrounded;
    public LayerMask whatIsGround;
    public Vector2 groundBoxSize = new Vector2(0.8f,0.2f);
    public float sprintMultiplier = 2f;
    
    private InputActions _input;
    private Rigidbody2D _rigidbody2D;

    private bool isJumping;
    private float jumpTimer;
    public float jumpTime;

    private void Start()
    {
        _input = GetComponent<InputActions>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        playerIsGrounded = Physics2D.OverlapBox(transform.position + new Vector3(0f,0.5f,0f) * Mathf.Sign(_rigidbody2D.gravityScale) * Mathf.Sign(Physics2D.gravity.y), groundBoxSize, 0f, whatIsGround);

        if (_input.Jump && playerIsGrounded)
        {
            _rigidbody2D.linearVelocityY = jumpSpeed * Mathf.Sign(Physics2D.gravity.y) *-1;
        }
        else if (isJumping && _input.Jump)
        {
            if (jumpTimer > 0)
            {
                _rigidbody2D.linearVelocityY = jumpSpeed * Mathf.Sign(Physics2D.gravity.y) *-1;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + new Vector3(0f,0.5f,0f) * Mathf.Sign(_rigidbody2D.gravityScale) * Mathf.Sign(Physics2D.gravity.y), groundBoxSize);
    }

    private void FixedUpdate()
    {
        if (_input.Sprint)
        {
            _rigidbody2D.linearVelocityX = _input.Horizontal * moveSpeed * sprintMultiplier;
        }
        else
        {
            _rigidbody2D.linearVelocityX = _input.Horizontal * moveSpeed;
        }
    }
}