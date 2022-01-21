using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Cell
{
    [SerializeField]
    public Vector2 position;
    [SerializeField]
    public Vector2 velocity;
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
        position = Vector2.zero;
        velocity = Vector2.zero;
        isActive = 1;
        isMoved = 0;
        type = _type;
        value = 0;
    }

    public Cell(Vector2 _position, uint _type)
    {
        position = _position;
        velocity = Vector2.zero;
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
