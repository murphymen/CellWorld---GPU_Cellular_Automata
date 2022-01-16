#ifndef _SandBehaviour_
#define _SandBehaviour_

#include "Assets/Scripts/Shaders/Tools/CellCheck.cginc"
#include "Assets/Scripts/Shaders/Tools/Directions.cginc"



void SandBehavior(uint2 id)
{
    uint flatIndex = id.x + (id.y * _width);

    bool check_down_left = CheckCell(id.x, id.y, DOWN_LEFT);
    bool check_down = CheckCell(id.x, id.y, DOWN);
    bool check_down_right = CheckCell(id.x, id.y, DOWN_RIGHT);

    // if below this cell is empty, then move down
    if(!check_down)
    {
        MoveCell(id.x, id.y, DOWN);
    }
    else
    {
        if(!check_down_left && !check_down_right)
        {
            if(_evenIteration)
                MoveCell(id.x, id.y, DOWN_LEFT);
            else
                MoveCell(id.x, id.y, DOWN_RIGHT);
        }
        else if(!check_down_left && check_down_right)
        {
            MoveCell(id.x, id.y, DOWN_LEFT);
        }
        else if(check_down_left && !check_down_right)
        {
            MoveCell(id.x, id.y, DOWN_RIGHT);
        }
    }

    _chunkBuffer[6].type = 666;
}


#endif