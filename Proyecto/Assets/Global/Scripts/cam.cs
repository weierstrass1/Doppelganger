using UnityEngine;

public class cam : MonoBehaviour
{
    public int initPosX = 0, initPosY = 0;
    public float lastPosX = 0;
	// Use this for initialization
	void Start ()
    {
        initPosX = (int)transform.position.x;
        lastPosX = initPosX;
        initPosY = (int)transform.position.y;
    }
	
	// Update is called once per frame
	void Update ()
    {
        lastPosX = transform.position.x;

    }
}
