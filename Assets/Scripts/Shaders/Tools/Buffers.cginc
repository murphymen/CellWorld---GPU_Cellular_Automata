#ifndef _Buffers_
#define _Buffers_

#include "Assets/Scripts/Shaders/Types/Cell.cginc"


uint _width;
uint _height;
uint _inputWidth;
uint _inputHeight;
uint _inputLength;
RWTexture2D<float4> _mainBuffer;
StructuredBuffer<uint> _colorBuffer;
RWStructuredBuffer<Cell> _chunkBuffer;
RWStructuredBuffer<Cell> _inputCellsBuffer;
RWStructuredBuffer<uint> _inputBuffer;
bool _debug;
bool _evenIteration;


#endif