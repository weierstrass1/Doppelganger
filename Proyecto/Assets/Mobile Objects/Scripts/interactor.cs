using System;
using UnityEngine;

public class interactor : MonoBehaviour
{
    private Action<Collision2D> collisionEnter, collisionExit, collisionStay;
    public Action<Collision2D> CollisionEnter { set { collisionEnter = value; } }
    public Action<Collision2D> CollisionExit { set { collisionExit = value; } }
    public Action<Collision2D> CollisionStay { set { collisionStay = value; } }
    private Action<Collider2D> triggerEnter, triggerExit, triggerStay;
    public Action<Collider2D> TriggerEnter { set { triggerEnter = value; } }
    public Action<Collider2D> TriggerExit { set { triggerExit = value; } }
    public Action<Collider2D> TriggerStay { set { triggerStay = value; } }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (collisionEnter != null) collisionEnter(coll);
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if (collisionExit != null) collisionExit(coll);
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        if (collisionStay != null) collisionStay(coll);
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (triggerEnter != null) triggerEnter(coll);
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (triggerExit != null) triggerExit(coll);
    }
    void OnTriggerStay2D(Collider2D coll)
    {
        if (triggerStay != null) triggerStay(coll);
    }
}
