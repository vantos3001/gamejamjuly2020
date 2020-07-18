using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityConfig", menuName = "Configs/AbilityConfig", order = 1)]
public class AbilityConfig : ScriptableObject
{
    public List<AbilityData> AbilityDatas;
}

