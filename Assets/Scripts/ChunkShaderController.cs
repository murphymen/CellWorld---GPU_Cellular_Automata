using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;


public class ChunkShaderController : UnitySingleton<ChunkShaderController>
{
    // Shader and kernels.
    public ComputeShader chunkShader;
    //int PopulateCellMapKernel;
    int ClearDataKernel;
    int OneStepKernel;
    int DrawCellsKernel;


    // Start is called before the first frame update
    void Start()
    {
        // Compute Shader kernels
        //PopulateCellMapKernel = chunkShader.FindKernel("PopulateCellMap");
        ClearDataKernel = chunkShader.FindKernel("ClearData");
        OneStepKernel = chunkShader.FindKernel("OneStep");
        DrawCellsKernel = chunkShader.FindKernel("DrawCells");
    }

    public void ClearData(CellChunk chunk)
    {
        chunkShader.SetBuffer(ClearDataKernel, "_cellChunkBuffer", chunk.buffer);
        chunkShader.DispatchThreads(ClearDataKernel, chunk.size.x, chunk.size.y, 1);
    }
}