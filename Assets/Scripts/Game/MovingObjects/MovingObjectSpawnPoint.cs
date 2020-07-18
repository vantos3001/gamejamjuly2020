using Game.MovingObjects;
using UnityEngine;

public class MovingObjectSpawnPoint : MonoBehaviour {
    public float SpawnInterval = 3f;
    public float TimeSinceLastSpawn = 0;
    public MovingObject.Directions Direction;
}
