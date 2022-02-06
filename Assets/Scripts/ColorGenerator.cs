using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorGenerator : UnitySingleton<ColorGenerator>
{
    public int paletteSize = 128;

    // Dictionary of pair (int, Vector4[64])
    public Dictionary<int, Color32[]> cellColors;


    // Start is called before the first frame update
    void Start()
    {
        cellColors = new Dictionary<int, Color32[]>();

        // Generate sand palette
        CreateColorPallette(1, new Vector4(0.9f, 0.9f, 0.9f, 1.0f));
    }

    // Function that converts vector4 to Color32
    Color32 Vector4ToColor32(Vector4 color)
    {
        return new Color32((byte)(color.x * 255), (byte)(color.y * 255), (byte)(color.z * 255), (byte)(color.w * 255));
    }


    void CreateColorPallette(int cellType, Vector4 baseColor)
    {
        Color32[] colorPallette = new Color32[paletteSize];

        for (int i = 0; i < paletteSize; i++)
        {
            // Randomize color.xyzw from 0 to 1 then multiply baseColor.x then normalize
            float x = Random.Range(0f, 1f) * baseColor.x;
            float y = Random.Range(0f, 1f) * baseColor.y;
            float z = Random.Range(0f, 1f) * baseColor.z;
            float w = 1f;
            Vector4 colorV4 = new Vector4(x, y, z, w);
            colorV4.Normalize();
            colorPallette[i] = Vector4ToColor32(colorV4);
        }

        cellColors.Add(cellType, colorPallette);
    }
}
