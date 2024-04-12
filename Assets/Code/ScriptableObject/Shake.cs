using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Shaker")]
public class Shake : ScriptableObject
{
    public float delay = .1f;
    public float magnitude = .1f;
    public int amount = 1;
}
