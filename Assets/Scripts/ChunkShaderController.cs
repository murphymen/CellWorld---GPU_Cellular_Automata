using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;


public class ChunkShaderController : UnitySingleton<ChunkShaderController>
{
    // Shader and kernels.
    public ComputeShader chunkShader;
    int ClearDataKernel;
    int OneStepKernel;
    int DrawChunkKernel;
    int SetCellsKernel;


    // Start is called before the first frame update
    void Start()
    {
        // Compute Shader kernels
        ClearDataKernel = chunkShader.FindKernel("ClearData");
        OneStepKernel = chunkShader.FindKernel("OneStep");
        DrawChunkKernel = chunkShader.FindKernel("DrawChunk");
        SetCellsKernel = chunkShader.FindKernel("SetCells");
    }

    // function info header
    //********************************************************************
    //  Clear the data buffers.
    //********************************************************************
    public void ClearData(CellChunk chunk)
    {
        chunkShader.SetBuffer(ClearDataKernel, "_cellChunkBuffer", chunk.buffer);
        chunkShader.DispatchThreads(ClearDataKernel, chunk.size.x, chunk.size.y, 1);
    }

    // function info header
    //********************************************************************
    //  One step of the simulation.
    //********************************************************************
    public void OneStep(CellChunk chunk, RenderTexture tex)
    {
        chunkShader.SetInt("_width", chunk.size.x);
        chunkShader.SetInt("_height", chunk.size.y);
        chunkShader.SetTexture(OneStepKernel, "_renderTexture", tex);
        chunkShader.SetBuffer(OneStepKernel, "_cellChunkBuffer", chunk.buffer);
        chunkShader.DispatchThreads(OneStepKernel, chunk.size.x, chunk.size.y, 1);

        Debug.Log("ChunkShaderControllerOneStep");
    }

    // function info header
    //********************************************************************
    //  Draw the chunk.
    //********************************************************************
    public void DrawChunk(CellChunk chunk, RenderTexture tex)
    {
        chunkShader.SetInt("_width", chunk.size.x);
        chunkShader.SetInt("_height", chunk.size.y);
        chunkShader.SetTexture(DrawChunkKernel, "_renderTexture", tex);
        chunkShader.SetBuffer(DrawChunkKernel, "_cellChunkBuffer", chunk.buffer);
        chunkShader.DispatchThreads(DrawChunkKernel, chunk.size.x, chunk.size.y, 1);

        Debug.Log("ChunkShaderController.DrawChunk");
    }
}