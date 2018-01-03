using UnityEngine;

public class left6343SlopeSolidBlockP3 : solidSlopeBlock
{
    public left6343SlopeSolidBlockP3() : base(-2, 4, 0, -63.43f)
    {
        color = new Color32(224, 0, 0, 128);
        blockName = "Solid Block Left Slope 63.43 degrees Part 3";
        vertices = new Vector3[3];
        vertices[0] = new Vector3(-4, -8, 0);
        vertices[1] = new Vector3(-8, 0, 0);
        vertices[2] = new Vector3(-8, -8, 0);
    }
}
