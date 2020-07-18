using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPlayManager : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private LevelMapGenerator _mapGen;
    private readonly PlayerState _playerState = new PlayerState();

    private void Awake() {
        _playerMovement = GetComponent<PlayerMovement>();
        _mapGen = GetComponent<LevelMapGenerator>();
    }

    private void Update() {
        if (Input.GetMouseButtonUp(0)) {
            if (_playerMovement.MoveTo(_playerState.Distance+1)) {
                _playerState.Distance++;
                _mapGen.TryGenerateNew(_playerState.Distance);
            }
        }
    }
}

public class PlayerState {
    public int Distance;
}
