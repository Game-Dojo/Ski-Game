using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")] 
    [SerializeField] private bool followTarget;
    [SerializeField] private Transform targetFollow;
    [SerializeField] private float targetSpeed = 5.0f;
    [SerializeField] private Vector3 targetOffset = new Vector3(0f, 0f, 0);
    
    [Header("Constrain")] 
    [SerializeField] private bool lockX = false;
    [SerializeField] private bool lockY = false;

    [Header("Limits")]
    [SerializeField] private bool applyLimits = true;
    [SerializeField] private Vector2 limits = new Vector2(-29.0f, 3.0f);
    
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
                targetPosition.x = Mathf.Clamp(targetFollow.position.x, limits.x, limits.y);
                targetPosition.y = 0;
            }
            
            if (lockY)
            {
                targetPosition.x = 0;
                targetPosition.y = Mathf.Clamp(targetFollow.position.y, limits.x, limits.y);
            }
        }
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * targetSpeed);
    }
}
