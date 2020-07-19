using System;
using System.Collections.Generic;
using System.Linq;
using Game.MovingObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Map {
    public class MovingObjectsGenerator : MonoBehaviour {
        [SerializeField] private MovingObject[] _templates;
        
        private readonly List<MovingObjectSpawnPoint> _spawnPoints = new List<MovingObjectSpawnPoint>();
        private Transform _playerTarget;
        private Transform _parent;

        private void Awake() {
            _parent = new GameObject("moving obects").transform;
            _parent.position = Vector2.zero;
        }

        public void GenerateObject(Vector3 position, MovingObject.Directions direction) {
            var randomIndex = Random.Range(0, _templates.Length);
            var newObj = Instantiate(_templates.ElementAt(randomIndex), _parent);
            var newDirection = direction;
            newObj.SetDirection(newDirection);
            newObj.transform.localPosition = position;
        }

        private void Update() {
            foreach (var spawnPoint in _spawnPoints) {
                spawnPoint.TimeSinceLastSpawn += Time.deltaTime;
                if (spawnPoint.TimeSinceLastSpawn > spawnPoint.SpawnInterval) {
                    GenerateObject(spawnPoint.transform.position, spawnPoint.Direction);
                    spawnPoint.TimeSinceLastSpawn = 0;
                }
            }
        }

        public void HandleLevelGenerated(MapLevel newLevel) {
            _spawnPoints.AddRange(newLevel.GetSpawnPoints());
        }

        public void SetPlayerTarget(Transform player) {
            _playerTarget = player;
        }
    }
}
