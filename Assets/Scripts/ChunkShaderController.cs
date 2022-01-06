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
    int DrawChunkKernel;


    // Start is called before the first frame update
    void Start()
    {
        // Compute Shader kernels
        //PopulateCellMapKernel = chunkShader.FindKernel("PopulateCellMap");
        ClearDataKernel = chunkShader.FindKernel("ClearData");
        OneStepKernel = chunkShader.FindKernel("OneStep");
        DrawChunkKernel = chunkShader.FindKernel("DrawChunk");
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
    public void OneStep(CellChunk chunk)
    {
        chunkShader.SetInt("_width", chunk.size.x);
        chunkShader.SetInt("_height", chunk.size.y);
        chunkShader.SetTexture(OneStepKernel, "_renderTexture", CellWorld.Instance.renderTexture);
        chunkShader.SetBuffer(OneStepKernel, "_cellChunkBuffer", chunk.buffer);
        chunkShader.DispatchThreads(OneStepKernel, chunk.size.x, chunk.size.y, 1);

        CellWorld.Instance.material.mainTexture = CellWorld.Instance.renderTexture;
    }
}