using UnityEngine;

public class left2657SlopeSolidBlockP2 : solidSlopeBlock
{
    public left2657SlopeSolidBlockP2() : base(-0.5f, 0, 8, -26.57f)
    {
        color = new Color32(224, 0, 0, 128);
        blockName = "Solid Block Left Slope 26.57 degrees Part 2";
        vertices = new Vector3[3];
        vertices[0] = new Vector3(8, -8, 0);
        vertices[1] = new Vector3(-8, 0, 0);
        vertices[2] = new Vector3(-8, -8, 0);
    }
}
