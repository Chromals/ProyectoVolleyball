using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackerManager : MonoBehaviour
{
    private int ANIMATION_SPEED;
    private int ANIMATION_FORCE;
    private int ANIMATION_SMASH;

    [Header("Tracking")]
    [SerializeField]
    Transform ballTracking;

    [Header("Movement")]
    [SerializeField]
    float walkSpeed;

    [SerializeField]
    float jumpForce;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    Vector2 groundCheckSize;

    [SerializeField]
    LayerMask groundMask;
    [Header("Smash")]
    [SerializeField]
    private float smashForceX;
    [SerializeField]
    private float smashForceY;

    Rigidbody2D _rigidbody;
    Animator _animator;

    float _velocityY;
    bool _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();

        ANIMATION_SPEED = Animator.StringToHash("speed");
        ANIMATION_FORCE = Animator.StringToHash("force");
        ANIMATION_SMASH = Animator.StringToHash("smash");
    }
    private void Start()
    {
        HandleGrounded();
    }
    private void Update()
    {
        HandleGravity();
    }
    private void FixedUpdate()
    {
        AiController();
    }

    private void AiController()
    {
        Vector3 tmp = ballTracking.position;
        float distanceY = Mathf.Abs(tmp.y - transform.position.y);
        float distanceX = Mathf.Abs(tmp.x - transform.position.x);
        Debug.Log($"Diferencia Vertical: {distanceY} |||| Diferencia Horizontal: {distanceY}");


        if (transform.position.x > 2.6f)
        {
            transform.position = new Vector3(2.6f, transform.position.y, transform.position.z);
            Stay();
            return;
        }

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
                Jump();
                if (!_isGrounded) 
                {
                    TrySmash();
                }
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
            

            _velocityY = jumpForce;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _velocityY);

            _animator.SetTrigger(ANIMATION_FORCE);

            StartCoroutine(WaitForGroundedCoroutine());
        }
    }

    private void TrySmash()
    {
        int randomValue = UnityEngine.Random.Range(1, 10);
        Debug.Log($"randomvalue: {randomValue}");
        if (randomValue > 5)
        {
            SmashAnimation();
            Smash();
        }
        //else
        //{
        //    StartCoroutine(DisableBallColliderTemporarily());
        //}
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

    private void SmashAnimation()
    {
        _animator.SetTrigger(ANIMATION_SMASH);
    }

    private void Smash()
    {
        Rigidbody2D ballRigidbody = ballTracking.GetComponent<Rigidbody2D>();

        if (ballRigidbody != null)
        {
            Vector2 forceDirection = new Vector2(smashForceX, smashForceY);
            ballRigidbody.AddForce(forceDirection, ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && _isGrounded)
        {
            Rigidbody2D ballRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (ballRigidbody != null)
            {
                Vector2 forceDirection = new Vector2(0.5f, 0.5f); 
                ballRigidbody.AddForce(forceDirection, ForceMode2D.Impulse);
            }
        }
    }

    private IEnumerator DisableBallColliderTemporarily()
    {
        Collider2D ballCollider = ballTracking.GetComponent<Collider2D>();
        if (ballCollider != null)
        {
            ballCollider.enabled = false; 
            yield return new WaitForSeconds(0.5f);
            ballCollider.enabled = true; 
        }
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
}
