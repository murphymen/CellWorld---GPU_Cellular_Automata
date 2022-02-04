using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorGenerator : UnitySingleton<ColorGenerator>
{
    public int paletteSize = 128;

    // Dictionary of pair (int, Vector4[64])
    public Dictionary<int, Vector4[]> cellColors;


    // Start is called before the first frame update
    void Start()
    {
        cellColors = new Dictionary<int, Vector4[]>();

        // Generate sand palette
        CreateColorPallette(1, new Vector4(0.9f, 0.9f, 0.9f, 1.0f));
    }

    void CreateColorPallette(int cellType, Vector4 baseColor)
    {
        Vector4[] colorPallette = new Vector4[paletteSize];

        for (int i = 0; i < paletteSize; i++)
        {
            Vector4 color = new Vector4();
            // Randomize color.xyzw from 0 to 1 then multiply baseColor.x then normalize
            color.x = Random.Range(0f, 1f) * baseColor.x;
            color.y = Random.Range(0f, 1f) * baseColor.y;
            color.z = Random.Range(0f, 1f) * baseColor.z;
            color.w = 1f;
            color.Normalize();
            colorPallette[i] = color;
        }

        cellColors.Add(cellType, colorPallette);
    }
}
