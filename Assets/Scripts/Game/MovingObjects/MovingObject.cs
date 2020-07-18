using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float movementSpeed = 1f;
    public float Mx =-0.5f;
    public float My = 0.25f;
    MovingObjectRenderer isoRenderer;
    
    public static Vector2 Left = new Vector2(-1f, 0.5f);
    public static Vector2 Right = new Vector2(1f, -0.5f);
    public static Vector2 Forward = new Vector2(1f, 0.5f);
    public static Vector2 Backward = new Vector2(-1f, -0.5f);

    Rigidbody2D rbody;
    private Vector2 _direction;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<MovingObjectRenderer>();
        _direction = Left;
    }

    public void SetDirection(Vector2 value) {
        _direction = value;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 currentPos = rbody.position;
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = _direction;
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);
    }
}
