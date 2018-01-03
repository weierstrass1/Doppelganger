using UnityEngine;

public class AvailablesLayers
{
    public const int Default = 0;
    public const int TransparentFX = 1;
    public const int IgnoreRayCast = 2;
    public const int Water = 4;
    public const int UI = 5;
    public const int Floor = 5;
    public const int Camera = 9;
    public const int CCPFromRight = 10;
    public const int CCPFromLeft = 11;
    public const int CCPFromTop = 12;
    public const int CCPFromBottom = 13;
    public const int Ally = 14;
    public const int Enemy = 15;
}

public abstract class MobileObject : MonoBehaviour
{
    public const float pixelPerMeter = 28;

    [HideInInspector]
    public int mass;
    public int Mass
    {
        get { return mass; }
        set { mass = value; }
    }

    [HideInInspector]
    public bool solid = true;
    public bool Solid
    {
        get { return solid; }
        set { solid = value; }
    }

    public bool GuidedByTerrain = true;

    [HideInInspector]
    public bool blockedAngleDetector = true;
    public bool BlockedAngleDetector
    {
        get { return blockedAngleDetector; }
        set { blockedAngleDetector = value; }
    }

    public bool blockedFromBelow = true;
    public bool BlockedFromBelow
    {
        get { return blockedFromBelow; }
        set { blockedFromBelow = value; }
    }

    public bool IgnoreBelowInteraction = false;

    [HideInInspector]
    public bool blockedFromLeft;
    public bool BlockedFromLeft
    {
        get { return blockedFromLeft; }
        set
        {
            blockedFromLeft = value;
        }
    }

    [HideInInspector]
    public bool blockedFromRight;
    public bool BlockedFromRight
    {
        get { return blockedFromRight; }
        set { blockedFromRight = value; }
    }

    [HideInInspector]
    public bool blockedFromAbove;
    public bool BlockedFromAbove
    {
        get { return blockedFromAbove; }
        set { blockedFromAbove = value; }
    }

    public float x
    {
        get { return GetComponent<Transform>().position.x; }
        set
        {
            GetComponent<Transform>().position = new Vector3(
                value,
                GetComponent<Transform>().position.y,
                GetComponent<Transform>().position.z);
        }
    }
    public float y
    {
        get { return GetComponent<Transform>().position.y; }
        set
        {
            GetComponent<Transform>().position = new Vector3(
                GetComponent<Transform>().position.x,
                value,
                GetComponent<Transform>().position.z);
        }
    }

    [HideInInspector]
    private float terrainAngle;
    public float TerrainAngle
    {
        get
        {
            return terrainAngle;
        }
        set
        {
            terrainAngle = value;
        }
    }

    [HideInInspector]
    public float xSpeed;
    public float XSpeed
    {
        get
        {
            return xSpeed;
        }
        set
        {
            xSpeed = value;
        }
    }

    [HideInInspector]
    public float ySpeed;
    public float YSpeed
    {
        get
        {
            return ySpeed;
        }
        set
        {
            ySpeed = value;
        }
    }

    [HideInInspector]
    public float minXSpeed;
    public float MinXSpeed
    {
        get
        {
            return minXSpeed;
        }
        set
        {
            minXSpeed = value;
            if (minXSpeed > maxXSpeed) minXSpeed = maxXSpeed;
        }
    }

    [HideInInspector]
    public float minYSpeed;
    public float MinYSpeed
    {
        get
        {
            return minYSpeed;
        }
        set
        {
            minYSpeed = value;
            if (minYSpeed > maxYSpeed) minYSpeed = maxYSpeed;
        }
    }

    [HideInInspector]
    public float maxXSpeed;
    public float MaxXSpeed
    {
        get
        {
            return maxXSpeed;
        }
        set
        {
            maxXSpeed = value;
            if (maxXSpeed < minXSpeed) maxXSpeed = minXSpeed;
        }
    }

    [HideInInspector]
    public float maxYSpeed;
    public float MaxYSpeed
    {
        get
        {
            return maxYSpeed;
        }
        set
        {
            maxYSpeed = value;
            if (maxYSpeed < minYSpeed) maxYSpeed = minYSpeed;
        }
    }

    [HideInInspector]
    public float xAccel;
    public float XAccel
    {
        get
        {
            return xAccel;
        }
        set
        {
            xAccel = value;
        }
    }

