using UnityEngine;

public class Collided : MonoBehaviour {
    public Stickman stick;
    public bool HasCollided;
    public Collided otherleg;
    // this is fine now, don't trip each other up
    // holding and all arm ***REMOVED*** is fine bar climbing
    // no idea how gonna do it, might have to drop
    // you can hold and grab the same thing, which might be problematic

    void Update()
    {
        if(otherleg == null)
        {
            if(gameObject.tag == "rFoot")
            {
                otherleg = stick.muscleL.bone.gameObject.GetComponent<Collided>();
            }
            else
            {
                otherleg = stick.muscleR.bone.gameObject.GetComponent<Collided>();
            }
        }
        else if (HasCollided && otherleg.HasCollided)
        {
            stick.HasCollided = true;
        }
        else
        {
            stick.HasCollided = false;
        }
    }

    void OnCollisionEnter2D (Collision2D coll) {
        if(otherleg != null)
        {
            if(coll.gameObject.tag == "World" || coll.gameObject.tag == otherleg.gameObject.tag)
            {
                HasCollided = true;
            }
        }
    }
    void OnCollisionStay2d (Collision2D coll) {
        if(coll.gameObject.tag == "World" || coll.gameObject.tag == otherleg.gameObject.tag)
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