using UnityEngine;

public delegate void charComponentAction();
public delegate void charComponentStartAction();

[System.Serializable]
public class charComponent : MonoBehaviour
{
    [HideInInspector]
    public string scriptName;
    public string Name;
    public string Description;
    [HideInInspector]
    [SerializeField]
    public bool flipX;
    public bool FlipX
    {
        get
        {
            return flipX;
        }
        set
        {
            bool lastFX = flipX;
            flipX = value;

            if (lastFX != flipX)
            {
                if (graphicComponents != null)
                {
                    foreach (graphicComponent gc in graphicComponents)
                    {
                        if (gc != null)
                        {
                            gc.mainFlipX = flipX;
                        }
                    }
                }

                if (graphicsContainers != null)
                {
                    foreach (Transform t in graphicsContainers)
                    {
                        t.localPosition = new Vector3(-t.localPosition.x, t.localPosition.y, t.localPosition.z);
                    }
                }

                if (contactDown != null)
                {
                    foreach (Transform t in contactDown)
                    {
                        t.localPosition = new Vector3(-t.localPosition.x, t.localPosition.y, t.localPosition.z);
                    }
                }

                if (FlippableXPoints != null)
                {
                    foreach (Transform t in FlippableXPoints)
                    {
                        t.localPosition = new Vector3(-t.localPosition.x, t.localPosition.y, t.localPosition.z);
                    }
                }

                if (projectileLaunchers != null)
                {
                    Transform t;
                    foreach (projectileLauncher pl in projectileLaunchers)
                    {
                        t = pl.transform;
                        t.localPosition = new Vector3(-t.localPosition.x, t.localPosition.y, t.localPosition.z);
                    }
                }
                
                Transform[] taux = contactLeft;
                contactLeft = contactRight;
                contactRight = taux;
            }
        }
    }
    [HideInInspector]
    [SerializeField]
    public bool flipY;
    public bool FlipY
    {
        get
        {
            return flipY;
        }
        set
        {
            flipY = value;
            if (graphicsContainers != null)
            {
                foreach (graphicComponent gc in graphicComponents)
                {
                    if (gc != null)
                    {
                        gc.mainFlipY = flipY;
                    } 
                }
            }
        }
    }

    [SerializeField]
    public charComponentAction updateComponent;
    public charComponentStartAction startComponent;

    public int baseMaxHP=1, baseEnergy=1, baseSTR=1, baseAGI=1, baseDEX=1, baseDEF=1;
    public int hpIncresing = 1, energyIncresing = 1;

    public skill[] skills;
    public command[] cmds;
    public properties[] props;
    public Transform mainPos;
    public Transform[] contactDown, contactUp, contactLeft, contactRight, angleDetector;
    public Transform[] FlippableXPoints, FlippableYPoints;
    public interactor[] interactors;
    public Transform[] graphicsContainers;
    public graphicComponent[] graphicComponents;
    public animationComponent animations;

    public animation[] Animations
    {
        get { return animations.animations; }
    }

    public projectileLauncher[] projectileLaunchers;
    public BaseCharacter owner;

    public void AwakeComponent()
    {
        if (animations != null && animations.animations != null)
        {
            foreach (animation an in animations.animations)
            {
                an.owner = this;
            }
        }
        
        if (startComponent != null) startComponent();
    }

    public void UpdateComponent()
    {
        if (updateComponent != null) updateComponent();
    }
}