using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class solidBlockMidLeft : block
{
    public solidBlockMidLeft()
    {
        color = new Color32(224, 0, 0, 128);
        blockName = "Solid Block 8x16 Left";
        vertices = new Vector3[4];
        vertices[0] = new Vector3(-8, 8, 0);
        vertices[1] = new Vector3(0, 8, 0);
        vertices[2] = new Vector3(0, -8, 0);
        vertices[3] = new Vector3(-8, -8, 0);
    }
    public override void angleDetector(MobileObject target, Transform contactPoint, float x, float y, int blockSize, layer l)
    {
        if (!target.Solid) return;
        if (contactPoint.position.x - x > 7) return;

        float contactY = contactPoint.position.y;
        if ((target.YSpeed <= 0 && contactY - y <= 15) || target.GuidedByTerrain)
        {
            target.y += (y + 15) - contactY;
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
        if (target.XSpeed > 0 && contactPoint.position.x > x + 1 && contactPoint.position.x - x > 7)
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
        if (target.XSpeed < 0 && contactPoint.position.x < (x + 7))
        {
            target.BlockedFromLeft = true;
            target.x += (x + 15) - contactPoint.position.x;
            target.XSpeed = 0;
        }
    }

    public override void up(MobileObject target, Transform contactPoint, float x, float y, int blockSize, layer l)
    {
        if (!target.Solid) return;
        if (contactPoint.position.x - x > 7) return;

        float contactY = contactPoint.position.y;
        if ((target.YSpeed <= 0 && contactY - y <= 7) || target.GuidedByTerrain)
        {
            target.BlockedFromBelow = true;
        }
    }
}