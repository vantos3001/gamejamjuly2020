using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rbody;
    private IsometricCharacterRenderer _isoRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        _isoRenderer.SetDirection(Vector2.one);
    }

    void FixedUpdate()
    {
        Vector2 currentPos = _rbody.position;
        Vector2 inputVector = new Vector2(1, 0.5f);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * 1;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        _isoRenderer.SetDirection(movement);
        _rbody.MovePosition(newPos);
    }
    
    // Update is called once per frame
    /*void FixedUpdate()
    {
        Vector2 currentPos = _rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * 1;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        _isoRenderer.SetDirection(movement);
        _rbody.MovePosition(newPos);
    }*/
}
