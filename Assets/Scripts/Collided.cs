using UnityEngine;

public class Collided : MonoBehaviour {
    public Stickman stick;
    public bool HasCollided;
    public Collided otherleg;

    void Update()
    {
        if (HasCollided && otherleg.HasCollided)
        {
            stick.HasCollided = true;
        }
        else
        {
            stick.HasCollided = false;
        }
    }

    void OnCollisionEnter2D (Collision2D coll) {
        if(coll.gameObject.tag == "World")
        {
            HasCollided = true;
        }
    }
    void OnCollisionStay2d (Collision2D coll) {
        if(coll.gameObject.tag == "World")
        {
            HasCollided = true;
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "World")
        {
            HasCollided = false;
        }
    }
}