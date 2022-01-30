#ifndef _Direction_
#define _Direction_

// Constants directions
#define DOWN_LEFT 0
#define DOWN 1
#define DOWN_RIGHT 2
#define LEFT 3
#define RIGHT 4
#define UP_LEFT 5
#define UP 6
#define UP_RIGHT 7


static const float2 direction[8] =
{
    float2(-1.0f, -1.0f), 
    float2( 0.0f, -1.0f),
    float2( 1.0f, -1.0f),
    float2(-1.0f,  0.0f),
    float2( 1.0f,  0.0f),
    float2(-1.0f,  1.0f),
    float2( 0.0f,  1.0f),
    float2( 1.0f,  1.0f)	
};


#endif