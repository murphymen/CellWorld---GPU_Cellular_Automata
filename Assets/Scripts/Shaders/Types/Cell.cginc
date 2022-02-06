#ifndef _Cell_
#define _Cell_

struct Cell
{
    //float4 color;
    float2 position;
    float2 velocity;
    uint isActive;
    uint isMoved;
    uint type;
    uint colorIndex;
};


#endif