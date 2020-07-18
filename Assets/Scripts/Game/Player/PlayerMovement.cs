using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rbody;
    private IsometricCharacterRenderer _isoRenderer;
    private float _timeSinceLastMove;
    public float MoveDuration = 0.45f;
    private Vector2 _targetPosition;
    private Vector2 _prevPosition;

    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        _isoRenderer.SetDirection(new Vector2(1, 0.5f));
    }

    public bool MoveTo(float position) {
        if (IsMoveCooldown()) {
            return false;
        }
        
        Vector2 inputVector = new Vector2(position * 0.5f, position * 0.25f);
        
        _prevPosition = _rbody.position;
        _targetPosition = inputVector;
        
        //_rbody.MovePosition(inputVector);
        
        _timeSinceLastMove = 0;

        return true;
    }

    private bool IsMoveCooldown() {
        return _timeSinceLastMove < MoveDuration;
    }

    void FixedUpdate() {
        _timeSinceLastMove += Time.fixedDeltaTime;
        if (_rbody.position == _targetPosition) {
            return;
        }
        var time = _timeSinceLastMove / MoveDuration;
        var newPosition = Vector2.Lerp(_prevPosition, _targetPosition, time);
        _rbody.MovePosition(newPosition);
    }
}
