using System;
using UnityEngine;

public class solidBlockMidDown : block
{
    public solidBlockMidDown()
    {
        color = new Color32(224, 0, 0, 128);
        blockName = "Solid Block 16x8 Down";
        vertices = new Vector3[4];
        vertices[0] = new Vector3(-8, -8, 0);
        vertices[1] = new Vector3(-8, 0, 0);
        vertices[2] = new Vector3(8, 0, 0);
        vertices[3] = new Vector3(8, -8, 0);
    }

    public override void angleDetector(MobileObject target, Transform contactPoint, float x, float y, int blockSize, layer l)
    {
        if (!target.Solid) return;

        float contactY = contactPoint.position.y;
        if ((target.YSpeed <= 0 && contactY - y <= 7) || target.GuidedByTerrain)
        {
            target.y += (y + 7) - contactY;
            target.TerrainAngle = 0;
            target.BlockedAngleDetector = true;
        }
    }

    public override void down(MobileObject target, Transform contactPoint, float x, float y, int blockSize, layer l)
    {
        throw new NotImplementedException();
    }

    public override void left(MobileObject target, Transform contactPoint, float x, float y, int blockSize, layer l)
    {
        if (!target.Solid || (target.TerrainAngle > 0 && target.BlockedFromBelow)) return;

        float contactY = contactPoint.position.y;
        if (target.YSpeed <= 0 && contactY - y <= 7 && target.XSpeed > 0 && contactPoint.position.x > x + 1)
        {
            target.BlockedFromRight = true;
            target.x += x + 1 - contactPoint.position.x;
            target.XSpeed = 0;
        }
    }

    public override void right(MobileObject target, Transform contactPoint, float x, float y, int blockSize, layer l)
    {
        if (!target.Solid || (target.TerrainAngle < 0 && target.BlockedFromBelow)) return;

        float contactY = contactPoint.position.y;
        if (target.YSpeed <= 0 && contactY - y <= 7 && target.XSpeed < 0 && contactPoint.position.x < (x + 15))
        {
            target.BlockedFromLeft = true;
            target.x += (x + 15) - contactPoint.position.x;
            target.XSpeed = 0;
        }
    }

    public override void up(MobileObject target, Transform contactPoint, float x, float y, int blockSize, layer l)
    {
        if (!target.Solid) return;

        float contactY = contactPoint.position.y;
        if ((target.YSpeed <= 0 && contactY - y <= 7) || target.GuidedByTerrain)
        {
            target.BlockedFromBelow = true;
        }
    }
}
