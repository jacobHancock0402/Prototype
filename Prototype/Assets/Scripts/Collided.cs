using UnityEngine;

public class Collided : MonoBehaviour {
    public Stickman stick;
    public bool HasCollidedJump;
    public bool HasCollidedWalk;
    public Collided otherleg;
    public AudioSource Audio;
    public float calledAnAngle = 0f;
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
            if(HasCollidedWalk)
            {
                if(stick.oneLegHasCollided)
                {
                    stick.LastFrameHasCollidedWalk = true;
                }
                else
                {
                    stick.LastFrameHasCollidedWalk = false;
                }
                stick.oneLegHasCollided = true;
            }
            else if(!HasCollidedWalk && !otherleg.HasCollidedWalk)
            {
                stick.oneLegHasCollided = false;
            }
            if(HasCollidedWalk && !LastHasCollidedWalk)
            {
                Audio.PlayOneShot(Audio.clip);
            }
            if (HasCollidedWalk && otherleg.HasCollidedWalk)
            {
                if(stick.HasCollidedWalk)
                {
                    stick.LastFrameHasCollidedWalk = true;
                }
                else
                {
                    stick.LastFrameHasCollidedWalk = false;
                }
                stick.HasCollidedWalk = true;
            }
            else
            {
                stick.HasCollidedWalk = false;
            }
            if(!HasCollidedWalk && !otherleg.HasCollidedWalk)
            {
                stick.freefall = true;
            }
            else
            {
                stick.freefall = false;
            }
            if (HasCollidedJump && otherleg.HasCollidedJump)
            {
                if(stick.HasCollidedWalk)
                {
                    stick.LastFrameHasCollidedWalk = true;
                }
                else
                {
                    stick.LastFrameHasCollidedWalk = false;
                }
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
            Vector3 direct = coll.GetContact(0).normal;
            calledAnAngle = Mathf.Atan2(direct.x, direct.y) * Mathf.Rad2Deg; 
            if(coll.gameObject.tag == "World" || coll.gameObject.tag == "Metallic" || coll.gameObject.tag == "Incline")
            {
                stick.flying = false;
                if(gameObject.tag == "rFoot")
                {
                    Vector3 direction = coll.GetContact(0).normal;
                    float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg; 
                    if(Mathf.Abs(angle) < 80)
                    {
                        if(Mathf.Abs(angle) < 60)
                        {
                            HasCollidedWalk = true;
                        }
                        if(!stick.crouching && !stick.proning)
                        {
                            stick.muscleR.restRotation = Mathf.Round(-angle);
                            stick.body_muscle.restRotation = Mathf.Round(-angle);
                        }
                    }
                     //stick.WalkRightVector = -Vector2.Perpendicular(direction) * 1000;
                    //stick.WalkLeftVector = Vector2.Perpendicular(direction) * 1000;
                }
                else if(gameObject.tag == "lFoot")
                {
                    Vector3 direction = coll.GetContact(0).normal;
                    float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg; 
                    if(Mathf.Abs(angle) < 80)
                    {
                        if(Mathf.Abs(angle) < 60)
                        {
                            HasCollidedWalk = true;
                        }
                        
                        if(!stick.crouching && !stick.proning)
                        {
                            stick.muscleL.restRotation = Mathf.Round(-angle);
                            stick.body_muscle.restRotation = Mathf.Round(-angle);
                        }
                    }
                    //stick.WalkRightVector = -Vector2.Perpendicular(direction) * 1000;
                    //stick.WalkLeftVector = Vector2.Perpendicular(direction) * 1000;
                }
                HasCollidedJump = true;
                //Audio.Play(0);
            }
            else if(coll.gameObject.tag == otherleg.gameObject.tag)
            {
                //HasCollidedWalk = true;
            }
        }
    }
    void OnCollisionStay2d (Collision2D coll) {
        if(coll.gameObject.tag == "World" || coll.gameObject.tag == "Metallic" || coll.gameObject.tag == "Incline")
        {
            Vector3 direction = coll.GetContact(0).normal;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            if(angle < 60)
            {
                HasCollidedWalk = true;
            } 
            HasCollidedJump = true;
        }
        else if(coll.gameObject.tag == otherleg.gameObject.tag)
        {
            //HasCollidedWalk = true;
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "World" || coll.gameObject.tag == "Metallic" || coll.gameObject.tag == "Incline")
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