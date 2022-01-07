using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellChunk
{
    public Vector2Int size;
    public ChunkCoordinate position;
    public ComputeBuffer buffer;
    public List<Cell> cells;



    public CellChunk(Vector2Int _size, ChunkCoordinate _position)
    {
        position = _position;
        size = _size;
        buffer = new ComputeBuffer(size.x*size.y, sizeof(uint) * 3, ComputeBufferType.Structured);
    }

// function info header
// ***********************************************************************
// function:    LoadFromFile
// description: Loads the chunk from a file
// parameters:  string fileName - the name of the file to load
// ***********************************************************************
public void LoadFromFile(string fileName)
{
    //
    string[] lines = System.IO.File.ReadAllLines(fileName);
    cells = new List<Cell>();
    
    
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