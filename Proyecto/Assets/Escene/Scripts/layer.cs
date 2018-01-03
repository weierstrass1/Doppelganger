using UnityEngine;
using Assets.Escene.Scripts.Blocks;

[System.Serializable]
public class layer : MobileObject
{
    public bool show = true;
    public int selectedX = 0, selectedY = 0;
    public int blockSize;
    public int width;
    public int height;
    public static block[] AllBlocks = { new solidBlock(),
        new solidBlockMidDown(),
        new right45SlopeSolidBlockP1(),
        new right45SlopeSolidBlockP2(),
        new left45SlopeSolidBlockP1(),
        new left45SlopeSolidBlockP2(),
        new right2657SlopeSolidBlockP1(),
        new right2657SlopeSolidBlockP2(),
        new DelegateToUpBlock(),
        new left2657SlopeSolidBlockP1(),
        new left2657SlopeSolidBlockP2(),
        new right6343SlopeSolidBlockP1(),
        new right6343SlopeSolidBlockP2(),
        new right6343SlopeSolidBlockP3(),
        new left6343SlopeSolidBlockP1(),
        new left6343SlopeSolidBlockP2(),
        new left6343SlopeSolidBlockP3(),
        new right7596SlopeSolidBlockP1(),
        new right7596SlopeSolidBlockP2(),
        new right7596SlopeSolidBlockP3(),
        new right7596SlopeSolidBlockP4(),
        new right7596SlopeSolidBlockP5(),
        new left7596SlopeSolidBlockP1(),
        new left7596SlopeSolidBlockP2(),
        new left7596SlopeSolidBlockP3(),
        new left7596SlopeSolidBlockP4(),
        new left7596SlopeSolidBlockP5(),
        new right1404SlopeSolidBlockP1(),
        new right1404SlopeSolidBlockP2(),
        new right1404SlopeSolidBlockP3(),
        new right1404SlopeSolidBlockP4(),
        new left1404SlopeSolidBlockP1(),
        new left1404SlopeSolidBlockP2(),
        new left1404SlopeSolidBlockP3(),
        new left1404SlopeSolidBlockP4(),
    };
    public int state = 0;

    public int initX = 0, initY = 0, endX = 0, endY = 0;
    public bool selectionStart = false;

    [SerializeField]
    [HideInInspector]
    public int[] map;

    public int getXPos(float X)
    {
        int xint = (int)x;
        int Xint = (int)X;

        return (Xint - xint) / blockSize;
    }

    public int getYPos(float Y)
    {
        int yint = (int)y;
        int Yint = (int)Y;

        return (Yint - height * blockSize + yint) / blockSize;
    }


    public int getBlock(float X,float Y)
    {
        int blockXPos = getXPos(X);

        int blockYPos = getYPos(Y);
        
        if (blockXPos >= width) return -1;
        if (blockXPos < 0) return -1;
        if (blockYPos >= height) return -1;
        if (blockYPos < 0) return -1;

        int index = map[blockXPos + (width * blockYPos)];

        if (index < 0) return -1;

        return index;
    }
}
