﻿using UnityEngine;

public class right7596SlopeSolidBlockP4 : solidSlopeBlock
{
    public right7596SlopeSolidBlockP4() : base(4, 10, 0, 75.96f)
    {
        color = new Color32(224, 0, 0, 128);
        blockName = "Solid Block Right Slope 75.96 degrees Part 4";
        vertices = new Vector3[4];
        vertices[0] = new Vector3(2, -8, 0);
        vertices[1] = new Vector3(6, 8, 0);
        vertices[2] = new Vector3(8, 8, 0);
        vertices[3] = new Vector3(8, -8, 0);
    }
}