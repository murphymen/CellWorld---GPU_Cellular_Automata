using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;


public class ChunkShaderController : UnitySingleton<ChunkShaderController>
{
    // Shader and kernels.
    public ComputeShader chunkShader;
    public ComputeBuffer colorBuffer;
    int ClearDataKernel;
    int OneStepKernel;
    int DrawChunkKernel;
    int SetCellsKernel;
    int CopyInputKernel;
    int InsertCellsKernel;
    public bool evenIteration;
    

    void CreateColorPallettes()
    {
        colorBuffer = new ComputeBuffer(128, sizeof(uint));

        ColorGenerator colorGenerator = new ColorGenerator();
        colorBuffer.SetData(colorGenerator.GetPalette(1));
    }

    // Start is called before the first frame update
    void Start()
    {
        // Compute Shader kernels
        ClearDataKernel = chunkShader.FindKernel("ClearData");
        OneStepKernel = chunkShader.FindKernel("OneStep");
        DrawChunkKernel = chunkShader.FindKernel("DrawChunk");
        CopyInputKernel = chunkShader.FindKernel("CopyInput");
        InsertCellsKernel = chunkShader.FindKernel("InsertCells");

        chunkShader.SetBool("debug", CellWorld.Instance.debug);
        chunkShader.SetInt("_width", CellWorld.Instance.chunkSize.x);
        chunkShader.SetInt("_height", CellWorld.Instance.chunkSize.y);

        evenIteration = true;

        CreateColorPallettes();
    }

    void OnDestroy()
    {
        colorBuffer.Dispose();
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
    public void OneStep(CellChunk chunk)
    {
        chunkShader.SetBool("_evenIteration", evenIteration);
        chunkShader.SetBuffer(OneStepKernel, "_chunkBuffer", chunk.buffer);
        chunkShader.DispatchThreads(OneStepKernel, chunk.size.x, chunk.size.y, 1);
        evenIteration = !evenIteration;

        //Debug.Log("ChunkShaderControllerOneStep");
    }

    // function info header
    //********************************************************************
    //  Draw the chunk.
    //********************************************************************
    public void DrawChunk(CellChunk chunk, RenderTexture tex)
    {
        chunkShader.SetTexture(DrawChunkKernel, "_mainBuffer", tex);
        chunkShader.SetBuffer(DrawChunkKernel, "_chunkBuffer", chunk.buffer);
        chunkShader.SetBuffer(DrawChunkKernel, "_colorBuffer", colorBuffer);
        chunkShader.DispatchThreads(DrawChunkKernel, chunk.size.x, chunk.size.y, 1);
        
        if(CellWorld.Instance.debug) 
        {
            Debug.Log("Debug: DrawChunk");
        }
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
        //Debug.Log("ChunkShaderController.CopyInput");
    }

    // function info header 
    //********************************************************************
    //  Insert cells from inputCellBuffer to chunk buffer.
    //********************************************************************
    public void InsertCells(CellChunk chunk)
    {
        chunkShader.SetInt("_inputLength", CellWorld.Instance.inputLength);
        chunkShader.SetBuffer(InsertCellsKernel, "_chunkBuffer", chunk.buffer);
        chunkShader.SetBuffer(InsertCellsKernel, "_inputCellsBuffer", CellWorld.Instance.inputCellsBuffer);
        chunkShader.Dispatch(InsertCellsKernel, 1, 1, 1);
        CellWorld.Instance.inputCellsBuffer.Release();
    }

}