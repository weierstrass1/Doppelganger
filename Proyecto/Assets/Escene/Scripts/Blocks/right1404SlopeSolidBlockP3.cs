using UnityEngine;

public class right1404SlopeSolidBlockP3 : solidSlopeBlock
{
    public right1404SlopeSolidBlockP3() : base(0.25f, 0, 0, 14.04f)
    {
        color = new Color32(224, 0, 0, 128);
        blockName = "Solid Block Right Slope 14.04 degrees Part 3";
        vertices = new Vector3[3];
        vertices[0] = new Vector3(-8, -8, 0);
        vertices[1] = new Vector3(8, -4, 0);
        vertices[2] = new Vector3(8, -8, 0);
    }
}
