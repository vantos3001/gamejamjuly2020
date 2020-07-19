using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game.MovingObjects {
    public class MovingObject : MonoBehaviour
    {
        public float movementSpeed = 1f;
        public int Width;
        public int Height;
        MovingObjectRenderer[] _renderers;
        Rigidbody2D rbody;
        private Vector2 _direction;
        public int VisualIndex = -1;
        public Vector3 StartPosition { get; private set; }
        public static float TileX = 0.5f;
        public static float TileY = 0.25f;
        public bool IsPaused => _playerCollisionPause || _collisionPause;

        public enum Directions {
            Left,
            Right,
            Forward,
            Backward
        }
    
        private static readonly Dictionary<Directions, Vector2> _directionsByName = new Dictionary<Directions, Vector2>() {
            {Directions.Left, new Vector2(-TileX, TileY)},
            {Directions.Right, new Vector2(TileX, -TileY)},
            {Directions.Forward, new Vector2(TileX, TileY)},
            {Directions.Backward, new Vector2(-TileX, -TileY)}
        };

        private Directions _directionKey;
        private bool _playerCollisionPause;
        private bool _collisionPause;

        private void Awake()
        {
            rbody = GetComponent<Rigidbody2D>();
            _renderers = GetComponentsInChildren<MovingObjectRenderer>();
            _directionsByName.TryGetValue(Directions.Left, out _direction);
        }

        public int TilesPassed() {
            var diff = transform.position - StartPosition;
            var tilesDiffX = (int)Mathf.Abs(diff.x / TileX);
            var tilesDiffY = (int)Mathf.Abs(diff.x / TileY);

            switch (_directionKey) {
                case Directions.Left:
                case Directions.Right:
                    return tilesDiffX;
                case Directions.Forward:
                case Directions.Backward:
                    return tilesDiffY;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void StartMoving(Directions value) {
            StartPosition = transform.position;
            SetDirection(value);
        }
        
        public void SetDirection(Directions value) {
            _directionKey = value;
            GetDirectionByName(_directionKey, out _direction);
        }

        public static void GetDirectionByName(Directions value, out Vector2 direction) {
            _directionsByName.TryGetValue(value, out direction);
        }

        public void UpdateMovement(float delta)
        {
            if (IsPaused) {
                return;
            }
            
            Vector3 currentPos = rbody.position;
            Vector3 inputVector = new Vector3(_direction.x, _direction.y, 0);
            //inputVector = Vector2.ClampMagnitude(inputVector, 1);
            Vector3 movement = inputVector * movementSpeed;
            Vector3 newPos = currentPos + movement * delta;
            SetRendererDirections(movement);
            rbody.MovePosition(newPos);
        }

        private void SetRendererDirections(Vector2 movement) {
            foreach (var r in _renderers) {
                r.SetDirection(movement);
            }
        }

        public static void SnapPosition(Transform target) {
            var x = (int) (target.position.x / TileX) * TileX;
            var y = (int) (target.position.y / TileY) * TileY;
            target.position = new Vector3(x, y, 0);
        }
        
        public static Vector3 SnappedPosition(Vector3 target) {
            var x = (int) (target.x / TileX) * TileX;
            var y = (int) (target.y / TileY) * TileY;
            var result = new Vector3(x, y, 0);
            return result;
        }

        public static Vector2 GetTile(float posX, float posY, Directions directionKey, bool debug = false) {
            var x = posX / TileX;
            var y = posY / TileY;
            switch (directionKey) {
                case Directions.Left:
                    x = Mathf.Round(x);
                    y = Mathf.Round(y);
                    break;
                case Directions.Right:
                    x = Mathf.Round(x);
                    y = Mathf.Round(y);
                    break;
                case Directions.Forward:
                    x = Mathf.Round(x);
                    y = Mathf.Round(y);
                    break;
                case Directions.Backward:
                    x = Mathf.Round(x);
                    y = Mathf.Round(y);
                    break;
            }
            if (debug) {
                Debug.Log($"direction:'{directionKey.ToString()}' x: '{(posX / TileX):F1}' -> '{x}'. y:  '{(posY / TileY):F1}' -> '{y}'");
            }
            var result = new Vector3(x, y);
            return result;
        }

        public Vector2[] GetTiles(bool debug = false) {
            return GetTiles(transform.position, Width, Height, _directionKey, debug);
        }
        
        public static Vector2[] GetTiles(Vector3 target, int w, int h, Directions directionKey, bool debug = false) {
            var posX = target.x;
            var posY = target.y;
            Vector2[] tiles = new Vector2[w * h];
            for (int i = 0; i < w; i++) {
                for (int j = 0; j < h; j++) {
                    var tile = GetTile(posX + TileX * i, posY + -TileY * i, directionKey, debug);
                    tiles[i + (i + 1) * j] = tile;
                }
            }

            return tiles;
        }

        public void PlayerCollisionPause() {
            _playerCollisionPause = true;
        }
        
        public void PlayerCollisionResume() {
            _playerCollisionPause = false;
        }
        
        public void CollisionPause() {
            _collisionPause = true;
        }
        
        public void CollisionResume() {
            _collisionPause = false;
        }
    }
}
