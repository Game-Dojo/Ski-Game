using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")] 
    [SerializeField] private bool followTarget;
    [SerializeField] private Transform targetFollow;
    [SerializeField] private float targetSpeed = 5.0f;
    [SerializeField] private Vector3 targetOffset;
    
    [Header("Constrain")] 
    [SerializeField] private bool lockX = false;
    [SerializeField] private bool lockY = false;

    [Header("Limits")]
    [SerializeField] private bool applyLimits = true;
    [SerializeField] private Vector2 limits = new Vector2(-29.0f, 3.0f);

    private Camera _mainCamera;
    
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (!followTarget || !targetFollow) return;
        
        Vector3 targetPosition = transform.position;
        
        if (!lockX || !lockY)
        {
            targetPosition = targetFollow.position;
            targetPosition.z = transform.position.z;
        }
        
        targetPosition += targetOffset;
        
        if (applyLimits)
        {
            if (lockX)
            {
                targetPosition.x = Mathf.Clamp(targetPosition.x, limits.x, limits.y);
                targetPosition.y = 0;
            }
            
            if (lockY)
            {
                targetPosition.x = 0;
                targetPosition.y = Mathf.Clamp(targetPosition.y, limits.x, limits.y);
            }
        }
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * targetSpeed);

        DrawRectangle();
    }

    private void DrawRectangle()
    {
        float width = 10.0f;
        float height = 5.5f;
        
        //Top Line
        Debug.DrawLine(new Vector3(transform.position.x - width, transform.position.y + height + targetOffset.y, 0),
            new Vector3(transform.position.x + width, transform.position.y + height+ targetOffset.y, 0), Color.black);
        //Right Line
        Debug.DrawLine(new Vector3(transform.position.x + width, transform.position.y + height+ targetOffset.y, 0),
            new Vector3(transform.position.x + width, transform.position.y - height+ targetOffset.y, 0), Color.black);
        //Bottom Line
        Debug.DrawLine(new Vector3(transform.position.x + width, transform.position.y - height+ targetOffset.y, 0),
            new Vector3(transform.position.x - width, transform.position.y - height+ targetOffset.y, 0), Color.black);
        //Left Line
        Debug.DrawLine(new Vector3(transform.position.x - width, transform.position.y - height+ targetOffset.y, 0),
            new Vector3(transform.position.x - width, transform.position.y + height+ targetOffset.y, 0), Color.black);
    }
}
