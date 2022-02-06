using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellWorld : UnitySingleton<CellWorld>
{
    // World data
    public Vector2Int worldSize;
    public Vector2Int chunkSize;
    public Vector2Int textureSize;
    public Material material;
    public RenderTexture mainBuffer;
    public bool evenIteration = true;
    // Input
    public string[] lines;
    public Vector2Int inputSize;
    public int inputLength;
    public ComputeBuffer inputBuffer;
    public ComputeBuffer inputCellsBuffer;
    public uint[] inputCells;
    public Cell[] inputCellsArray;
    // Debug
    public bool debug;
    public ComputeBuffer counterBuffer;
    public ComputeBuffer argsBuffer;
    uint[] counter = new uint[1];
    public ComputeBuffer debugBuffer;
    public uint[] debugArray;
    public CellChunk chunk;


    // Start is called before the first frame update
    void Start()
    {
        if (worldSize.x < 1 || worldSize.y < 1) return;
        AllocateMemory();
    }

    // Update is called once per frame
    void Update()
    {
        OneStep();
    }

    //  function info header
    //********************************************************************
    //  Allocate memory for the world
    //********************************************************************
    void AllocateMemory()
    {
        // Create texture
        mainBuffer = new RenderTexture(textureSize.x, textureSize.y, 24, RenderTextureFormat.ARGB32); //new RenderTexture(textureSize.x, textureSize.y, 24);
        mainBuffer.wrapMode = TextureWrapMode.Repeat;
        mainBuffer.enableRandomWrite = true;
        mainBuffer.filterMode = FilterMode.Point;
        mainBuffer.useMipMap = false;
        mainBuffer.Create();
        material.SetTexture("_mainBuffer", mainBuffer);

        // Create Counter
        counterBuffer = new ComputeBuffer(1, 4, ComputeBufferType.Counter);
        argsBuffer = new ComputeBuffer(1, sizeof(int), ComputeBufferType.IndirectArguments);

        // Create chunk
        chunk = new CellChunk(chunkSize, new ChunkCoordinate(0, 0, 0));

        // Debug
        if(debug)
        {
            //debugBuffer = new ComputeBuffer(chunkSize.x * chunkSize.y, sizeof(uint));
            //debugArray = new uint[chunkSize.x * chunkSize.y];
        }
    }

    //********************************************************************
    //  One step of the simulation
    //********************************************************************
    void OneStep()
    {
        ChunkShaderController.Instance.OneStep(chunk);
        ChunkShaderController.Instance.DrawChunk(chunk, mainBuffer);
        material.mainTexture = mainBuffer;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(material.mainTexture, destination);
    }

    void OnDestroy()
    {
        /*
        for (int i = 0; i < chunks.Count; i++)
        {
            chunks[i]?.DisposeChunk();
        }
        */
        chunk?.DisposeChunk();
        counterBuffer?.Dispose();
        argsBuffer?.Dispose();
        mainBuffer?.Release();
        inputBuffer?.Dispose();
        debugBuffer?.Dispose();
    }

    // On gui
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Cells: " + counter[0]);

        // Button OneStep
        if (GUI.Button(new Rect(10, 30, 100, 20), "Load"))
        {
            // Load From File in assets folder
            //LoadFromFile("Assets/inputChunk.txt");
            InsertCells();
        }

        // Button OneStep
        if (GUI.Button(new Rect(10, 50, 100, 20), "OneStep"))
        {
            OneStep();
        }
    }

    // ***********************************************************************
    // function:    LoadFromFile
    // description: Loads the chunk from a file
    // parameters:  string fileName - the name of the file to load
    // ***********************************************************************
    public void LoadFromFile(string fileName)
    {
        lines = System.IO.File.ReadAllLines(fileName);
        inputSize = new Vector2Int(lines[0].Length, lines.Length);
        inputCells = new uint[inputSize.x * inputSize.y];
        inputBuffer = new ComputeBuffer(inputSize.x * inputSize.y, sizeof(uint));

        for (int j = 0; j < inputSize.y; j++)
        {
            for (int i = 0; i < inputSize.x; i++)
            {
                int index = i + j * inputSize.x;

                switch (lines[j][i])
                {
                    case '.':
                        inputCells[index] = 0;
                        break;
                    case '#':
                        inputCells[index] = 1;
                        break;
                    case 'e':
                        inputCells[index] = 10;
                        break;
                }
            }
        }

        inputBuffer.SetData(inputCells);
        // Copy input buffer to chunk
        ChunkShaderController.Instance.CopyInput(chunk, mainBuffer);
        inputBuffer.Release();

        //material.mainTexture = mainBuffer;
    }

    // ***********************************************************************
    // function:    InsertCells
    // ***********************************************************************
    public void InsertCells()
    {
        inputLength = 3;
        ChunkShaderController.Instance.chunkShader.SetInt("_inputLength", inputLength);
        inputCellsArray = new Cell[3];
        inputCellsArray[0] = new Cell(new Vector2(0, 50), 10);
        inputCellsArray[1] = new Cell(new Vector2(64, 90), 10);
        inputCellsArray[2] = new Cell(new Vector2(100, 60), 10);
        inputCellsBuffer = new ComputeBuffer(3, sizeof(uint)*8, ComputeBufferType.Structured);
        inputCellsBuffer.SetData(inputCellsArray);
        ChunkShaderController.Instance.InsertCells(chunk);
    }

}
