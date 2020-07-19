using System.Collections.Generic;
using UnityEngine;

namespace Game.MovingObjects {
    public class MovingObject : MonoBehaviour
    {
        public float movementSpeed = 1f;
        public int Width;
        public int Height;
        MovingObjectRenderer[] _renderers;

        public enum Directions {
            Left,
            Right,
            Forward,
            Backward
        }
    
        private static readonly Dictionary<Directions, Vector2> _directionsByName = new Dictionary<Directions, Vector2>() {
            {Directions.Left, new Vector2(-0.5f, 0.25f)},
            {Directions.Right, new Vector2(0.5f, -0.25f)},
            {Directions.Forward, new Vector2(0.5f, 0.25f)},
            {Directions.Backward, new Vector2(-0.5f, -0.25f)}
        };

        Rigidbody2D rbody;
        private Vector2 _direction;

        private void Awake()
        {
            rbody = GetComponent<Rigidbody2D>();
            _renderers = GetComponentsInChildren<MovingObjectRenderer>();
            _directionsByName.TryGetValue(Directions.Left, out _direction);
        }

        public void SetDirection(Directions value) {
            GetDirectionByName(value, out _direction);
        }

        public static void GetDirectionByName(Directions value, out Vector2 direction) {
            _directionsByName.TryGetValue(value, out direction);
        }

        void FixedUpdate()
        {
            Vector3 currentPos = rbody.position;
            Vector3 inputVector = new Vector3(_direction.x, _direction.y, 0);
            //inputVector = Vector2.ClampMagnitude(inputVector, 1);
            Vector3 movement = inputVector * movementSpeed;
            Vector3 newPos = currentPos + movement * Time.fixedDeltaTime;
            SetRendererDirections(movement);
            rbody.MovePosition(newPos);
        }

        private void SetRendererDirections(Vector2 movement) {
            foreach (var r in _renderers) {
                r.SetDirection(movement);
            }
        }

        public static void SnapPosition(Transform target) {
            var x = (int) (target.position.x / 0.5f) * 0.5f;
            var y = (int) (target.position.y / 0.25f) * 0.25f;
            target.position = new Vector3(x, y, 0);
        }
    }
}
