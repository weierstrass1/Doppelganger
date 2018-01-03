using UnityEngine;

public class windowParallax : MonoBehaviour
{
    public float xoffset = 0;
    public float xoffsetSpeed = 0;
    public float moveFraction;
    public cam c;
    public Renderer rend;
    public Texture tex;

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
        //transform.localPosition = new Vector3(initPos + deltaX * moveFraction, transform.localPosition.y, transform.localPosition.z);
        rend.material.mainTextureOffset = new Vector2(xoffset, 0);

        float deltaX = c.transform.position.x - c.lastPosX;
        deltaX /= transform.localScale.x;
        xoffset += (xoffsetSpeed * Time.deltaTime) - (deltaX * moveFraction);
    }

    void OnDrawGizmos()
    {
        rend = GetComponent<Renderer>();
        rend.sharedMaterial.mainTexture = tex;
        rend.sharedMaterial.mainTextureScale = new Vector2(2, 1);
    }
}
