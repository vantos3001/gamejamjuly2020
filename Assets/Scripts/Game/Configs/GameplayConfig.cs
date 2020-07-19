using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameplayConfig", menuName = "Configs/GameplayConfig", order = 1)]
public class GameplayConfig : ScriptableObject
{
    public float CarvalolFreezeTime;
    public float RestClickTimeDamage;
    public float CoinEveryDistance;
}
