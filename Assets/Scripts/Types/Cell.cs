using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Cell
{
    [SerializeField]
    public bool isActive;
    [SerializeField]
    public bool isMoved;
    [SerializeField]
    public uint type;
    [SerializeField]
    public uint value;
}
