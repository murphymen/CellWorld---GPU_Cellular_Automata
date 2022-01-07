using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellChunk
{
    public Vector2Int size;
    public ChunkCoordinate position;
    public ComputeBuffer buffer;


    public CellChunk()
    {
        size = new Vector2Int(0, 0);
        position = new ChunkCoordinate(0, 0, 0);
        buffer = new ComputeBuffer(0, sizeof(uint));
    }

    public CellChunk(Vector2Int _size, ChunkCoordinate chunkCoord)
    {
        size = _size;
        position = chunkCoord;
        buffer = new ComputeBuffer(size.x*size.y, sizeof(uint)*4, ComputeBufferType.Structured);
    }

    public void DisposeChunk()
    {
        buffer?.Dispose();
    }

    public void Initalize(Vector2Int _size, ChunkCoordinate chunkCoord)
    {
        size = _size;
        position = chunkCoord;
        buffer = new ComputeBuffer(size.x*size.y, sizeof(uint)*4, ComputeBufferType.Structured);
    }

    bool IsInChunk(Vector2Int _pos)
    {
        return _pos.x >= 0 && _pos.x < size.x && _pos.y >= 0 && _pos.y < size.y;
    }
}