using UnityEngine;

public class left6343SlopeSolidBlockP1 : solidSlopeBlock
{
    public left6343SlopeSolidBlockP1() : base(-2, 16, 8, -63.43f)
    {
        color = new Color32(224, 0, 0, 128);
        blockName = "Solid Block Left Slope 63.43 degrees Part 1";
        vertices = new Vector3[5];
        vertices[0] = new Vector3(8, -8, 0);
        vertices[1] = new Vector3(8, 0, 0);
        vertices[2] = new Vector3(4, 8, 0);
        vertices[3] = new Vector3(-8, 8, 0);
        vertices[4] = new Vector3(-8, -8, 0);
    }
}
