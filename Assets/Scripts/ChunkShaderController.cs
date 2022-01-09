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
    int CopyInputKernel;


    // Start is called before the first frame update
    void Start()
    {
        // Compute Shader kernels
        ClearDataKernel = chunkShader.FindKernel("ClearData");
        OneStepKernel = chunkShader.FindKernel("OneStep");
        DrawChunkKernel = chunkShader.FindKernel("DrawChunk");
        SetCellsKernel = chunkShader.FindKernel("SetCells");
        CopyInputKernel = chunkShader.FindKernel("CopyInput");

        chunkShader.SetBool("debug", CellWorld.Instance.debug);
        chunkShader.SetInt("_width", CellWorld.Instance.worldSize.x);
        chunkShader.SetInt("_height", CellWorld.Instance.worldSize.y);
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
        //chunkShader.SetTexture(OneStepKernel, "_mainBuffer", tex);
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
        chunkShader.SetTexture(DrawChunkKernel, "_mainBuffer", tex);
        chunkShader.SetBuffer(DrawChunkKernel, "_chunkBuffer", chunk.buffer);
        chunkShader.SetBuffer(DrawChunkKernel, "_debugBuffer", CellWorld.Instance.debugBuffer);

        chunkShader.DispatchThreads(DrawChunkKernel, chunk.size.x, chunk.size.y, 1);
        
        if(CellWorld.Instance.debug) 
        {
            CellWorld.Instance.debugBuffer.GetData(CellWorld.Instance.debugArray);
            Debug.Log("Debug: DrawChunk");
        }
        //Debug.Log("ChunkShaderController.DrawChunk");
    }

    // function info header
    //********************************************************************
    //  load array of cells from file.
    //********************************************************************
    public void InsertToChunk(CellChunk chunk, ComputeBuffer __inputBuffer, uint[] __inputArray, RenderTexture tex)
    {
        chunkShader.SetInt("_inputWidth", CellWorld.Instance.inputSize.x);
        chunkShader.SetInt("_inputHeight", CellWorld.Instance.inputSize.y);
        chunkShader.SetTexture(DrawChunkKernel, "_mainBuffer", tex);
        chunkShader.SetBuffer(DrawChunkKernel, "_chunkBuffer", chunk.buffer);
        chunkShader.SetBuffer(DrawChunkKernel, "_debugBuffer", CellWorld.Instance.debugBuffer);

        //__inputBuffer.SetData(__inputArray);
        chunkShader.DispatchThreads(SetCellsKernel, 1, 1 , 1);
        //chunkShader.DispatchThreads(SetCellsKernel, chunk.size.x, chunk.size.y, 1);
        //CellWorld.Instance.debugBuffer.GetData(CellWorld.Instance.debugArray);
        //Debug.Log("ChunkShaderController.LoadCellsFromFile");     
    }

    // function info header 
    //********************************************************************
    //  Copy the input buffer to the chunk buffer.
    //********************************************************************
    public void CopyInput(CellChunk chunk, RenderTexture tex)
    {
        chunkShader.SetInt("_inputWidth", CellWorld.Instance.inputSize.x);
        chunkShader.SetInt("_inputHeight", CellWorld.Instance.inputSize.y);
        chunkShader.SetTexture(CopyInputKernel, "_mainBuffer", tex);
        chunkShader.SetBuffer(CopyInputKernel, "_chunkBuffer", chunk.buffer);
        chunkShader.SetBuffer(CopyInputKernel, "_inputBuffer", CellWorld.Instance.inputBuffer);


        chunkShader.DispatchThreads(CopyInputKernel, chunk.size.x, chunk.size.y, 1);
        Debug.Log("ChunkShaderController.CopyInput");
    }
}