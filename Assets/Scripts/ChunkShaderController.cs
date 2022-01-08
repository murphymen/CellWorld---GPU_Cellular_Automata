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
        chunkShader.SetBuffer(ClearDataKernel, "_chunkBuffer", chunk.buffer);
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
        chunkShader.SetTexture(OneStepKernel, "_mainBuffer", tex);
        chunkShader.SetBuffer(OneStepKernel, "_chunkBuffer", chunk.buffer);
        //chunkShader.SetBuffer(OneStepKernel, "_inputBuffer", CellWorld.Instance.inputBuffer);

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
        chunkShader.SetTexture(DrawChunkKernel, "_mainBuffer", tex);
        chunkShader.SetBuffer(DrawChunkKernel, "_chunkBuffer", chunk.buffer);
        chunkShader.DispatchThreads(DrawChunkKernel, chunk.size.x, chunk.size.y, 1);

        Debug.Log("ChunkShaderController.DrawChunk");
    }

    // function info header
    //********************************************************************
    //  load array of cells from file.
    //********************************************************************
    public void InsertToChunk(CellChunk chunk, ComputeBuffer __inputBuffer, uint[] __inputArray)
    {
        chunkShader.SetInt("_width", chunk.size.x);
        chunkShader.SetInt("_height", chunk.size.y);
        chunkShader.SetInt("_inputWidth", CellWorld.Instance.inputSize.x);
        chunkShader.SetInt("_inputHeight", CellWorld.Instance.inputSize.y);
        chunkShader.SetBuffer(SetCellsKernel, "_chunkBuffer", chunk.buffer);
        chunkShader.SetBuffer(SetCellsKernel, "_inputBuffer", __inputBuffer);  
        __inputBuffer.SetData(__inputArray);

        chunkShader.DispatchThreads(SetCellsKernel, chunk.size.x, chunk.size.y, 1);
        Debug.Log("ChunkShaderController.LoadCellsFromFile");     
    }
}