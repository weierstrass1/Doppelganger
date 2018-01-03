using UnityEngine;

[System.Serializable]
public class customAnimation : MonoBehaviour
{
    [SerializeField]
    [HideInInspector]
    public frame[] frames;
    public graphicComponent mainObject;

    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
	}
}
