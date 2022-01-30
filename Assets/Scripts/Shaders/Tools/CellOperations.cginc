#ifndef _CellOperations_
#define _CellOperations_

#include "Assets/Scripts/Shaders/Tools/Buffers.cginc"


bool ChunkBoundaryCheck(uint2 pos)
{
    return pos.x>=0 && pos.x<_width && pos.y>=0 && pos.y<_height;
}

// Check if the cell is empty
bool CheckCell(uint x, uint y, uint dir)
{
    return _chunkBuffer[(x+direction[dir].x) + (y+direction[dir].y) * _width].type > 0;
}

void MoveCell(uint x, uint y, uint dir)
{
    _chunkBuffer[(x+direction[dir].x) + (y+direction[dir].y) * _width].type = _chunkBuffer[x + y * _width].type;
    _chunkBuffer[x + y * _width].type = 0;
}

void SetCell(uint2 pos, uint type)
{
    if(ChunkBoundaryCheck(pos))
    {
        _chunkBuffer[pos.x + pos.y * _width].type = type;
    }
}


#endif