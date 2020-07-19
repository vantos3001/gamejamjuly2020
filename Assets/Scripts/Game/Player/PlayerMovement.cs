using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rbody;
    private IsometricCharacterRenderer _isoRenderer;
    private float _timeSinceLastMove;
    public float MoveDuration = 0.45f;
    private Vector2 _targetPosition;
    private Vector2 _prevPosition;
    private Action _onMoveEnd;
    private bool _moving;
    public Vector2 TargetPosition => _targetPosition;

    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        _isoRenderer.SetDirection(new Vector2(1, 0.5f));
        MoveTo(0);
    }

    public bool MoveTo(float position, Action onMoveEnd = null) {
        if (IsMoveCooldown()) {
            return false;
        }

        _moving = true;

        _onMoveEnd = onMoveEnd;
        
        Vector2 inputVector = new Vector2(position * 0.5f, position * 0.25f);
        
        _prevPosition = _rbody.position;
        _targetPosition = inputVector;
        
        _isoRenderer.SetDirection(new Vector2(1, 0.5f));
        
        _timeSinceLastMove = 0;

        return true;
    }

    private bool IsMoveCooldown() {
        return _timeSinceLastMove < MoveDuration;
    }

    void FixedUpdate() {
        _timeSinceLastMove += Time.fixedDeltaTime;
        if (_rbody.position == _targetPosition) {
            if (_moving && _timeSinceLastMove - MoveDuration >= 0.2f) {
                _isoRenderer.SetDirection(Vector2.zero);
                _onMoveEnd?.Invoke();
                _onMoveEnd = null;
                _moving = false;
            }
            return;
        }
        var time = _timeSinceLastMove / MoveDuration;
        var newPosition = Vector2.Lerp(_prevPosition, _targetPosition, time);
        _rbody.MovePosition(newPosition);
    }
}
