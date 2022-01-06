using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellChunk
{
    public Vector2Int size;
    public ChunkCoordinate position;

    public ComputeBuffer buffer;


public CellChunk(Vector2Int _size, ChunkCoordinate _position)
{
    position = _position;
    size = _size;
    buffer = new ComputeBuffer(size.x*size.y, sizeof(uint) * 3, ComputeBufferType.Structured);
}

public void DisposeChunk()
{
    buffer.Dispose();
}

/*
    public void Initalize(ChunkCoordinate chunkCoord, World _world)
    {
        chunkCoordinate = chunkCoord;
        world = _world;

        voxelMap = new ComputeBuffer(World.Instance.chunkVolume, 4, ComputeBufferType.Structured);
    }
    */
}