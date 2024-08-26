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
            transform.position = new Vector3(3.64f, transform.position.y, transform.position.z);
            Stay(); 
            return;
        }

        if (tmp.x > 2.6f)
        {
            if (Mathf.Abs(tmp.x - transform.position.x) > 0.3f)
            {
                if (tmp.x > transform.position.x)
                {
                    MoveRight();
                }
                else if (tmp.x < transform.position.x)
                {
                    MoveLeft();
                }
            }
            else
            {
                Stay();
            }

            if (Mathf.Abs(tmp.x - transform.position.x) <= 0.3f && Mathf.Abs(tmp.y - transform.position.y) < 1.5f)
            {
                TryReception();
            }
        }
        else
        {
            Stay();  
        }
    }

    private void TryReception()
    {
        int randomValue = UnityEngine.Random.Range(1, 10);
        Debug.Log($"randomvalue: {randomValue}");
        if (randomValue > 5)
        {
            ReceptionAnimation();
            Reception(); 
        }
        else
        {
            StartCoroutine(DisableBallCollider());
        }
    }

    private IEnumerator DisableBallCollider()
    {
        Collider2D ballCollider = ballTracking.GetComponent<Collider2D>();

        if (ballCollider != null)
        {
            ballCollider.enabled = false;
            yield return new WaitForSeconds(0.5f);
            ballCollider.enabled = true;
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
            Vector2 forceDirection = new Vector2(1.0f, 2.0f); 
            ballRigidbody.AddForce(forceDirection * 5.0f, ForceMode2D.Impulse); 
        }
    }
}
