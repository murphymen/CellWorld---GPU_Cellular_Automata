#ifndef _CellMove_
#define _CellMove_

#include "Assets/Scripts/Shaders/Tools/Buffers.cginc"
//#include "Assets/Scripts/Shaders/Tools/Directions.cginc"
//#include "Assets/Scripts/Shaders/Types/Cell.cginc"



void MoveCell(uint x, uint y, uint dir)
{
    _chunkBuffer[(x+direction[dir].x) + (y+direction[dir].y) * _width].type = _chunkBuffer[x + y * _width].type;
    _chunkBuffer[x + y * _width].type = 0;
}


#endif