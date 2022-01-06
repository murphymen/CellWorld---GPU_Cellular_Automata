using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellWorld : UnitySingleton<CellWorld>
{
    public Vector2Int worldSize;
    public Vector2Int textureSize;
    public List<CellChunk> chunks;
    public Material material;
    public RenderTexture renderTexture;

    // Debug
    public ComputeBuffer counterBuffer;
    public ComputeBuffer argsBuffer;
    uint[] counter = new uint[1];


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

    void AllocateMemory()
    {
        //
        renderTexture = new RenderTexture(worldSize.x, worldSize.y, 24);
        renderTexture.wrapMode = TextureWrapMode.Repeat;
        renderTexture.enableRandomWrite = true;
        renderTexture.filterMode = FilterMode.Point;
        renderTexture.useMipMap = false;
        renderTexture.Create();
        material.SetTexture("_renderTexture", renderTexture);

        //
        counterBuffer = new ComputeBuffer(1, 4, ComputeBufferType.Counter);
        argsBuffer = new ComputeBuffer(1, sizeof(int), ComputeBufferType.IndirectArguments);

        //
        for (int j = 0; j < worldSize.y; j++)
        {
            for (int i = 0; i < worldSize.x; i++)
            {
                chunks.Add(new CellChunk(textureSize, new ChunkCoordinate(i, j, 0)));
            }
        }
    }

    void OneStep()
    {
        for(int i = 0; i < chunks.Count; i++)
        {
            
        }
    }
}
