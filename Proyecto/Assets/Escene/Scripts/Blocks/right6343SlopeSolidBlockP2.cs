﻿using UnityEngine;

public class right6343SlopeSolidBlockP2 : solidSlopeBlock
{
    public right6343SlopeSolidBlockP2() : base(2, 4, 0, 63.43f)
    {
        color = new Color32(224, 0, 0, 128);
        blockName = "Solid Block Right Slope 63.43 degrees Part 2";
        vertices = new Vector3[4];
        vertices[0] = new Vector3(-4, -8, 0);
        vertices[1] = new Vector3(4, 8, 0);
        vertices[2] = new Vector3(8, 8, 0);
        vertices[3] = new Vector3(8, -8, 0);
    }
}