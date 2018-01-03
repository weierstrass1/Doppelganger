using UnityEngine;

[System.Serializable]
public class frame
{
    public int index;
    public string name;
    public bool fold;
    public graphicComponent mainObject;
    SpriteRenderer mainSR;
    Transform mainTrans;
    public bool GlobalFlipX = false;
    public bool GlobalFlipY = false;
    public bool canChangeSprite;
    public Sprite mainSprite;
    public bool specialXFlipSprite, specialYFlipSprite, specialXYFlipSprite;
    public Sprite xfMainSprite, yfMainSprite, xyfMainSprite;
    public bool blaxVersion,haveBlaxVersion;
    public Sprite blaxMainSprite;
    public bool blaxSpecialXFlipSprite, blaxSpecialYFlipSprite, blaxSpecialXYFlipSprite;
    public Sprite blaxXFMainSprite, blaxYFMainSprite, blaxXYFMainSprite;
    public bool canChangeX, canChangeY;
    public float mainPosX, mainPosY;

    public frame(graphicComponent MainObject,int Index)
    {
        index = Index;
        mainObject = MainObject;
    }

    public void Use()
    {
        mainSR = mainObject.GetComponent<SpriteRenderer>();

        mainTrans = mainObject.GetComponent<Transform>();

        bool fx = mainObject.mainFlipX ^ GlobalFlipX;
        bool fy = mainObject.mainFlipY ^ GlobalFlipY;
        if (mainSR != null && canChangeSprite)
        {
            if (haveBlaxVersion && mainObject.blax)
            {
                if (fx && fy)
                {
                    if (blaxXYFMainSprite != null) mainSR.sprite = blaxXYFMainSprite;
                    else
                    {
                        mainSR.flipX = true;
                        mainSR.flipY = true;
                    }
                }
                else if (fx)
                {
                    if (blaxXFMainSprite != null) mainSR.sprite = blaxXFMainSprite;
                    else mainSR.flipX = true;
                    mainSR.flipY = false;
                }
                else if (fy)
                {
                    if (blaxYFMainSprite != null) mainSR.sprite = blaxYFMainSprite;
                    else mainSR.flipY = true;
                    mainSR.flipX = false;
                }
                else
                {
                    mainSR.sprite = blaxMainSprite;
                    mainSR.flipX = false;
                    mainSR.flipY = false;
                }
            }
            else
            {
                if (mainSprite != null)
                {
                    if (fx && fy)
                    {
                        if (xyfMainSprite != null) mainSR.sprite = xyfMainSprite;
                        else
                        {
                            mainSR.sprite = mainSprite;
                            mainSR.flipX = true;
                            mainSR.flipY = true;
                        }
                    }
                    else if (fx)
                    {
                        if (xfMainSprite != null) mainSR.sprite = xfMainSprite;
                        else
                        {
                            mainSR.sprite = mainSprite;
                            mainSR.flipX = true;
                            mainSR.flipY = false;
                        }
                    }
                    else if (fy)
                    {
                        if (yfMainSprite != null) mainSR.sprite = yfMainSprite;
                        else
                        {
                            mainSR.sprite = mainSprite;
                            mainSR.flipY = true;
                        }
                        mainSR.flipX = false;
                    }
                    else
                    {
                        mainSR.sprite = mainSprite;
                        mainSR.flipX = false;
                        mainSR.flipY = false;
                    }
                }
                else
                {
                    mainSR.sprite = null;
                }
            }
            mainObject.currentFrame = index;
        }
        
        float x = mainTrans.localPosition.x;
        if(canChangeX)
        {
            x = mainPosX;
            if (GlobalFlipX) x = -x;
        }

        float y = mainTrans.localPosition.y;
        if(canChangeY)
        {
            y = mainPosY;
            if (GlobalFlipY) y = -y;
        }
        
        mainTrans.localPosition = new Vector3(x, y, mainTrans.localPosition.z);
    }
}
