using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController2D : MonoBehaviour
{


    private int ANIMATION_SPEED;
    private int ANIMATION_FORCE;
    private int ANIMATION_FALL;
    private int ANIMATION_PUNCH;
    private int ANIMATION_UPPERCUT;
    private int ANIMATION_DIE;

    [Header("Switch Characters")]

    [SerializeField]
    public GameObject player1;

    [SerializeField]
    public GameObject ally;

    [Header("Movement")]
    [SerializeField]
    float walkSpeed;

    [SerializeField]
    float jumpForce;

    [SerializeField]
    float gravityMultiplier;

    //[SerializeField] Transform groundCheck;

    [SerializeField]
    Vector2 groundCheckSize;

    [SerializeField]
    LayerMask groundMask;

    [SerializeField]
    bool isFacingRight;

    [Header("Attacks")]
    [SerializeField]
    Transform punchPoint;

    [SerializeField]
    float punchRadius;

    [SerializeField]
    LayerMask attackMask;

    [SerializeField]
    float _dieAnimationTime;

    GameObject _activePlayer;
    Rigidbody2D _rigidbody;
    Animator _animator;
    Transform _groundCheck;

    float _inputX;
    float _gravityY;
    float _velocityY;

    bool _isGrounded;
    bool _isJumpPressed;
    bool _isJumping;

    private void Awake()
    {
        _activePlayer = player1;

        _rigidbody = _activePlayer.GetComponent<Rigidbody2D>();
        _animator = _activePlayer.GetComponentInChildren<Animator>();

        _gravityY = Physics2D.gravity.y;
        ANIMATION_SPEED = Animator.StringToHash("speed");
        ANIMATION_FORCE = Animator.StringToHash("force");
        ANIMATION_FALL = Animator.StringToHash("fall");
        ANIMATION_PUNCH = Animator.StringToHash("punch");
        ANIMATION_DIE = Animator.StringToHash("die");
        ANIMATION_UPPERCUT = Animator.StringToHash("uppercut");

        AssignControl(_activePlayer);
    }

    private void Start()
    {
        HandleGrounded();
    }

    private void Update()
    {
        HandleGravity();
        HandleInputMove();
        HandleCharacterSwitch();
    }



    private void FixedUpdate()
    {
        HandleJump();
        HandleRotate();
        HandleMove();
    }

    private void AssignControl(GameObject player)
    {
        if (_activePlayer != null)
        {
            var previousRigidbody = _activePlayer.GetComponent<Rigidbody2D>();
            previousRigidbody.velocity = new Vector2(0.0f, -1.0F);
            previousRigidbody.angularVelocity = 0.0f; // Detener la rotación

            previousRigidbody.simulated = true;
            previousRigidbody.isKinematic = false;
        }

        _activePlayer = player;
        _rigidbody = _activePlayer.GetComponent<Rigidbody2D>();
        _animator = _activePlayer.GetComponentInChildren<Animator>();

        player1.GetComponent<Rigidbody2D>().isKinematic = _activePlayer != player1;
        ally.GetComponent<Rigidbody2D>().isKinematic = _activePlayer != ally;

        _rigidbody.simulated = true;
        // Asignar el GroundCheck del nuevo personaje
        _groundCheck = _activePlayer.transform.Find("GroundCheck");

        // Restablecer el input y la velocidad vertical
        _inputX = 0.0f;
        _velocityY = 0.0f;

        // Verificar y corregir la posición del personaje si está por debajo del suelo
        if (_groundCheck != null && !IsGrounded())
        {
            _rigidbody.position = new Vector2(_rigidbody.position.x, 0.0F); // Ajusta el valor Y según sea necesario
        }
    }

    private void HandleCharacterSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_activePlayer == player1)
            {
                player1.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, player1.GetComponent<Rigidbody2D>().velocity.y);
                AssignControl(ally);
            }
            else
            {
                ally.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, ally.GetComponent<Rigidbody2D>().velocity.y);
                AssignControl(player1);
            }
        }
    }


    private void HandleJump()
    {
        if (_isJumpPressed)
        {
            _isJumpPressed = false;
            _isGrounded = false;
            _isJumping = true;

            _velocityY = jumpForce;

            _animator.SetTrigger(ANIMATION_FORCE);
            StartCoroutine(WaitForGroundedCoroutine());

        }
        else if (!_isGrounded)
        {
            _velocityY += _gravityY * gravityMultiplier * Time.fixedDeltaTime;
            if (!_isJumping)
                _animator.SetTrigger(ANIMATION_FALL);
        }
        else if (_isGrounded)
        {
            if (_velocityY >= 0.0F)
                _velocityY = -1.0F;
            else
                HandleGrounded();

            _isJumping = false;

        }

    }

    private void HandleGrounded()
    {
        _isGrounded = IsGrounded();
        if (!_isGrounded)
            StartCoroutine(WaitForGroundedCoroutine());
    }

    private bool IsGrounded()
    {
        Collider2D collider2D =
            Physics2D.OverlapBox(_groundCheck.position, groundCheckSize, 0.0F, groundMask);
        return collider2D != null;
    }

    private IEnumerator WaitForGroundedCoroutine()
    {
        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(() => IsGrounded());
        _isGrounded = true;
    }

    private void HandleGravity()
    {
        //print("velocity: " + _velocityY + "   //// IsGrounded: " + _isGrounded + "//// isJumping: " + _isJumping);
        if (_isGrounded)
        {
            if (_velocityY < -1.0F)
                _velocityY = -1.0F;

            HandleImputJump();
        }
    }

    private void HandleImputJump()
    {
        _isJumpPressed = Input.GetButton("Jump");
    }

    public void HandleMove()
    {
        float speed = _inputX != 0.0F ? 1.0F : 0.0F;
        float animatorSpeed = _animator.GetFloat(ANIMATION_SPEED);

        if (speed != animatorSpeed)
            _animator.SetFloat(ANIMATION_SPEED, speed);

        Vector2 velocity = new Vector2(_inputX, 0.0F) * walkSpeed * Time.fixedDeltaTime;
        velocity.y = _velocityY;
        _rigidbody.velocity = velocity;
    }

    public void HandleInputMove()
    {
        _inputX = Input.GetAxisRaw("Horizontal");
    }

    public void HandleRotate()
    {
        if (_inputX == 0.0F)
            return;
        bool facingRight = _inputX > 0.0F;
        if (isFacingRight != facingRight)
        {
            isFacingRight = facingRight;
            _activePlayer.transform.Rotate(0.0F, 180.0F, 0.0F);
        }
    }

    

   

    public void Punch(float damage, bool isPercentage)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(punchPoint.position, punchRadius, attackMask);
        foreach (Collider2D collider in colliders)
        {
            //DamageableController controller = collider.GetComponent<DamageableController>();

            //if (controller == null)
            //    continue;

            //controller.TakeDamage(damage, isPercentage);
        }

    }

    public void Punch()
    {
        _animator.SetTrigger(ANIMATION_PUNCH);


    }

    public void Die()
    {
        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        _animator.SetTrigger(ANIMATION_DIE);
        yield return new WaitForSeconds(_dieAnimationTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}


