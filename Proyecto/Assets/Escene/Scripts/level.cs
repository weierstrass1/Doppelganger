using UnityEngine;

public class level : MonoBehaviour
{
    public layer[] layers;

    private static level instance;
    public static level Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<level>();
            }
            return instance;
        }
    }
}
