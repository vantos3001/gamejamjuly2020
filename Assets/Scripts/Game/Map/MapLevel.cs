using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapLevel : MonoBehaviour
{
    private Tilemap[] _tileMaps;
    private MovingObjectSpawnPoint[] _spawnPoints;

    private void Awake() {
        _tileMaps = GetComponentsInChildren<Tilemap>();
        _spawnPoints = GetComponentsInChildren<MovingObjectSpawnPoint>();
    }

    public Vector3Int GetSize() {
        return _tileMaps.FirstOrDefault().size;
    }

    public MovingObjectSpawnPoint[] GetSpawnPoints() {
        return _spawnPoints;
    }
}
