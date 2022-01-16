#ifndef _Buffers_
#define _Buffers_

#include "Assets/Scripts/Shaders/Types/Cell.cginc"


uint _width;
uint _height;
uint _inputWidth;
uint _inputHeight;
RWTexture2D<float4> _mainBuffer;
RWStructuredBuffer<Cell> _chunkBuffer;
RWStructuredBuffer<uint> _inputBuffer;
bool _debug;
RWStructuredBuffer<uint> _debugBuffer;
bool _evenIteration;


#endif