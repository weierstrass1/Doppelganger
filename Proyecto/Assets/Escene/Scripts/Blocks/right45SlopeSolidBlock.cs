using UnityEngine;

public class right45SlopeSolidBlock : solidSlopeBlock
{
    public right45SlopeSolidBlock() : base(1, 0, 0, 45)
    {
        color = new Color32(224, 0, 0, 128);
        blockName = "Solid Block Right Slope 45 degrees";
        vertices = new Vector3[3];
        vertices[0] = new Vector3(-8, -8, 0);
        vertices[1] = new Vector3(8, 8, 0);
        vertices[2] = new Vector3(8, -8, 0);
    }
}
