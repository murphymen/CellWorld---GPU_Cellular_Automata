using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Cell
{
    [SerializeField]
    public uint isActive;
    [SerializeField]
    public uint isMoved;
    [SerializeField]
    public uint type;
    [SerializeField]
    public uint value;

    public Cell(uint _type)
    {
        isActive = 1;
        isMoved = 0;
        type = _type;
        value = 0;
    }

   /*
    public Cell(uint _type, uint _isActive)
    {
        isActive = _isActive;
        isMoved = 0;
        type = _type;
    }*/


}
