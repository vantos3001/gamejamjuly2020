using System;
using System.Collections.Generic;
using Game.Map;
using UnityEngine;

public class LevelPlayManager : MonoBehaviour {
    public int OperationsQueueLimit = 2; 
    private PlayerMovement _playerMovement;
    private LevelMapGenerator _mapGen;
    private readonly PlayerState _playerState = new PlayerState();

    private readonly Dictionary<Operations, Func<bool>> _operationsByName = new Dictionary<Operations, Func<bool>>();
    private readonly Queue<Operations> _operationsQueue = new Queue<Operations>();
    private MovingObjectsGenerator _movingObjGen;

    private enum Operations {
        Move
    }

    private void Awake() {
        _operationsByName.Add(Operations.Move, Move);
        _playerMovement = GetComponent<PlayerMovement>();
        _mapGen = GetComponent<LevelMapGenerator>();
        _movingObjGen = GetComponent<MovingObjectsGenerator>();
    }

    private bool Move() {
        var success = _playerMovement.MoveTo(_playerState.Distance + 1);
        if (success) {
            _playerState.Distance++;
            _mapGen.TryGenerateNew(_playerState.Distance);
        }
        return success;
    }

    private void Update() {
        if (Input.GetMouseButtonUp(1)) {
            _movingObjGen.GenerateObject(_playerMovement.transform.localPosition);
        }
        
        if (Input.GetMouseButtonUp(0)) {
            EnqueueOperation(Operations.Move);
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

    private bool EnqueueOperation(Operations op) {
        if (_operationsQueue.Count >= OperationsQueueLimit) {
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
