#ifndef _CellCheck_
#define _CellCheck_
#include "Assets/Scripts/Shaders/Tools/Buffers.cginc"

// Constants directions
#define DOWN_LEFT 0
#define DOWN 1
#define DOWN_RIGHT 2

// Check if the cell is empty
bool CheckCell(uint x, uint y, uint dir)
{
    return _chunkBuffer[(x+direction[dir].x) + (y+direction[dir].y) * _width].type > 0;
}


#endif