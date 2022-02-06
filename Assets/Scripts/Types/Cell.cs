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
    public Color32 colorIndex;



    public Cell(uint _type)
    {
        //color = Vector4.zero;
        position = Vector2.zero;
        velocity = Vector2.zero;
        isActive = 1;
        isMoved = 0;
        type = _type;
        colorIndex = new Color32(0, 0, 0, 0);
    }

    public Cell(Vector2 _position, uint _type)
    {
        //color = Vector4.zero;
        position = _position;
        velocity = Vector2.zero;
        isActive = 1;
        isMoved = 0;
        type = _type;
        colorIndex = new Color32(0, 0, 0, 0);
    }
}