using UnityEngine;

public class spriteRandomCreator : MonoBehaviour
{
    public GameObject go;
    public int minX, maxX;
    public int minY, maxY;
    public int minUp, maxUp;
    public int minZ, maxZ;

	// Use this for initialization
	void Awake ()
    {
        int x = minX;

        while (x < maxX)
        {
            x += globalVars.Random.Next(minUp, maxUp);
            GameObject goaux = Instantiate(go);
            goaux.transform.parent = transform;
            goaux.transform.localPosition = new Vector3(x,
                globalVars.Random.Next(minY, maxY),
                globalVars.Random.Next(minZ, maxZ));
        }		
	}
}
