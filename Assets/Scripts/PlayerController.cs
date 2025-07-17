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
    
    private void Awake()
    {
        _visual = transform.GetChild(0); // Getting the Sprite visuals
        _collider = GetComponent<CircleCollider2D>();
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 movement = new Vector3(horizontal, 0, 0);
        MoveTowards(movement);

        if (!_isJumping) return;
        JumpVisuals();
    }

    private void MoveTowards(Vector3 target)
    {
        _body.AddForce(target * movementSpeed);
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
        print("Reset State");
        _isFalling = false;
        _isJumping = false;
        _collider.enabled = true;
    }
    
    public void Jump()
    {
        print("Jump Called");
        _isJumping = true;
        _isFalling = false;
        _collider.enabled = false;
    }
}
