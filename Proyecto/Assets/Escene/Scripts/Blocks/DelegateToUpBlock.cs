using UnityEngine;

namespace Assets.Escene.Scripts.Blocks
{
    public class DelegateToUpBlock : block
    {

        public DelegateToUpBlock()
        {
            color = new Color32(0, 224, 0, 128);
            blockName = "Delegate To Up Block";
            vertices = new Vector3[4];
            vertices[0] = new Vector3(-8, -8, 0);
            vertices[1] = new Vector3(-8, 8, 0);
            vertices[2] = new Vector3(8, 8, 0);
            vertices[3] = new Vector3(8, -8, 0);
        }

        public override void angleDetector(MobileObject target, Transform contactPoint, float x, float y, int blockSize, layer l)
        {
            float ny = (l.y - blockSize * l.height);
            ny += y + blockSize;

            int b = l.getBlock(contactPoint.position.x, ny);

            if (b >= 0)
            {
                float X = l.getXPos(contactPoint.position.x) * l.blockSize;
                float Y = l.getYPos(ny) * l.blockSize;

                layer.AllBlocks[b].angleDetector(target, contactPoint, X, Y, l.blockSize, l);
            }
        }

        public override void down(MobileObject target, Transform contactPoint, float x, float y, int blockSize, layer l)
        {
        }

        public override void left(MobileObject target, Transform contactPoint, float x, float y, int blockSize, layer l)
        {
        }

        public override void right(MobileObject target, Transform contactPoint, float x, float y, int blockSize, layer l)
        {
        }

        public override void up(MobileObject target, Transform contactPoint, float x, float y, int blockSize, layer l)
        {
            float ny = (l.y - blockSize * l.height);
            ny += y + blockSize;

            int b = l.getBlock(contactPoint.position.x, ny);

            if (b >= 0)
            {
                float X = l.getXPos(contactPoint.position.x) * l.blockSize;
                float Y = l.getYPos(ny) * l.blockSize;

                layer.AllBlocks[b].up(target, contactPoint, X, Y, l.blockSize, l);
            }
        }
    }
}
