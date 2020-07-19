using System;
using System.Collections.Generic;
using Game.Map;
using Game.MovingObjects;
using UnityEngine;

public class LevelPlayManager : MonoBehaviour {
    public int OperationsQueueLimit = 2; 
    private PlayerMovement _playerMovement;
    private LevelMapGenerator _mapGen;
    private readonly PlayerState _playerState = new PlayerState();

    private readonly Dictionary<Operations, Func<bool>> _operationsByName = new Dictionary<Operations, Func<bool>>();
    private readonly Queue<Operations> _operationsQueue = new Queue<Operations>();
    private MovingObjectsGenerator _movingObjGen;
    private MovingObject _lastCollisedObj;
    private int _activeOperationsCount;

    private float _currentRestClickTime;

    private enum Operations {
        Move
    }

    private void Awake() {
        _operationsByName.Add(Operations.Move, Move);
        _playerMovement = GetComponent<PlayerMovement>();
        _mapGen = GetComponent<LevelMapGenerator>();
        _movingObjGen = GetComponent<MovingObjectsGenerator>();
        _movingObjGen.SetPlayerTarget(_playerMovement.transform);
        TryGenerateNewLevelPart();
    }

    private bool Move() {
        var nextTile = ForwardTile();
        var collisedObj = _movingObjGen.GetObjectOnTile(nextTile);
        var pathIsClear = collisedObj is null;

        if (!pathIsClear) {
            return false;
        }
        
        var success = _playerMovement.MoveTo(_playerState.Distance + 1, HandleOperationEnd);
        if (success) {
            HandleOperationStart();
            _playerState.Distance++;
            EventManager.OnDistanceChanged(_playerState.Distance);
            CheckRestClickTime();
            _currentRestClickTime = PlayerManager.GetRestClickTime();
            TryGenerateNewLevelPart();
        }
        return success;
    }

    private void UpdateRestClickTime(float delta)
    {
        
        _currentRestClickTime -= delta;
        EventManager.NotifyOnOnRestClickTimeChanged(_currentRestClickTime);
    }

    private void CheckRestClickTime()
    {
        if (0 < _currentRestClickTime)
        {
            PlayerManager.Health.AddHealth(-DataManager.GetGameplayConfig().RestClickTimeDamage);
        }
    }

    private void HandleOperationStart() {
        _activeOperationsCount++;
    }

    private void HandleOperationEnd() {
        _activeOperationsCount--;
        _playerMovement.Rest();
    }

    private Vector2 ForwardTile() {
        MovingObject.GetDirectionByName(MovingObject.Directions.Forward, out var delta);
        var fPos = _playerMovement.TargetPosition + delta;
        var result =MovingObject.GetTile(fPos.x, fPos.y,
            MovingObject.Directions.Forward);

        return result;
    }

    private void TryGenerateNewLevelPart() {
        var levelGenerated = _mapGen.TryGenerateNew(_playerState.Distance, out var newLevel);
        if (levelGenerated) {
            _movingObjGen.HandleLevelGenerated(newLevel);
            TryGenerateNewLevelPart();
        }
    }

    private void Update() {
        CheckPlayerCollision();
        
        if (Input.GetMouseButtonDown(0)) {
            EnqueueOperation(Operations.Move);
        }

        if (!PlayerManager.Health.IsFreeze)
        {
            UpdateRestClickTime(Time.deltaTime);
        }

        if (_operationsQueue.Count == 0) {
            return;
        }

        var opName = _operationsQueue.Peek();
        var operation = GetOperationByName(opName);
        if (operation == null) {
            _operationsQueue.Dequeue();
            return;
        }

        if (operation.Invoke()) {
            _operationsQueue.Dequeue();
        }
    }

    private void CheckPlayerCollision() {
        var debug = _playerMovement.CompareTag("Debug");
        var playerTile = MovingObject.GetTile(_playerMovement.TargetPosition.x, _playerMovement.TargetPosition.y,
            MovingObject.Directions.Forward, debug);
        var collisedObj = _movingObjGen.GetObjectOnTile(playerTile);
        if (collisedObj) {
            if (collisedObj != _lastCollisedObj) {
                Debug.Log("new player collision");
                _lastCollisedObj = collisedObj;
                _movingObjGen.Pause(_lastCollisedObj);
            }
        } else {
            if (_lastCollisedObj is null) {
                return;
            }

            _movingObjGen.Resume(_lastCollisedObj);
        }
    }

    private bool EnqueueOperation(Operations op) {
        if (_operationsQueue.Count + _activeOperationsCount >= OperationsQueueLimit) {
            return false;
        }
        
        _operationsQueue.Enqueue(op);
        return true;
    }

    private Func<bool> GetOperationByName(Operations opName) {
        _operationsByName.TryGetValue(opName, out var result);
        return result;
    }
}

public class PlayerState {
    public int Distance;
}
