using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellChunk
{
    public Vector2Int size;
    public ChunkCoordinate position;
    public ComputeBuffer buffer;
    public List<Cell> cells;


    // function info header
    // ***********************************************************************
    // function:    LoadFromFile
    // description: Loads the chunk from a file
    // parameters:  string fileName - the name of the file to load
    // ***********************************************************************
    public void LoadFromFile(string fileName)
    {
        string[] lines = System.IO.File.ReadAllLines(fileName);
        Initalize(new Vector2Int(lines[0].Length, lines.Length), new ChunkCoordinate(0, 0, 0));

        for (int j = 0; j < size.y; j++)
        {
            for (int i = 0; i < size.x; i++)
            {
                int index = i + j * size.x;
                if (lines[i][j] == '.')
                    cells.Add(new Cell(0, true));
                else if (lines[i][j] == '#')
                    cells.Add(new Cell(1, true));
            }
        }
    }

    // ***********************************************************************
    // function:    InsetrtCell
    // description: Inserts a cell into the chunk
    // parameters:  Vector2Int _pos, uint _type
    // ***********************************************************************
    public void InsertCell(Vector2Int _pos, uint _type)
    {
        if (!IsInChunk(_pos)) return;

        int index = _pos.x + _pos.y * size.x;
        Cell cell = new Cell(_type, true);
        if(index<cells.Count)
            cells[index] = cell;
        else
        Debug.Log("Error: index out of range");
    }

    public void DisposeChunk()
    {
        buffer?.Dispose();
    }

    public void Initalize(Vector2Int _size, ChunkCoordinate chunkCoord)
    {
        size = _size;
        position = chunkCoord;

        cells = new List<Cell>();
        buffer = new ComputeBuffer(size.x*size.y, sizeof(uint)*4, ComputeBufferType.Structured);
    }

    bool IsInChunk(Vector2Int _pos)
    {
        return _pos.x >= 0 && _pos.x < size.x && _pos.y >= 0 && _pos.y < size.y;
    }
}