    [HideInInspector]
    public float yAccel;
    public float YAccel
    {
        get
        {
            return yAccel;
        }
        set
        {
            yAccel = value;
        }
    }

    [HideInInspector]
    public float xGravity;
    public float XGravity
    {
        get
        {
            return xGravity;
        }
        set
        {
            xGravity = value;
        }
    }

    [HideInInspector]
    public float yGravity;
    public float YGravity
    {
        get
        {
            return yGravity;
        }
        set
        {
            yGravity = value;
        }
    }

    [HideInInspector]
    public float xFriction;
    public float XFriction
    {
        get
        {
            return xFriction;
        }
        set
        {
            xFriction = value;
        }
    }

    [HideInInspector]
    public float yFriction;
    public float YFriction
    {
        get
        {
            return yFriction;
        }
        set
        {
            yFriction = value;
        }
    }

    public bool tryContactBelow(charComponent charC)
    {
        if (charC.contactDown == null || charC.contactDown.Length <= 0) return false;

        layer[] ls = level.Instance.layers;

        if (ls == null || ls.Length <= 0) return false;

        bool lastBlockedFromBelow = BlockedFromBelow;
        float ny = transform.position.y;

        BlockedFromBelow = false;

        foreach (layer l in ls)
        {
            foreach (Transform t in charC.contactDown)
            {
                if (t.gameObject.activeSelf)
                {
                    int b = l.getBlock(t.position.x, t.position.y);

                    if (b >= 0)
                    {
                        float X = l.getXPos(t.position.x) * l.blockSize;
                        float Y = l.getYPos(t.position.y) * l.blockSize;

                        layer.AllBlocks[b].up(this, t, X, Y, l.blockSize, l);
                    }
                }
            }
        }
        bool ret = BlockedFromBelow;
        BlockedFromBelow = lastBlockedFromBelow;
        transform.position = new Vector3(transform.position.x, ny, transform.position.z);
        return ret;
    }

    public void checkContactPoints(charComponent charC)
    {
        if (Solid)
        {
            layer[] ls = level.Instance.layers;

            foreach (layer l in ls)
            {
                if (charC.contactDown != null && !IgnoreBelowInteraction)
                {
                    bool bfb = BlockedFromBelow;

                    BlockedFromBelow = false;

                    foreach (Transform t in charC.contactDown)
                    {
                        if (t.gameObject.activeSelf)
                        {
                            int b = l.getBlock(t.position.x, t.position.y);

                            if (b >= 0)
                            {
                                float X = l.getXPos(t.position.x) * l.blockSize;
                                float Y = l.getYPos(t.position.y) * l.blockSize;

                                layer.AllBlocks[b].up(this, t, X, Y, l.blockSize, l);
                            }
                        }
                    }

                    BlockedAngleDetector = false;

                    foreach (Transform t in charC.angleDetector)
                    {
                        if (t.gameObject.activeSelf)
                        {
                            int b = l.getBlock(t.position.x, t.position.y);

                            if (b >= 0)
                            {
                                float X = l.getXPos(t.position.x) * l.blockSize;
                                float Y = l.getYPos(t.position.y) * l.blockSize;

                                layer.AllBlocks[b].angleDetector(this, t, X, Y, l.blockSize, l);
                            }
                            break;
                        }
                    }

                    if (bfb && !BlockedFromBelow && !GuidedByTerrain)
                    {
                        Vector2 v = Rotate(TerrainAngle, XSpeed, YSpeed);

                        XSpeed = v.x;
                        YSpeed = v.y;
                        TerrainAngle = 0;
                    }
                }

                if (charC.contactRight != null)
                {
                    BlockedFromRight = false;
                    foreach (Transform t in charC.contactRight)
                    {
                        if (t.gameObject.activeSelf)
                        {
                            int b = l.getBlock(t.position.x, t.position.y);

                            if (b >= 0)
                            {
                                float X = l.getXPos(t.position.x) * l.blockSize;
                                float Y = l.getYPos(t.position.y) * l.blockSize;

                                layer.AllBlocks[b].left(this, t, X, Y, l.blockSize, l);
                            }
                        }
                    }
                }

                if (charC.contactLeft != null)
                {
                    BlockedFromLeft = false;
                    foreach (Transform t in charC.contactLeft)
                    {
                        if (t.gameObject.activeSelf)
                        {
                            int b = l.getBlock(t.position.x, t.position.y);

                            if (b >= 0)
                            {
                                float X = l.getXPos(t.position.x) * l.blockSize;
                                float Y = l.getYPos(t.position.y) * l.blockSize;

                                layer.AllBlocks[b].right(this, t, X, Y, l.blockSize, l);
                            }
                        }
                    }
                }
            }
        }
    }

