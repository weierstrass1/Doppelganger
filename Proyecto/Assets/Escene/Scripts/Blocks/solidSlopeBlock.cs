using UnityEngine;

public abstract class solidSlopeBlock : solidBlock
{
    protected float m;
    protected float xAdder;
    protected float yAdder;
    protected float angle;

    public solidSlopeBlock(float M, float XAdder, float YAdder, float Angle)
    {
        m = M;
        xAdder = XAdder;
        yAdder = YAdder;
        angle = Angle;
    }

    public override void angleDetector(MobileObject target, Transform contactPoint, float x, float y, int blockSize, layer l)
    {
        if (!target.Solid) return;

        float contactX = contactPoint.position.x;
        float contactY = contactPoint.position.y;

        float deltaX = contactX - (x + xAdder);
        float deltaY = contactY - (y + yAdder);

        if (deltaY <= m * deltaX || (target.BlockedFromBelow && target.GuidedByTerrain))
        {
            target.y += m * deltaX + (y + yAdder) - contactY;
            target.TerrainAngle = angle;
            target.BlockedAngleDetector = true;
        }
    }

    public override void left(MobileObject target, Transform contactPoint, float x, float y, int blockSize, layer l)
    {
        if (angle > 0) return;
        if (target.TerrainAngle < 0 && target.BlockedFromBelow) return;

        float contactX = contactPoint.position.x;
        float contactY = contactPoint.position.y;

        float deltaX = contactX - (x + xAdder);
        float deltaY = contactY - (y + yAdder);

        if (deltaY <= m * deltaX)
        {
            base.left(target, contactPoint, x, y, blockSize, l);
        }
    }

    public override void right(MobileObject target, Transform contactPoint, float x, float y, int blockSize, layer l)
    {
        if (angle < 0) return;
        if (target.TerrainAngle > 0 && target.BlockedFromBelow) return;

        float contactX = contactPoint.position.x;
        float contactY = contactPoint.position.y;

        float deltaX = contactX - (x + xAdder);
        float deltaY = contactY - (y + yAdder);

        if (deltaY <= m * deltaX)
        {
            base.right(target, contactPoint, x, y, blockSize, l);
        }
    }

    public override void up(MobileObject target, Transform contactPoint, float x, float y, int blockSize, layer l)
    {
        if (!target.Solid) return;

        float contactX = contactPoint.position.x;
        float contactY = contactPoint.position.y;

        float deltaX = contactX - (x + xAdder);
        float deltaY = contactY - (y + yAdder);

        if (deltaY <= m * deltaX || (target.BlockedFromBelow && target.GuidedByTerrain))
        {
            target.BlockedFromBelow = true;
        }
    }
}
