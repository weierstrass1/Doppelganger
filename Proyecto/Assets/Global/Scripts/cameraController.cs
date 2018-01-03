using UnityEngine;

public class cameraController : MonoBehaviour
{

    public Transform target;
    private Camera cam;
    float initY;
    public bool BlockedFromLeft;


	// Use this for initialization
	void Start ()
    {
        initY = transform.position.y;
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        cam = GetComponent<Camera>();

        collider.size = new Vector2(cam.orthographicSize * 2 * cam.aspect, cam.orthographicSize * 2);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            BlockedFromLeft = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            BlockedFromLeft = false;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        float newY = transform.position.y;
        if (target.position.y > initY) newY = target.position.y;

        float newX = target.position.x;

        if (newX < transform.position.x && BlockedFromLeft) newX = transform.position.x;
        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}
