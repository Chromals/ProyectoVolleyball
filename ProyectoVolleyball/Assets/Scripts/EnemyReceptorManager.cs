using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReceptorManager : MonoBehaviour
{
    private int ANIMATION_SPEED;
    private int ANIMATION_RECEPTION;

    [Header("Tracking")]
    [SerializeField]
    Transform ballTracking;

    [Header("Movement")]
    [SerializeField]
    float walkSpeed;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    Vector2 groundCheckSize;

    [SerializeField]
    LayerMask groundMask;

    [SerializeField]
    int dificullty;

    [Header("Reception")]
    [SerializeField]
    float receptionForceX;
    [SerializeField]
    float receptionForceY;
    [SerializeField]
    float force;

    Rigidbody2D _rigidbody;
    Animator _animator;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();


        ANIMATION_SPEED = Animator.StringToHash("speed");
        ANIMATION_RECEPTION = Animator.StringToHash("reception");
    }


    private void FixedUpdate()
    {
        AiController();
    }

    private void AiController()
    {
        Vector3 tmp = ballTracking.position;


        if (transform.position.x < 3.64f)
        {
            transform.position = new Vector3(3.65f, transform.position.y, transform.position.z);
            Stay(); 
            return;
        }

        if (tmp.x > 2.6  && tmp.x < 7.0)
        {
            if (tmp.x > transform.position.x)
            {
                MoveRight();
            }
            else if (tmp.x < transform.position.x)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            TryReception();
        }
    }

    private void TryReception()
    {
        int randomValue = UnityEngine.Random.Range(1, 10);
        Debug.Log($"randomvalue: {randomValue}");
        if (randomValue < dificullty)
        {
            Reception();
            ReceptionAnimation();
        }
        else
        {
            StartCoroutine(IgnoreCollisionWithBallTemporarily());
        }
    }

    private IEnumerator IgnoreCollisionWithBallTemporarily()
    {
        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D ballCollider = ballTracking.GetComponent<Collider2D>();

        if (playerCollider != null && ballCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, ballCollider, true);

            yield return new WaitForSeconds(0.5f);

            Physics2D.IgnoreCollision(playerCollider, ballCollider, false);
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


    private void ReceptionAnimation()
    {
        _animator.SetTrigger(ANIMATION_RECEPTION);
    }

    private void Reception()
    {
        Rigidbody2D ballRigidbody = ballTracking.GetComponent<Rigidbody2D>();

        if (ballRigidbody != null)
        {
            Vector2 forceDirection = new Vector2(receptionForceX, receptionForceY); 
            ballRigidbody.AddForce(forceDirection * force, ForceMode2D.Impulse); 
        }
    }
}
