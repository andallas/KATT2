using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class WaveAction
{
    public string name;
    public string message;
    public float delay;
    public int numToSpawn;
    public GameObject enemyPrefab;
}