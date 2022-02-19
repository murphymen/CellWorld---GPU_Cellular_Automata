using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorGenerator
{
    public int paletteSize = 128;

    // Dictionary of pair (int, Vector4[64])
    public Dictionary<int, Color32[]> cellColors;


    // Start is called before the first frame update
    public ColorGenerator()
    {
        cellColors = new Dictionary<int, Color32[]>();

        // Generate sand palette
        CreateColorPallette(1, new Vector4(0.9f, 0.9f, 0.9f, 1.0f));
    }

<<<<<<< HEAD
        uint[] Palette = new uint[paletteSize]; 
        //cellColors.TryGetValue(1, out palette);
=======
    // Function that converts vector4 to Color32
    Color32 Vector4ToColor32(Vector4 color)
    {
        return new Color32((byte)(color.x * 255), (byte)(color.y * 255), (byte)(color.z * 255), (byte)(color.w * 255));
>>>>>>> parent of e79fef1 (ColorGenerator)
    }


    void CreateColorPallette(int cellType, Vector4 baseColor)
    {
        Color32[] colorPallette = new Color32[paletteSize];

        for (int i = 0; i < paletteSize; i++)
        {
            // Randomize color.xyzw from 0 to 1 then multiply baseColor.x then normalize
<<<<<<< HEAD
            float r = Random.Range(0f, 1f) * baseColor.x;
            float g = Random.Range(0f, 1f) * baseColor.y;
            float b = Random.Range(0f, 1f) * baseColor.z;
            float a = 1f;
            Vector4 colorV4 = new Vector4(r, g, b, a);
            //Debug.Log("colorV4 :" + colorV4);
            colorV4.Normalize();
            //colorPallette[i] = Vector4ToColor32(colorV4);
            uint color = Vector4ToR8G8B8A8_uint(colorV4);
            colorPallette[i] = color;
            Debug.Log("color :" + colorPallette[i]);
=======
            float x = Random.Range(0f, 1f) * baseColor.x;
            float y = Random.Range(0f, 1f) * baseColor.y;
            float z = Random.Range(0f, 1f) * baseColor.z;
            float w = 1f;
            Vector4 colorV4 = new Vector4(x, y, z, w);
            colorV4.Normalize();
            colorPallette[i] = Vector4ToColor32(colorV4);
>>>>>>> parent of e79fef1 (ColorGenerator)
        }

        cellColors.Add(cellType, colorPallette);
    }
<<<<<<< HEAD

    // *********************************************************************************************
    // Conversion Vector4 (float4) to Color32 (uint)
    // *********************************************************************************************
    public uint Vector4ToR8G8B8A8_uint(Vector4 unpackedColor)
    {
        uint packedColor;

        unpackedColor.x = Mathf.Min(unpackedColor.x, 0x000000ff);
        unpackedColor.y = Mathf.Min(unpackedColor.y, 0x000000ff);
        unpackedColor.z = Mathf.Min(unpackedColor.z, 0x000000ff);
        unpackedColor.w = Mathf.Min(unpackedColor.w, 0x000000ff);

        packedColor = (uint)((uint)(unpackedColor.x*255) |
                            ((uint)(unpackedColor.y*255) << 8) |
                            ((uint)(unpackedColor.z*255) << 16) |
                            ((uint)(unpackedColor.w*255) << 24));

        return packedColor;
    }

    // Get Palette
    public uint[] GetPalette(int cellType)
    {
        uint[] palette;
        cellColors.TryGetValue(cellType, out palette);
        return palette;
    }

    // OnDestroy
    void OnDestroy()
    {
        cellColors.Clear();
    }

=======
>>>>>>> parent of e79fef1 (ColorGenerator)
}
