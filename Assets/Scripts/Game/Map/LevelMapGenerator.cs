using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelMapGenerator : MonoBehaviour {
    [SerializeField] private MapLevel[] _levelTemplates;
    public int TilesBuffer = 5;
    
    private readonly List<MapLevel> _levels = new List<MapLevel>();
    private int _distance;

    public MapLevel GenerateNew() {
        var randomIndex = Random.Range(0, _levelTemplates.Length);
        var newLevel = Instantiate(_levelTemplates.ElementAt(randomIndex));
        var lastLevel = _levels.LastOrDefault();
        _levels.Add(newLevel);
        _distance += newLevel.GetSize().x;
        if (lastLevel is null) {
            newLevel.transform.localPosition = Vector3.zero;
            return newLevel;
        }

        var lastLevelSize = lastLevel.GetSize();
        newLevel.transform.localPosition =
            lastLevel.transform.localPosition + new Vector3(lastLevelSize.x * 0.5f, lastLevelSize.x * 0.25f, 0);
        
        return newLevel;
    }

    public bool TryGenerateNew(int distancePassed, out MapLevel newLevel) {
        if (_distance < distancePassed + TilesBuffer) {
            newLevel = GenerateNew();
            return true;
        }

        newLevel = null;
        return false;
    }
}
