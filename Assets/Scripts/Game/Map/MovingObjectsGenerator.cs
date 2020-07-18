using System;
using System.Linq;
using Game.MovingObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Map {
    public class MovingObjectsGenerator : MonoBehaviour {
        [SerializeField] private MovingObject[] _templates;

        public void GenerateObject(Vector3 position) {
            var randomIndex = Random.Range(0, _templates.Length);
            var newObj = Instantiate(_templates.ElementAt(randomIndex));
            var newDirection =
                Random.Range(0, 2) == 0 ? MovingObject.Directions.Left : MovingObject.Directions.Right;
            newObj.SetDirection(newDirection);
            newObj.transform.localPosition = position;
        }
    }
}
