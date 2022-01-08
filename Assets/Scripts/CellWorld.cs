using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellWorld : UnitySingleton<CellWorld>
{
    // World data3
     
    public Vector2Int worldSize;
    public Vector2Int chunkSize;
    public Vector2Int textureSize;
    public List<CellChunk> chunks;
    public Material material;
    public RenderTexture mainBuffer;
    // Input
    public Vector2Int inputSize;
    public ComputeBuffer inputBuffer;
    public uint[] inputCells;
    // Debug
    public ComputeBuffer counterBuffer;
    public ComputeBuffer argsBuffer;
    uint[] counter = new uint[1];
    public uint[] debugArray;
    public CellChunk chunk;


    // Start is called before the first frame update
    void Start()
    {
        if (worldSize.x < 1 || worldSize.y < 1) return;
        AllocateMemory();

        // LoadFromFile in assets folder
        LoadFromFile("Assets/inputChunk.txt");
        ChunkShaderController.Instance.InsertToChunk(chunk, inputBuffer, inputCells);

    }

    // Update is called once per frame
    void Update()
    {
    }

    //  function info header
    //********************************************************************
    //
    //********************************************************************
    void AllocateMemory()
    {
        //
        mainBuffer = new RenderTexture(textureSize.x, textureSize.y, 24);
        mainBuffer.wrapMode = TextureWrapMode.Repeat;
        mainBuffer.enableRandomWrite = true;
        mainBuffer.filterMode = FilterMode.Point;
        mainBuffer.useMipMap = false;
        mainBuffer.Create();
        material.SetTexture("_mainBuffer", mainBuffer);

        // 
        counterBuffer = new ComputeBuffer(1, 4, ComputeBufferType.Counter);
        argsBuffer = new ComputeBuffer(1, sizeof(int), ComputeBufferType.IndirectArguments);

        // Create chunk
        chunk = new CellChunk(chunkSize, new ChunkCoordinate(0, 0, 0));
        
        /*
        chunks = new List<CellChunk>();
        for (int j = 0; j < worldSize.y; j++)
        {
            for (int i = 0; i < worldSize.x; i++)
            {
                //chunks.Add(new CellChunk(textureSize, new ChunkCoordinate(i, j, 0)));
            }
        }
        */
    }

    //********************************************************************
    //  One step of the simulation
    //********************************************************************
    void OneStep()
    {
        ChunkShaderController.Instance.OneStep(chunk, mainBuffer);
        ChunkShaderController.Instance.DrawChunk(chunk, mainBuffer);
        material.mainTexture = mainBuffer;
        debugArray = new uint[inputSize.x*inputSize.y];
        inputBuffer.GetData(debugArray);
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
    }

    // On gui
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Cells: " + counter[0]);

        // Button OneStep
        if (GUI.Button(new Rect(10, 30, 100, 20), "OneStep"))
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
        string[] lines = System.IO.File.ReadAllLines(fileName);
        inputSize = new Vector2Int(lines[0].Length, lines.Length);
        inputCells = new uint[inputSize.x * inputSize.y];
        inputBuffer = new ComputeBuffer(inputSize.x * inputSize.y, sizeof(uint));

        for (int j = 0; j < inputSize.y; j++)
        {
            for (int i = 0; i < inputSize.x; i++)
            {
                int index = i + j * inputSize.x;
                if (lines[j][i] == '.')
                    inputCells[index] = 0;
                else if (lines[i][j] == '#')
                    inputCells[index] = 1;
                else
                    inputCells[index] = 2;
            }
        }
    }

}
