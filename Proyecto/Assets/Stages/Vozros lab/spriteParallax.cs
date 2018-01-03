using UnityEngine;

public class spriteParallax : MonoBehaviour {
    public float Speed = 0;
    public float moveFraction;
    public cam c;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x + Speed*Time.deltaTime, transform.localPosition.y, transform.localPosition.z);

        float deltaX = c.transform.position.x - c.lastPosX;

        transform.localPosition = new Vector3(transform.localPosition.x + deltaX * moveFraction, transform.localPosition.y, transform.localPosition.z);

    }
}
