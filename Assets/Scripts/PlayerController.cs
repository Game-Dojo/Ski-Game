using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float jumpAnimationSpeed = 3f;

    private Transform _visual;
    private Rigidbody2D _body;
    private CircleCollider2D _collider;
    
    private bool _isJumping = false;
    private bool _isFalling = false;

    private GameManager _manager;
    private Rigidbody2D _rigid;

    private Vector3 _lastVelocity;
    private float _lastGravity;
    private bool _isPaused = false;
    
    private void Awake()
    {
        _visual = transform.GetChild(0); // Getting the Sprite visuals
        _collider = GetComponent<CircleCollider2D>();
        _body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _manager = FindAnyObjectByType<GameManager>();
        _manager.OnGamePause += OnGamePaused;
        
        _lastVelocity = _rigid.linearVelocity;
        _lastGravity = _rigid.gravityScale;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float jumpAirControl = (_isJumping) ? 0.4f : 1;
        Vector3 movement = new Vector3(horizontal, 0, 0) * jumpAirControl;
        MoveTowards(movement);

        if (!_isJumping) return;
        JumpVisuals();
    }

    private void MoveTowards(Vector3 target)
    {
        _body.AddForce(target * movementSpeed);
    }

    private void OnGamePaused()
    {
        _isPaused = !_isPaused;
        
        if (_isPaused)
        {
            _lastVelocity = _rigid.linearVelocity;
            _lastGravity = _rigid.gravityScale;
            
            _rigid.linearVelocity = Vector3.zero;
            _rigid.gravityScale = 0;
        }
        else
        {
            _rigid.linearVelocity = _lastVelocity;
            _rigid.gravityScale = _lastGravity;
        }
    }
    
    private void JumpVisuals()
    {
        Vector3 visualPosition = _visual.localPosition;
        
        if (!_isFalling && visualPosition.y > 0.95f)
        {
            _isFalling = true;
        }
        
        if (_isFalling)
        {
            visualPosition.y -= (jumpAnimationSpeed - 0.2f) * Time.deltaTime;
            if (visualPosition.y <= 0) ResetState();
        }else
        {
            visualPosition.y += jumpAnimationSpeed * Time.deltaTime;
        }
        
        visualPosition.y = Mathf.Clamp(visualPosition.y, 0, 1.0f);
        
        _visual.localPosition = visualPosition;
    }

    private void ResetState()
    {
        //print("Reset State");
        _isFalling = false;
        _isJumping = false;
        _collider.enabled = true;
    }
    
    public void Jump()
    {
        //print("Jump Called");
        _isJumping = true;
        _isFalling = false;
        _collider.enabled = false;
    }

    private void OnDestroy()
    {
        _manager.OnGamePause -= OnGamePaused;
    }
}
