using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Wave
{
    public string name;
    public string message;
    public float delay;
    public List<WaveAction> waveActions;
}