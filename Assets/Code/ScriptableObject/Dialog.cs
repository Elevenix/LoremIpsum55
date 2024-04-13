using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Dialog")]
public class Dialog : ScriptableObject
{
    public TextClone[] exchanges;
}

[Serializable]
public class TextClone
{
    public int id;
    [TextArea] public string text;
    public float delaySwitch = 2;
}
