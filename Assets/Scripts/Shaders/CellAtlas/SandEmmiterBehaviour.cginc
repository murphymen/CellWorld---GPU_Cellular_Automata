#ifndef _SandEmmiterBehaviour_
#define _SandEmmiterBehaviour_

#include "Assets/Scripts/Shaders/Tools/CellOperations.cginc"


void SandEmmiterBehaviour(uint2 id)
{
    // if below this cell is empty, then make below type 1
    if(!CheckCell(id.x, id.y-1, 1))
    {
        _chunkBuffer[(id.x) + (id.y-1) * _width].type = 1;
    }
}


#endif