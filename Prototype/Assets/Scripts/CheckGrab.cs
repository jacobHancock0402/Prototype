    
using UnityEngine;
public class CheckGrab : MonoBehaviour {
    public Stickman Stick;
    //public Gun Gun;
    public float deltaleg_mass = 0.1f;
    public float deltafoot_mass = 0.02f;
    public float deltabody_mass = 0.01f;
    public float deltaarm_mass = 7.5f;
    public float deltaoffarm_mass = 0;
    public float JumpScalar = 0.1f;
    public bool active = true;
    void FormatColl(Collision2D coll)
    {
        if(gameObject.tag == "rArm")
        {
                if (Stick.grabbingR && (coll.gameObject.tag == "World" || coll.gameObject.tag == "Gun" || coll.gameObject.tag == "Weapon"))
                {
                    Stick.grabbingR = false;
                    gameObject.transform.rotation = Quaternion.Euler(0,0, -720);
                    // no idea why not following anymore
                    if(coll.gameObject.tag == "Gun")
                    {
                        FollowPos script = coll.gameObject.AddComponent(typeof(FollowPos)) as FollowPos;
                        script.target = gameObject;
                        NoCollision nocoll = coll.gameObject.AddComponent(typeof(NoCollision)) as NoCollision;
                        coll.gameObject.GetComponent<shooting>().grabbed = true;
                    }
                    else
                    {
                        coll.gameObject.transform.SetParent(gameObject.transform, true);
                    }
                    Collider2D collider = coll.gameObject.GetComponent<Collider2D>();
                    // might have to add value on + change wether adding or minus depending on direction the arm is in // add a bit of y and x depending on angle like multiply init
                    //coll.gameObject.transform.position = new Vector3((gameObject.transform.position.x + Mathf.Abs(collider.bounds.max[0] - collider.bounds.center[0])), gameObject.transform.position.y, gameObject.transform.position.z);
                    Destroy(coll.gameObject.GetComponent<Rigidbody2D>());
                    Stick.NowGrabbingR = true;
                    Stick.rWeapon = coll.gameObject;
                }

            if ((coll.gameObject.tag == "World" || coll.gameObject.tag == "Gun" || coll.gameObject.tag == "Weapon" || coll.gameObject.tag == "Rope" || coll.gameObject.tag == "Metallic")&& Stick.holdingR && !Stick.PreHoldingR)
            {
                Rigidbody2D connectedBody = coll.gameObject.GetComponent<Rigidbody2D>();
                Stick.holdingR = false;
                if (coll.gameObject.tag == "Rope")
                {
                    DistanceJoint2D Hinge = gameObject.AddComponent(typeof(DistanceJoint2D )) as DistanceJoint2D ;
                    Hinge.connectedBody = connectedBody;
                    Stick.swingingR = true;
                }

                else
                {
                    HingeJoint2D Hinge = gameObject.AddComponent(typeof(HingeJoint2D )) as HingeJoint2D ;
                    Hinge.connectedBody = connectedBody;
                }
                //SpringJoint2D Distance = Stick.Body.AddComponent(typeof(SpringJoint2D)) as SpringJoint2D;
                //Distance.connectedBody = gameObject.GetComponent<Rigidbody2D>();
                //Distance.autoConfigureDistance = false;
                //Distance.dampingRatio = 1;
                //Distance.frequency = 0;
                //Gun.DontAim = true;
                gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
                Stick.NowHoldingR = true;
                Stick.CopiedWalkLeftVector = Stick.WalkLeftVector;
                Stick.CopiedWalkRightVector = Stick.WalkRightVector;
                ScaleRightMasses();
                DistanceJoint2D Distance = Stick.Body.AddComponent(typeof(DistanceJoint2D)) as DistanceJoint2D;
                Distance.connectedBody = gameObject.GetComponent<Rigidbody2D>();
                Distance.autoConfigureDistance = false;
                Distance.maxDistanceOnly = true;
                Distance.enableCollision = true;
                Stick.currentPivotArm = "rArm";

                
                }
            }
        else
        {
            if ((coll.gameObject.tag == "World" || coll.gameObject.tag == "Gun" || coll.gameObject.tag == "Weapon" || coll.gameObject.tag == "Rope" || coll.gameObject.tag == "Metallic") && Stick.holdingL && !Stick.PreHoldingL)
        {
            Stick.holdingL = false;
            Rigidbody2D connectedBody = coll.gameObject.GetComponent<Rigidbody2D>();
            if (coll.gameObject.tag == "Rope")
            {
                DistanceJoint2D Hinge = gameObject.AddComponent(typeof(DistanceJoint2D )) as DistanceJoint2D ;
                Hinge.connectedBody = connectedBody;
                Stick.swingingL = true;
            }

            else
            {
                HingeJoint2D Hinge = gameObject.AddComponent(typeof(HingeJoint2D )) as HingeJoint2D ;
                Hinge.connectedBody = connectedBody;
            }
            Stick.NowHoldingL = true;
            Stick.CopiedWalkLeftVector = Stick.WalkLeftVector;
            Stick.CopiedWalkRightVector = Stick.WalkRightVector;
            ScaleLeftMasses();
            DistanceJoint2D Distance = Stick.Body.AddComponent(typeof(DistanceJoint2D)) as DistanceJoint2D;
            Distance.connectedBody = gameObject.GetComponent<Rigidbody2D>();
            Distance.autoConfigureDistance = false;
            Distance.maxDistanceOnly = true;
            Distance.enableCollision = true;
            Stick.currentPivotArm = "lArm";
            
            
        }

        if ((coll.gameObject.tag == "World" || coll.gameObject.tag == "Gun" || coll.gameObject.tag == "Weapon") && Stick.grabbingL) {
            Stick.grabbingL = false;
            gameObject.transform.rotation = Quaternion.Euler(0,0, -720);
            if(coll.gameObject.tag == "Gun")
            {
                FollowPos script = coll.gameObject.AddComponent(typeof(FollowPos)) as FollowPos;
                script.target = gameObject;
                NoCollision nocoll = coll.gameObject.AddComponent(typeof(NoCollision)) as NoCollision;
                coll.gameObject.GetComponent<shooting>().grabbed = true;
            }
            else
            {
                coll.gameObject.transform.SetParent(gameObject.transform, true);
            }
            Collider2D collider = coll.gameObject.GetComponent<Collider2D>();
            // might have to add value on + change wether adding or minus depending on direction the arm is in // add a bit of y and x depending on angle like multiply init
            coll.gameObject.transform.position = new Vector3((gameObject.transform.position.x + Mathf.Abs(collider.bounds.max[0] - collider.bounds.center[0])), gameObject.transform.position.y, gameObject.transform.position.z);
            Destroy(coll.gameObject.GetComponent<Rigidbody2D>());
            Stick.NowGrabbingL = true;
            Stick.lWeapon = coll.gameObject;
            //Stick.grabbingL = false;
            //gameObject.transform.rotation = Quaternion.Euler(0,0, 0);
            //coll.gameObject.transform.SetParent(gameObject.transform, true);
            //coll.gameObject.transform.position = new Vector3(gameObject.transform.position.x - 2, gameObject.transform.position.y, gameObject.transform.position.z);
            //Destroy(coll.gameObject.GetComponent<Rigidbody2D>());
            //Stick.NowGrabbingL = true;
        }

        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(active)
        {
            FormatColl(coll);
        }
    }
    void OnCollisionStay2D(Collision2D coll)
    {   if(active)
        {
            FormatColl(coll);
        }
    }
    public void ScaleRightMasses()
    {
        Stick.JumpVector = Stick.JumpVector * JumpScalar;
                    foreach(_Muscle muscle in Stick.muscles)
                    {
                        if(muscle.bone.gameObject.tag == "rArm")
                        {
                            muscle.bone.mass = deltaarm_mass*(1/Stick.arm_length);
                            Stick.rArmRigid.mass = deltaarm_mass*(1/Stick.arm_length);
                            muscle.bone.drag = 100f;
                            muscle.bone.gravityScale = 0f;
                        }
                    else
                    {
                    //         if(muscle.bone.gameObject.tag == "rLeg" || muscle.bone.gameObject.tag == "lLeg")
                    //         {
                    //             Stick.oldLegMass = muscle.bone.mass;
                    //             muscle.bone.mass = deltaleg_mass * Stick.leg_mass;
                    //             Stick.newLegMass = muscle.bone.mass;
                                
                    //         }
                    //         else if(muscle.bone.gameObject.tag == "Body" || muscle.bone.gameObject.tag == "Head")
                    //         {
                    //             muscle.bone.mass = deltabody_mass * (Stick.body_mass);
                    //         }
                            if(muscle.bone.gameObject.tag == "lArm" && !Stick.NowHoldingL)
                            {
                                muscle.bone.mass = 0.1f;
                                muscle.bone.drag = 0f;
                            }
                            // should prob try do the above
                    //         else if(muscle.bone.gameObject.tag == "rFoot" || muscle.bone.gameObject.tag == "lFoot")
                    //         {
                    //             muscle.bone.mass = deltafoot_mass * (Stick.foot_mass);
                    //         }
                    //     }
                    // }
                    }
                    }
                    //Stick.WalkLeftVector = new Vector2(Stick.WalkLeftVector.x * (deltaleg_mass), Stick.WalkLeftVector.y * (deltaleg_mass) );
                    //Stick.WalkRightVector = new Vector2(Stick.WalkRightVector.x * (deltaleg_mass), Stick.WalkRightVector.y * (deltaleg_mass) );
    }
    public void ScaleLeftMasses()
    {
        Stick.JumpVector = Stick.JumpVector * JumpScalar;
                foreach(_Muscle muscle in Stick.muscles)
                {
                    // this is decent balance between strength to hold up
                    // and light enought to not collapse
                    // gonna have try to hit a sweet spot, as right now slightly too heavy
                    // and prob will snap at high speeds, could try decrease leg mass
                    // also have to reset all values after
                    if(muscle.bone.gameObject.tag == "lArm")
                    {
                        muscle.bone.mass = deltaarm_mass*(1/Stick.arm_length);
                        Stick.lArmRigid.mass = deltaarm_mass*(1/Stick.arm_length);
                        muscle.bone.drag = 100f;
                        muscle.bone.gravityScale = 0f;
                    }
                    else
                    {
                       // if(muscle.bone.gameObject.tag == "rLeg" || muscle.bone.gameObject.tag == "lLeg")
                       // {
                            //Stick.oldLegMass = muscle.bone.mass;
                            //muscle.bone.mass = deltaleg_mass * Stick.leg_mass;
                            //Stick.newLegMass = muscle.bone.mass;
                        //}
                       // else if(muscle.bone.gameObject.tag == "Body" || muscle.bone.gameObject.tag == "Head")
                        //{
                            // delta body mass covers both head and body? problematic? i guess keep in ratio tho
                            //muscle.bone.mass = deltabody_mass * (Stick.body_mass);
                        //}
                       // else if(muscle.bone.gameObject.tag != "rArm")
                       // {
                            //muscle.bone.mass = deltafoot_mass * (Stick.foot_mass);
                        //}
                        if(muscle.bone.gameObject.tag == "rArm" && !Stick.NowHoldingR)
                        {
                            muscle.bone.mass = 0.1f;
                            muscle.bone.drag = 0f;
                        }
                    }
                }
                //Stick.WalkLeftVector = new Vector2(Stick.WalkLeftVector.x * (deltaleg_mass), Stick.WalkLeftVector.y * (deltaleg_mass) );
              //Stick.WalkRightVector = new Vector2(Stick.WalkRightVector.x * (deltaleg_mass), Stick.WalkRightVector.y * (deltaleg_mass) );
    }
}