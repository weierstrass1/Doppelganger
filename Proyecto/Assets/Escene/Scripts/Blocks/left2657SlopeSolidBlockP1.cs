using UnityEngine;

public class left2657SlopeSolidBlockP1 : solidSlopeBlock
{
    public left2657SlopeSolidBlockP1() : base(-0.5f, 0, 16, -26.57f)
    {
        color = new Color32(224, 0, 0, 128);
        blockName = "Solid Block Left Slope 26.57 degrees Part 1";
        vertices = new Vector3[4];
        vertices[0] = new Vector3(8, 0, 0);
        vertices[1] = new Vector3(-8, 8, 0);
        vertices[2] = new Vector3(-8, -8, 0);
        vertices[3] = new Vector3(8, -8, 0);
    }
}