    public float getRealSpeed(int XSpeedPercentBonus)
    {
        return XSpeed * (1f + (XSpeedPercentBonus / 100f));
    }

    public void applySpeed(bool gravity, bool friction, bool acceleration, bool minSpeed, bool maxSpeed)
    {
        float xs = XSpeed;
        float ys = YSpeed;
        if(BlockedFromBelow)
        {
            Vector2 v = Rotate(TerrainAngle, XSpeed, YSpeed);

            xs = v.x;
            ys = v.y;
        }
        
        float nx = xs * pixelPerMeter * Time.deltaTime;
        float ny = ys * pixelPerMeter * Time.deltaTime;

        if (nx > 16) nx = 16;
        if (nx < -16) nx = -16;
        if (ny > 16) ny = 16;
        if (ny < -16) ny = -16;

        x += nx;
        y += ny;

        if (gravity) applyGravity();

        if (friction) applyFriction();
    }

    public Vector2 gravityResistance()
    {
        Vector2 v = Rotate(TerrainAngle, XGravity, YGravity);
        return v;
    }

    public static Vector2 Rotate(float angle, float x, float y)
    {
        float radAngle = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(radAngle);

        if (Mathf.Abs(cos) < 0.0001) cos = 0;

        float sin = Mathf.Sin(radAngle);

        if (Mathf.Abs(sin) < 0.0001) sin = 0;

        float nx = x * cos - y * sin;
        float ny = x * sin + cos * y;

        return new Vector2(nx, ny);
    }

    private void applyAccel()
    {
        XSpeed += XAccel * Time.deltaTime;
        YSpeed += YAccel * Time.deltaTime;
    }

    private void applyGravity()
    {
        if(!BlockedFromBelow)
        {
            XSpeed -= XGravity * Time.deltaTime;
            YSpeed -= YGravity * Time.deltaTime;
        }
        else
        {
            YSpeed = 0;
        }
        
    }

    private void applyFriction()
    {
        if (!BlockedFromBelow) return;

        if (XSpeed > 0)
        {
            XSpeed -= XFriction * Time.deltaTime;
            if (XSpeed < 0) XSpeed = 0;
        }
        else if (XSpeed < 0)
        {
            XSpeed += XFriction * Time.deltaTime;
            if (XSpeed > 0) XSpeed = 0;
        }


        if (YSpeed > 0)
        {
            YSpeed -= YFriction * Time.deltaTime;
            if (YSpeed < 0) YSpeed = 0;
        }
        else if (YSpeed < 0)
        {
            YSpeed += YFriction * Time.deltaTime;
            if (YSpeed > 0) YSpeed = 0;
        }
    }

    private void applyMinSpeed()
    {
        if (MinXSpeed != 0)
        {
            if (XSpeed > 0 && XSpeed < MinXSpeed) XSpeed = MinXSpeed;
            else if (XSpeed < 0 && XSpeed > -MinXSpeed) XSpeed = -MinXSpeed;
        }

        if (MinYSpeed != 0)
        {
            if (YSpeed > 0 && YSpeed < MinYSpeed) YSpeed = MinYSpeed;
            else if (YSpeed < 0 && YSpeed > -MinYSpeed) YSpeed = -MinYSpeed;
        }
    }

    private void applyMaxSpeed()
    {
        if (MaxXSpeed != 0)
        {
            if (XSpeed > 0 && XSpeed > MaxXSpeed) XSpeed = MaxXSpeed;
            else if (XSpeed < 0 && XSpeed < -MaxXSpeed) XSpeed = -MaxXSpeed;
        }

        if (MaxYSpeed != 0)
        {
            if (YSpeed > 0 && YSpeed > MaxYSpeed) YSpeed = MaxYSpeed;
            else if (YSpeed < 0 && YSpeed < -MaxYSpeed) YSpeed = -MaxYSpeed;
        }
    }
}
