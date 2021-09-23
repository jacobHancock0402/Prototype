    
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
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
                if (Stick.grabbingR && (coll.gameObject.tag == "World" || coll.gameObject.tag == "Gun" || coll.gameObject.tag == "Weapon" || Stick.isGun(coll.gameObject)) && (!Stick.gunSearch || (coll.gameObject.tag == Stick.gunSearchTag)))
                {
                    Stick.grabbingR = false;
                    //gameObject.transform.rotation = Quaternion.Euler(0,0, -720);
                    // no idea why not following anymore
                    Debug.LogError("suckmyfatdick" + coll.gameObject.tag);
                    if(coll.gameObject.tag == "Gun" || Stick.isGun(coll.gameObject))
                    {
                        //FollowPos script = coll.gameObject.AddComponent(typeof(FollowPos)) as FollowPos;
                        //script.target = gameObject;
                        Stick.gunPickUpSources[coll.gameObject.tag].Play();
                        coll.gameObject.transform.position = gameObject.transform.GetChild(0).position; 
                        shooting shoot = coll.gameObject.GetComponent<shooting>();
                        shoot.stick = Stick;
                        coll.gameObject.transform.SetParent(gameObject.transform);
                        NoCollision nocoll = coll.gameObject.AddComponent(typeof(NoCollision)) as NoCollision;
                        coll.gameObject.GetComponent<shooting>().grabbed = true;
                        //coll.gameObject.transform.rotation = Quaternion.Euler(0,0, 90);
                        if(Stick.isGun(coll.gameObject))
                        {
                            Stick.TwoHanding = true;
                            Debug.LogError("sickonanigga" + coll.gameObject.tag);
                            Stick.disableR = false;
                            Stick.disableL = false;
                            Stick.NowGrabbingR = true;
                            Stick.NowGrabbingL = true;
                            Stick.gunInL = true;
                            Stick.gunInR = true;
                            FixedJoint2D joint1 = Stick.rHand.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
                            FixedJoint2D joint2 = Stick.lHand.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
                            joint1.autoConfigureConnectedAnchor = false;
                            joint2.autoConfigureConnectedAnchor = false;
                            joint1.connectedAnchor = shoot.frontGrip;
                            joint2.connectedAnchor = shoot.backGrip;
                            Rigidbody2D rbBody = coll.gameObject.transform.GetComponent<Rigidbody2D>(); 
                            joint1.connectedBody = rbBody;
                            joint2.connectedBody = rbBody;
                            _Muscle muscle = new _Muscle();
                            muscle.bone = rbBody;
                            muscle.force = 100;
                            // picks up gun with one hand then freezes idk what's happening, but should solve easy with debugger
                            // do github as well
                            Array.Resize(ref Stick.muscles, Stick.muscles.Length + 1);
                            Stick.muscles[Stick.muscles.Length - 1] = muscle;
                            Stick.gunMuscle = muscle;
                            //rbBody.simulated = false;
                        }
                        //coll.gameObject.transform.rotation = Quaternion.Euler(0,0, -180);

                    }
                    // else if(coll.gameObject.tag == "Shotgun")
                    // {

                    // }
                    else
                    {
                        //coll.gameObject.transform.SetParent(gameObject.transform, true);
                    }
                    Collider2D collider = coll.gameObject.GetComponent<Collider2D>();
                    // might have to add value on + change wether adding or minus depending on direction the arm is in // add a bit of y and x depending on angle like multiply init
                    //coll.gameObject.transform.position = new Vector3((gameObject.transform.position.x + Mathf.Abs(collider.bounds.max[0] - collider.bounds.center[0])), gameObject.transform.position.y, gameObject.transform.position.z);
                    // if(Stick.isGun(coll.gameObject))
                    // {
                    //     Destroy(coll.gameObject.GetComponent<Rigidbody2D>());
                    // }
                    Stick.NowGrabbingR = true;
                    Stick.rWeapon = coll.gameObject;
                }

            if ((coll.gameObject.tag == "World" || coll.gameObject.tag == "Gun" || coll.gameObject.tag == "Weapon" || coll.gameObject.tag == "Rope" || coll.gameObject.tag == "Metallic" || Stick.isGun(coll.gameObject) )&& Stick.holdingR && !Stick.PreHoldingR)
            {
                Rigidbody2D connectedBody = coll.gameObject.GetComponent<Rigidbody2D>();
                Stick.holdingR = false;
                if(coll.gameObject.transform.root.tag == "AI")
                {
                    coll.gameObject.transform.root.gameObject.GetComponent<Stickman>().grabbed = true;
                    Stick.PlayRandomClip(Stick.GrabSources, coll.gameObject.transform.position);
                    Stick.grabbing = true;
                    Stick.grabbingTimer = 0f;
                    Stick.SectionGrabTimer = 0f;
                }
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
            if ((coll.gameObject.tag == "World" || coll.gameObject.tag == "Gun" || coll.gameObject.tag == "Weapon" || coll.gameObject.tag == "Rope" || coll.gameObject.tag == "Metallic" || Stick.isGun(coll.gameObject) || (coll.gameObject.transform.root.tag == "AI" && !Stick.grabbing)) && Stick.holdingL && !Stick.PreHoldingL)
        {
            Stick.holdingL = false;
            Rigidbody2D connectedBody = coll.gameObject.GetComponent<Rigidbody2D>();
            if(coll.gameObject.transform.root.tag == "AI")
            {
                coll.gameObject.transform.root.gameObject.GetComponent<Stickman>().grabbed = true;
                Stick.PlayRandomClip(Stick.GrabSources, coll.gameObject.transform.position);
                Stick.grabbing = true;
                Stick.grabbingTimer = 0f;
                Stick.SectionGrabTimer = 0f;
            }
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
        // more sound effects, like when objects collide, check for these collision with relV
        // arms look weird af
        if ((coll.gameObject.tag == "World" || coll.gameObject.tag == "Gun" || coll.gameObject.tag == "Weapon" || Stick.isGun(coll.gameObject)) && Stick.grabbingL && (!Stick.gunSearch || (coll.gameObject.tag == Stick.gunSearchTag))) {
            Stick.grabbingL = false;
            //gameObject.transform.rotation = Quaternion.Euler(0,0, -720);
            if(coll.gameObject.tag == "Gun" || Stick.isGun(coll.gameObject))
            {
                // FollowPos script = coll.gameObject.AddComponent(typeof(FollowPos)) as FollowPos;
                // script.target = gameObject;
                // shooting dec right now, but prob need new sound
                // do three fire modes, single burst auto, the particle effect spaz's out because of scale
                Stick.gunPickUpSources[coll.gameObject.tag].Play();
                shooting shoot = coll.gameObject.GetComponent<shooting>();
                shoot.stick = Stick;
                coll.gameObject.transform.position = gameObject.transform.GetChild(0).position;
                coll.gameObject.transform.SetParent(gameObject.transform);
                NoCollision nocoll = coll.gameObject.AddComponent(typeof(NoCollision)) as NoCollision;
                coll.gameObject.GetComponent<shooting>().grabbed = true;
                if(Stick.isGun(coll.gameObject))
                {
                    Stick.TwoHanding = true;
                    Debug.LogError("sickonanigga" + coll.gameObject.tag);
                    Stick.disableR = false;
                    Stick.disableL = false;
                    Stick.gunInL = true;
                    Stick.gunInR = true;
                    Stick.NowGrabbingR = true;
                    Stick.NowGrabbingL = true;
                    FixedJoint2D joint1 = Stick.rHand.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
                    FixedJoint2D joint2 = Stick.lHand.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
                    joint1.autoConfigureConnectedAnchor = false;
                    joint2.autoConfigureConnectedAnchor = false;
                    joint1.connectedAnchor = shoot.frontGrip;
                    joint2.connectedAnchor = shoot.backGrip;
                    Rigidbody2D rbBody = coll.gameObject.transform.GetComponent<Rigidbody2D>(); 
                    joint1.connectedBody = rbBody;
                    joint2.connectedBody = rbBody;
                    // if(coll.gameObject.tag == "AR")
                    // {
                    //     FixedJoint2D joint3 = coll.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
                    //     joint3.autoConfigureConnectedAnchor = false;
                    //     joint3.connectedAnchor = shoot.shoulderPos;
                    //     rbBody = Stick.lArm.GetComponent<Rigidbody2D>(); 
                    //     joint3.connectedBody = rbBody;
                        
                    // }
                    _Muscle muscle = new _Muscle();
                    muscle.bone = rbBody;
                    muscle.force = 100;
                    Array.Resize(ref Stick.muscles, Stick.muscles.Length + 1);
                    Stick.muscles[Stick.muscles.Length - 1] = muscle;
                    Stick.gunMuscle = muscle;
                    //rbBody.simulated = false;
                }
            }
            else
            {
                //coll.gameObject.transform.SetParent(gameObject.transform, true);
            }
            Collider2D collider = coll.gameObject.GetComponent<Collider2D>();
            // might have to add value on + change wether adding or minus depending on direction the arm is in // add a bit of y and x depending on angle like multiply init
            coll.gameObject.transform.position = new Vector3((gameObject.transform.position.x + Mathf.Abs(collider.bounds.max[0] - collider.bounds.center[0])), gameObject.transform.position.y, gameObject.transform.position.z);
            //Destroy(coll.gameObject.GetComponent<Rigidbody2D>());
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