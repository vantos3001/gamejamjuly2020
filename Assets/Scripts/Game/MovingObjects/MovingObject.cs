using System.Collections.Generic;
using UnityEngine;

namespace Game.MovingObjects {
    public class MovingObject : MonoBehaviour
    {
        public float movementSpeed = 1f;
        MovingObjectRenderer isoRenderer;

        public enum Directions {
            Left,
            Right,
            Forward,
            Backward
        }
    
        private readonly Dictionary<Directions, Vector2> _directionsByName = new Dictionary<Directions, Vector2>() {
            {Directions.Left, new Vector2(-1f, 0.5f)},
            {Directions.Right, new Vector2(1f, -0.5f)},
            {Directions.Forward, new Vector2(1f, 0.5f)},
            {Directions.Backward, new Vector2(-1f, -0.5f)}
        };

        Rigidbody2D rbody;
        private Vector2 _direction;

        private void Awake()
        {
            rbody = GetComponent<Rigidbody2D>();
            isoRenderer = GetComponentInChildren<MovingObjectRenderer>();
            _directionsByName.TryGetValue(Directions.Left, out _direction);
        }

        public void SetDirection(Directions value) {
            _directionsByName.TryGetValue(value, out _direction);
        }

        void FixedUpdate()
        {
            Vector2 currentPos = rbody.position;
            Vector2 inputVector = _direction;
            inputVector = Vector2.ClampMagnitude(inputVector, 1);
            Vector2 movement = inputVector * movementSpeed;
            Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
            isoRenderer.SetDirection(movement);
            rbody.MovePosition(newPos);
        }
    }
}
