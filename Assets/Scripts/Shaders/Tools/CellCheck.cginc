#ifndef _CellCheck_
#define _CellCheck_
#include "Assets/Scripts/Shaders/Tools/Buffers.cginc"



// Check if the cell is empty
bool CheckCell(uint x, uint y, uint dir)
{
    return _chunkBuffer[(x+direction[dir].x) + (y+direction[dir].y) * _width].type > 0;
}


#endif