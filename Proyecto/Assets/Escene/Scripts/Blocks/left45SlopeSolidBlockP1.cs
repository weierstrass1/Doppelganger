using UnityEngine;

public class left45SlopeSolidBlockP1 : solidSlopeBlock
{
    public left45SlopeSolidBlockP1() : base(-1, 8, 16, -45)
    {
        color = new Color32(224, 0, 0, 128);
        blockName = "Solid Block Left Slope 45 degrees Part 1";
        vertices = new Vector3[5];
        vertices[0] = new Vector3(8, -8, 0);
        vertices[1] = new Vector3(8, 0, 0);
        vertices[2] = new Vector3(0, 8, 0);
        vertices[3] = new Vector3(-8, 8, 0);
        vertices[4] = new Vector3(-8, -8, 0);
    }
}
