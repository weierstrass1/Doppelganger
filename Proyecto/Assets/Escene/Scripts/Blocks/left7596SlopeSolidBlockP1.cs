using UnityEngine;

public class left7596SlopeSolidBlockP1 : solidSlopeBlock
{
    public left7596SlopeSolidBlockP1() : base(-4, 16, 8, -75.96f)
    {
        color = new Color32(224, 0, 0, 128);
        blockName = "Solid Block Left Slope 75.96 degrees Part 1";
        vertices = new Vector3[5];
        vertices[0] = new Vector3(8, -8, 0);
        vertices[1] = new Vector3(8, 0, 0);
        vertices[2] = new Vector3(6, 8, 0);
        vertices[3] = new Vector3(-8, 8, 0);
        vertices[4] = new Vector3(-8, -8, 0);
    }
}
