using System;
using UnityEngine;
using UnityEngine.Events;

public class BoxTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggered;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        if (_renderer) _renderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        onTriggered?.Invoke();
    }
}
