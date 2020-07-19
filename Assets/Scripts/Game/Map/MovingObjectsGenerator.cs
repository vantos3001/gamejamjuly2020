using System;
using System.Collections.Generic;
using System.Linq;
using Game.MovingObjects;
using UnityEngine;
using Utils.Pool;
using Random = UnityEngine.Random;

namespace Game.Map {
    public class MovingObjectsGenerator : MonoBehaviour
    {
        private const float DEFAULT_SPEED = 7f;
        private const float RANGE_SPEED = 2f;

        private float _prevRandSpeed = 7f;
        
        [SerializeField] private MovingObject[] _templates;
        private readonly List<MovingObjectSpawnPoint> _spawnPoints = new List<MovingObjectSpawnPoint>();
        public int DebugInstanceId;
        public int LifeDurationInTiles = 100;
        private Transform _playerTarget;
        private Transform _parent;

        private readonly ComplexComponentObjectPool<int, MovingObject> _pool =
            new ComplexComponentObjectPool<int, MovingObject>(Constructor);

        private List<MovingObject> _movingObjects = new List<MovingObject>();
        private List<MovingObject> _movingObjectsCopy = new List<MovingObject>();
        private bool _paused;

        private static MovingObject Constructor(object[] arg) {
            var prefabList = arg[0] as MovingObject[];
            var index = arg[1] as int? ?? 0;
            var parent = arg[2] as Transform;
            var prefab = prefabList.ElementAt(index);
            var newObj = Instantiate(prefab, parent);
            return newObj;
        }

        private void Awake() {
            _parent = new GameObject("moving obects").transform;
            _parent.position = Vector2.zero;
        }

        public void GenerateObject(Vector3 position, MovingObject.Directions direction, float movementSpeed) {
            var index = Random.Range(0, _templates.Length);
            var newObj = _pool.Get(index, true, _templates, index, _parent);
            var newDirection = direction;
            newObj.movementSpeed = movementSpeed;
            newObj.StartMoving(newDirection);
            newObj.transform.transform.position = position;
            newObj.VisualIndex = index;
            MovingObject.SnapPosition(newObj.transform);
            _movingObjects.Add(newObj);
        }

        public MovingObject GetObjectOnTile(Vector2 tile) {
            bool debug = false;
            foreach (var mO in _movingObjects) {
                if (mO.CompareTag("Debug")) {
                    debug = true;
                }
                
                if (mO.GetTiles(debug).Contains(tile)) {
                    //Debug.Log("collision enter");
                    
                    return mO;
                }
            }

            return null;
        }

        private float RandSpeed()
        {
            float speed = 0f;
            var randTry = 0;
            while (randTry < 100)
            {
                var range = Random.Range(-RANGE_SPEED, RANGE_SPEED);

                speed = DEFAULT_SPEED + range;

                if (Math.Abs(speed - _prevRandSpeed) > 0.001)
                {
                    break;
                }
                
                randTry++;
            }

            _prevRandSpeed = speed;
            
            return speed;
        }

        private void Update() {
            if (_paused) {
                return;
            }
            
            foreach (var spawnPoint in _spawnPoints) {
                spawnPoint.TimeSinceLastSpawn += Time.deltaTime;
                if (spawnPoint.TimeSinceLastSpawn > spawnPoint.SpawnInterval) {
                    if (spawnPoint.MovementSpeed <= 0)
                    {
                        spawnPoint.MovementSpeed = RandSpeed();
                    }
                    GenerateObject(spawnPoint.transform.position, spawnPoint.Direction, spawnPoint.MovementSpeed);
                    spawnPoint.TimeSinceLastSpawn = 0;
                }
            }
            
            foreach (var mO in _movingObjects) {
                mO.UpdateMovement(Time.fixedDeltaTime);
                if (mO.TilesPassed() >= LifeDurationInTiles) {
                    _pool.Release(mO, mO.VisualIndex);
                } else {
                    _movingObjectsCopy.Add(mO);
                }
            }
            
            _movingObjects = new List<MovingObject>(_movingObjectsCopy);
            _movingObjectsCopy.Clear();
        }

        public void HandleLevelGenerated(MapLevel newLevel) {
            _spawnPoints.AddRange(newLevel.GetSpawnPoints());
        }

        public void SetPlayerTarget(Transform player) {
            _playerTarget = player;
        }

        public void Resume(MovingObject lastCollisedObj) {
            _paused = false;
        }

        public void Pause(MovingObject lastCollisedObj) {
            _paused = true;
        }
    }
}
