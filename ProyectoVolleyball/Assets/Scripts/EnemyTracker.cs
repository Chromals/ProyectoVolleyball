using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    private int ANIMATION_SPEED;
    private int ANIMATION_FORCE;
    private int ANIMATION_FALL;
    private int ANIMATION_SMASH;
    private int ANIMATION_RECEPTION;

    [Header("Tracking")]
    [SerializeField]
    Transform ballTracking;

    [Header("Movement")]
    [SerializeField]
    float walkSpeed;

    [SerializeField]
    float jumpForce;

    [SerializeField]
    float gravityMultiplier;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    Vector2 groundCheckSize;

    [SerializeField]
    LayerMask groundMask;


    Rigidbody2D _rigidbody;
    Animator _animator;

    float _gravityY;
    float _velocityY;

    bool _isGrounded;
    bool _isJumping;
    bool _actionJump;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();

        _gravityY = Physics2D.gravity.y;

        ANIMATION_SPEED = Animator.StringToHash("speed");
        ANIMATION_FORCE = Animator.StringToHash("force");
        ANIMATION_FALL = Animator.StringToHash("fall");
        ANIMATION_SMASH = Animator.StringToHash("smash");
        ANIMATION_RECEPTION = Animator.StringToHash("reception");
    }

    private void Start()
    {
        HandleGrounded();
    }

    private void Update()
    {

        HandleGravity();
        if (Input.GetKeyDown(KeyCode.Space)) 
        { 
            Jump();
        }
    
    }
    private void FixedUpdate()
    {
        AiController();
       
       
    }
    private void AiController()
    {
        Vector3 tmp = ballTracking.position;

        float distanceY = Mathf.Abs(tmp.y - transform.position.y);

        // Imprime la diferencia vertical en la consola
        Debug.Log($"Diferencia Vertical: {distanceY}");

        if (tmp.x > 0 && Mathf.Abs(tmp.x - transform.position.x) > 0.3f)
        {
            if (tmp.x > transform.position.x)
            {
                MoveRight();
            }
            else if (tmp.x < transform.position.x)
            {
                MoveLeft();
            }

            if (Mathf.Abs(tmp.y - transform.position.y) > 1.5f &&
                Mathf.Abs(tmp.y - transform.position.y) < 4f && tmp.x < 1.75f)
            {
                //Debug.Log("Estoy saltando");
                //Debug.Log($"IsGrounded: {_isGrounded}, VelocityY: {_velocityY} ,actionJump :{_actionJump}");
                Jump();
            }
        }
        else if ((tmp.x > 0 && Mathf.Abs(tmp.x - transform.position.x) <= 0.3f) || (tmp.x <= 0 && tmp.x >= -0.6f))
        {
            Stay();
        }
        else if (tmp.x < -0.6f)
        {
            if (transform.position.x < 3.75f)
            {
                MoveRight();
            }
            else if (transform.position.x > 4.1f)
            {
                MoveLeft();
            }
            else
            {
                Stay();
            }
        }
        else
        {
            Stay();
        }
    }
    private void HandleGravity()
    {
        if (_isGrounded)
        {
            if (_velocityY < -1.0F)
            {
                _velocityY = -1.0F;
            }
        }
    }

    private void HandleGrounded()
    {
        _isGrounded = IsGrounded();
        if (!_isGrounded)
        {
            StartCoroutine(WaitForGroundedCoroutine());
        }
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _isGrounded = false;
            _isJumping = true;

            _velocityY = jumpForce;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _velocityY);

            _animator.SetTrigger(ANIMATION_FORCE);

            StartCoroutine(WaitForGroundedCoroutine());
        }
    }
    private void MoveLeft()
    {
        float speed = 1.0F;
        _animator.SetFloat(ANIMATION_SPEED, speed);

        Vector2 velocity = new Vector2(-1.0F, 0.0F) * walkSpeed * Time.fixedDeltaTime;
        velocity.y = _rigidbody.velocity.y; 

        _rigidbody.velocity = velocity;
    }

    private void MoveRight()
    {
        float speed = 1.0F;
        _animator.SetFloat(ANIMATION_SPEED, speed);

        Vector2 velocity = new Vector2(1.0F, 0.0F) * walkSpeed * Time.fixedDeltaTime;
        velocity.y = _rigidbody.velocity.y; 

        _rigidbody.velocity = velocity;
    }

    private void Stay()
    {
        _rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y);

        _animator.SetFloat(ANIMATION_SPEED, 0.0f);
    }
  

    private bool IsGrounded()
    {
        Collider2D collider2D = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0.0F, groundMask);
        return collider2D != null;
    }

    private IEnumerator WaitForGroundedCoroutine()
    {
        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(() => IsGrounded());
        _isGrounded = true;
    }
    private void SmashAnimation ()
    {
        _animator.SetTrigger(ANIMATION_SMASH);
    }
    private void Smash()
    {
       
    }
    private void ReceptionAnimation()
    {
        _animator.SetTrigger(ANIMATION_RECEPTION);
    }
    private void Reception ()
    {

    }
}
