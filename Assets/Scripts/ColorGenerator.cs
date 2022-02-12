using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorGenerator : UnitySingleton<ColorGenerator>
{
    public int palletesCount = 1;
    public int paletteSize = 128;

    // Dictionary of pair (int, Vector4[64])
    public Dictionary<int, uint[]> cellColors;
    public uint[] palette;

    // Start is called before the first frame update
    void Start()
    {
        cellColors = new Dictionary<int, uint[]>();

        // Generate sand palette
        CreateColorPallette(1, new Vector4(0.9f, 0.9f, 0.9f, 1.0f));

        uint[] Palette = new uint[paletteSize]; 
        cellColors.TryGetValue(1, out palette);
    }

    void CreateColorPallette(int cellType, Vector4 baseColor)
    {
        uint[] colorPallette = new uint[paletteSize];

        for (int i = 0; i < paletteSize; i++)
        {
            // Randomize color.xyzw from 0 to 1 then multiply baseColor.x then normalize
            float r = Random.Range(0f, 1f) * baseColor.x;
            //Debug.Log("r :"+r);
            float g = Random.Range(0f, 1f) * baseColor.y;
            float b = Random.Range(0f, 1f) * baseColor.z;
            float a = 1f;
            Vector4 colorV4 = new Vector4(r, g, b, a);
            //Debug.Log("colorV4 :" + colorV4);
            colorV4.Normalize();
            //colorPallette[i] = Vector4ToColor32(colorV4);
            uint color = Vector4ToColor32(colorV4);
            Debug.Log("color :" + color);
        }

        cellColors.Add(cellType, colorPallette);
    }

    public uint Vector4ToColor32(Vector4 unpackedColor)
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

    

}
