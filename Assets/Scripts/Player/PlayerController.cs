using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 4f;
 
    public bool playerIsGrounded;
    public LayerMask whatIsGround;
    public Vector2 groundBoxSize = new Vector2(0.8f,0.2f);
    public float sprintMultiplier = 2f;
    
    private InputActions _input;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    
    [Header("Text")]
    public TMP_Text flowersText;

    [SerializeField]
    private bool isJumping;
    
    [SerializeField]
    private float jumpTimer;
    public float jumpTime = 0.5f;
    
    public bool isUpsideDown;
    
    [Header("Audio")]
    public AudioClip jumpSound;
    public AudioClip flowerPickupSound;
    
    [Header("Collectables")]
    private int flowersCollected = 0;


    private void Start()
    {
        _input = GetComponent<InputActions>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        playerIsGrounded = Physics2D.OverlapBox(transform.position + new Vector3(0f,0.5f,0f) * Mathf.Sign(_rigidbody2D.gravityScale) * Mathf.Sign(Physics2D.gravity.y), groundBoxSize, 0f, whatIsGround);

        if (_input.Jump && playerIsGrounded)
        {
            isJumping = true;
            jumpTimer = jumpTime;
            _rigidbody2D.linearVelocityY = jumpSpeed * Mathf.Sign(Physics2D.gravity.y) *-1;
            _audioSource.PlayOneShot(jumpSound); // Playing Jump Sound
        }
        if (isJumping && _input.Jump)
        {
            if (jumpTimer > 0)
            {
                _rigidbody2D.linearVelocityY = jumpSpeed * Mathf.Sign(Physics2D.gravity.y) *-1;
                jumpTimer -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (!_input.Jump)
        {
            isJumping = false;
        }
        
        flowersText.text = flowersCollected.ToString();
        
        UpdateAnimation();
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

    private void UpdateAnimation()
    {
        // Animations
        if (playerIsGrounded)
        {
            if (_input.Horizontal != 0)
            {
                _animator.Play("Player_Walk");
            }
            else
            {
                _animator.Play("Player_Idle");
            }
        }
        else
        {
            if (_rigidbody2D.linearVelocityY > 0)
            {
                _animator.Play("Player_Jump");
            }
        }
        
        // Sprite facing
        if (_input.Horizontal != 0)
        {
            if (_input.Horizontal < 0) // Fix when upside down
            {
                _spriteRenderer.flipX = true;
            }
            else
            {
                _spriteRenderer.flipX = false;
            }
        }

        if (Physics2D.gravity.y > 0)
        {
            _spriteRenderer.flipY = true;
        }
        else
        {
            _spriteRenderer.flipY = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Flower"))
        {
            flowersCollected++;
            _audioSource.PlayOneShot(flowerPickupSound);
            Destroy(other.gameObject);
        }
    }
}