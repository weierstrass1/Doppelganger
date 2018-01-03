using UnityEngine;

public class solidBlock : block
{
    public solidBlock()
    {
        color = new Color32(224, 0, 0, 128);
        blockName = "Solid Block 16x16";
        vertices = new Vector3[4];
        vertices[0] = new Vector3(-8, -8, 0);
        vertices[1] = new Vector3(-8, 8, 0);
        vertices[2] = new Vector3(8, 8, 0);
        vertices[3] = new Vector3(8, -8, 0);
    }

    public override void angleDetector(MobileObject target, Transform contactPoint, float x, float y, int blockSize, layer l)
    {
        if (!target.Solid) return;

        float contactY = contactPoint.position.y;
        if (target.YSpeed <= 0 || target.GuidedByTerrain)
        {
            target.y += (y + 15) - contactY;
            target.TerrainAngle = 0;
            target.BlockedAngleDetector = true;
        }
    }

    public override void down(MobileObject target, Transform contactPoint, float x, float y, int blockSize, layer l)
    {
        if (!target.Solid) return;
        float contactY = contactPoint.position.y;
        if (target.YSpeed > 0 && contactY < y + (blockSize / 2))
        {
            target.y -= contactY - y;
            target.BlockedFromAbove = true;
        }
    }

    public override void left(MobileObject target, Transform contactPoint, float x, float y, int blockSize, layer l)
    {
        if (!target.Solid) return;

        if (target.XSpeed > 0 && contactPoint.position.x > x + 1)
        {
            target.BlockedFromRight = true;
            target.x += x + 1 - contactPoint.position.x;
            target.XSpeed = 0;
        }
    }

    public override void right(MobileObject target, Transform contactPoint, float x, float y, int blockSize, layer l)
    {
        if (!target.Solid) return;


        if (target.XSpeed < 0 && contactPoint.position.x < (x + 15))
        {
            target.BlockedFromLeft = true;
            target.x += (x + 15) - contactPoint.position.x;
            target.XSpeed = 0;
        }
    }

    public override void up(MobileObject target, Transform contactPoint, float x, float y, int blockSize, layer l)
    {
        if (!target.Solid) return;

        if (target.YSpeed <= 0 || target.GuidedByTerrain)
        {
            target.BlockedFromBelow = true;
        }
    }
}
