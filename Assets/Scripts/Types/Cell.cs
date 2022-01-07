using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cell
{
    [SerializeField]
    public bool isActive;
    [SerializeField]
    public bool isMoved;
    [SerializeField]
    public uint type;
    [SerializeField]
    public uint value;

    public Cell(uint _type, bool _isActive)
    {
        isActive = _isActive;
        isMoved = false;
        type = _type;
    }
}
