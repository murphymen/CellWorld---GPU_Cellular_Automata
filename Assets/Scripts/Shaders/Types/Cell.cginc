#ifndef _Cell_
#define _Cell_

struct Cell
{
    float2 position;
    float2 velocity;
    uint isActive;
    uint isMoved;
    uint type;
    uint value;
};


#endif