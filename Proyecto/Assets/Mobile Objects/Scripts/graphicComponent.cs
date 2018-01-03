using UnityEngine;

[System.Serializable]
public class graphicComponent : MonoBehaviour
{
    public bool blax;
    public int currentFrame = 0;
    [HideInInspector]
    public frame[] allFrames;
    [SerializeField]
    public bool mainFlipX,mainFlipY;
    [HideInInspector]
    public bool invisible;
}
