using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    public float strength = 1;
    [Range(1,16)]
    public int numLayers = 1;
    public float baseRoughness;
    public float roughness = 2;
    public float persistence = .5f;
    public Vector3 center;
    public float minValue;
}
