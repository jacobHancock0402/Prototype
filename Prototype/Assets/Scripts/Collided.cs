using UnityEngine;

public class Collided : MonoBehaviour {
    public Stickman stick;
    public bool HasCollidedJump;
    public bool HasCollidedWalk;
    public Collided otherleg;
    public AudioSource Audio;
    public bool LastHasCollidedWalk;
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
        else
        {
            if(HasCollidedWalk && !LastHasCollidedWalk)
            {
                Audio.PlayOneShot(Audio.clip);
            }
            if (HasCollidedWalk && otherleg.HasCollidedWalk)
            {
                stick.HasCollidedWalk = true;
            }
            else
            {
                stick.HasCollidedWalk = false;
            }
            if (HasCollidedJump && otherleg.HasCollidedJump)
            {
                stick.HasCollidedJump = true;
                stick.HasCollidedWalk = true;
            }
            else
            {
                stick.HasCollidedJump = false;
            }
            LastHasCollidedWalk = HasCollidedWalk;
    }
    }

    void OnCollisionEnter2D (Collision2D coll) {
        if(otherleg != null)
        {
            if(coll.gameObject.tag == "World" || coll.gameObject.tag == "Metallic")
            {
                if(gameObject.tag == "rFoot")
                {
                    Vector3 direction = coll.GetContact(0).normal;
                    float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg; 
                    stick.rLegRestRotation = angle;
                     //stick.WalkRightVector = -Vector2.Perpendicular(direction) * 1000;
                    //stick.WalkLeftVector = Vector2.Perpendicular(direction) * 1000;
                }
                else if(gameObject.tag == "lFoot")
                {
                    Vector3 direction = coll.GetContact(0).normal;
                    float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg; 
                    stick.lLegRestRotation = angle;
                    //stick.WalkRightVector = -Vector2.Perpendicular(direction) * 1000;
                    //stick.WalkLeftVector = Vector2.Perpendicular(direction) * 1000;
                }
                HasCollidedJump = true;
                HasCollidedWalk = true;
                //Audio.Play(0);
            }
            else if(coll.gameObject.tag == otherleg.gameObject.tag)
            {
                HasCollidedWalk = true;
            }
        }
    }
    void OnCollisionStay2d (Collision2D coll) {
        if(coll.gameObject.tag == "World" || coll.gameObject.tag == "Metallic")
        {
            HasCollidedJump = true;
            HasCollidedWalk = true;
        }
        else if(coll.gameObject.tag == otherleg.gameObject.tag)
        {
            HasCollidedWalk = true;
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "World" || coll.gameObject.tag == "Metallic")
        {
            HasCollidedJump = false;
            HasCollidedWalk = false;
            stick.AirTime = Time.time;
        }
        else if(coll.gameObject.tag == otherleg.gameObject.tag)
        {
            HasCollidedWalk = false;
        }
    }
}