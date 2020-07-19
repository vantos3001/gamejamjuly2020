﻿using System;
using System.Collections.Generic;
using System.Linq;
using Game.MovingObjects;
using UnityEngine;
using Utils.Pool;
using Random = UnityEngine.Random;

namespace Game.Map {
    public class MovingObjectsGenerator : MonoBehaviour {
        [SerializeField] private MovingObject[] _templates;
        private readonly List<MovingObjectSpawnPoint> _spawnPoints = new List<MovingObjectSpawnPoint>();
        private Transform _playerTarget;
        private Transform _parent;
        public int DebugInstanceId;

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

        public void GenerateObject(Vector3 position, MovingObject.Directions direction) {
            var index = Random.Range(0, _templates.Length);
            var newObj = _pool.Get(index, true, _templates, index, _parent);
            var newDirection = direction;
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

        private void Update() {
            if (_paused) {
                return;
            }
            
            foreach (var spawnPoint in _spawnPoints) {
                spawnPoint.TimeSinceLastSpawn += Time.deltaTime;
                if (spawnPoint.TimeSinceLastSpawn > spawnPoint.SpawnInterval) {
                    GenerateObject(spawnPoint.transform.position, spawnPoint.Direction);
                    spawnPoint.TimeSinceLastSpawn = 0;
                }
            }
            
            foreach (var mO in _movingObjects) {
                mO.UpdateMovement(Time.fixedDeltaTime);
                if (mO.TilesPassed() >= 50) {
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
