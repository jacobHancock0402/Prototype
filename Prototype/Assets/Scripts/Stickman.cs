

using UnityEngine;
using System.Collections;
public class Stickman: MonoBehaviour
{
 
    public _Muscle[] muscles;
    public bool wasCrouched;
    public GameObject lWeapon;
    public GameObject rWeapon;
    public _Muscle body_muscle;
    public bool crouching;
    // this might cause problems if die midair, like you can't jump after spawn. = make reset
    public bool jumping = false;
    public float head_mass;
    public float body_mass;
    public bool CrouchAniDone;
    public _Muscle[] upperLeg;
    public float leg_mass;
    public float foot_mass;
    // jump vector aight, just fix walking rhythm and when you can actually jump , right now infintiely
    public bool Right;
    public int counter;
    public bool Left;
<<<<<<< HEAD
    public float arm_length;
=======
    public int arm_length;
>>>>>>> 242da17ffddb7cca4e9364343fad6ca7e8037683
    public float leg_changex = 5f;
    public float leg_changey = 5f;
    public bool stretch = false;
    public bool flying = false;
    public Rigidbody2D rRigid;
    public Rigidbody2D[] rbRIGHT;
    public GameObject rHand;
    public GameObject lHand;
    public GameObject bulletPrefab;
    public bool collided = false;
    public Rigidbody2D[] rbLEFT;
    public Rigidbody2D[] rbARIGHT;
    public Rigidbody2D[] rbALEFT;
    public Rigidbody2D rbLLeg;
    public Rigidbody2D rbRLeg;
    public Rigidbody2D rbHead;
    public RaycastHit2D rayR;
    public RaycastHit2D rayL;
    public GameObject rArm;
    public GameObject lArm;
    public Vector2 JumpVector;
    public Vector2 CopiedWalkRightVector = new Vector2(40,80);
    public Vector2 CopiedWalkLeftVector = new Vector2(-40,80);
    public float newLegMass;
    public string stepL = "left";
    public string Direction = "Right";
    public bool Sliding;
    public float time = 0;
    public GameObject Body;
    public float delay;
    public bool disable = true;
    public bool HasCollided;
    public string stepR = "right";
 
    public Vector2 WalkRightVector;
    public Vector2 WalkLeftVector;
 
    private float MoveDelayPointer;
    public float MoveDelay;
    public static bool shooting;
    public bool grabbingL;
    public bool grabbingR;
    public Rigidbody2D lRigid;
    public _Muscle[] legs;
    public int count;
    public bool disableR;
    public bool disableL;
    public float ti = 0;
    public bool holdingR;
    public bool holdingL;
    public bool posturingL;
    public bool posturingR;
    public bool NowHoldingL;
    public bool NowGrabbingL;
    public bool NowGrabbingR;
    public bool NowHoldingR;
    public bool NowPosturingL;
    public bool NowPosturingR;
    public GameObject rLeg;
    public GameObject lLeg;
    public GameObject arm;
    public bool pivoting;
    public string way;
    public Rigidbody2D rArmRigid;
    public Rigidbody2D lArmRigid;
    public string dir;
    public bool pivotedR = false;
    public bool pivotedL = false;
    public float oldLegMass;
    public Rigidbody2D head;
    public float tim = 0;
    public bool found1 = false;
    public bool found2 = false;
    public _Muscle muscleL;
    public Vector2 ogJumpVector;
    public _Muscle muscleR;
 
 
    // Update is called once per frame

    // the movement from gun is caused by collider but , without it, arm goes weird
    // and start going inwards to body
    // this is caused by the rigidbody trying to push back
    // minimising mass seems to get rid of unnauathorised movement but still jank
    // gravity scale = 0 means jank is minimal so it's clear rigid body is at fault
    private void Start()
    {
        lRigid = lArm.GetComponent<Rigidbody2D>();
            foreach (_Muscle muscle in muscles)
            {   
                if(muscle.bone == rbLLeg || muscle.bone == rbRLeg )
                {
                    legs[legs.Length - 1 - counter] = muscle;
                    counter = 1;
                }
                else if(muscle.bone.tag == "Body")
                {
                    body_muscle = muscle;
                }
            }
            ogJumpVector = JumpVector;

        ti = Time.time;
    }
    // add animation for backflip and frontflip by rotating 360 over set time
    private void Update()
    {
        int q = 0;
        foreach (_Muscle muscle in muscles)
        {   
            
            if (muscle.bone)
            {

                if (collided == true && flying)
                {
                    muscle.restRotation = 0;
                }
                // change this to shooting arm is using 2hands
                if (muscle.bone.gameObject.tag == "rArm")
                {
                        //muscle.force = 15;
                        if(disableR == false)
                        {
                            muscle.force = 100;
                            muscle.bone.gravityScale = 0;
                            muscle.ActivateMuscle();
                        }
                        else
                        {
                            muscle.bone.gravityScale = 1;
                            muscle.force = 0;
                        }
                        //muscle.force = 0;
                        
                }
                // could control arms always but give them like an animation or rules
                else if (muscle.bone.gameObject.tag == "lArm")
<<<<<<< HEAD
=======
                {
                    if(disableL == false)
                    {
                        muscle.force = 100;
                        muscle.bone.gravityScale = 0;
                        muscle.ActivateMuscle();
                    }
                    else
                    {
                        muscle.force = 0;
                        muscle.bone.gravityScale = 1;
                    }
                }
                else
                {
                    muscle.ActivateMuscle();
                }
                if (muscle.bone.gameObject.tag == "rLeg" && muscles[q + 1].bone.gameObject.tag == "lLeg")
>>>>>>> 242da17ffddb7cca4e9364343fad6ca7e8037683
                {
                    if(disableL == false)
                    {
                        muscle.force = 100;
                        muscle.bone.gravityScale = 0;
                        muscle.ActivateMuscle();
                    }
                    else
                    {
                        muscle.force = 0;
                        muscle.bone.gravityScale = 1;
                    }
                }
                else
                {
                    muscle.ActivateMuscle();
                }
                q++;
            }
        }

        // legitimately no clue why right side doesn't work
        // seems to go through function and everything, but like resets quickly

 
        if (Input.GetKeyDown(KeyCode.D))
        {
            Right = true;
            Direction = "Right";
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            if(crouching)
            {
                wasCrouched = true;
                CrouchAni();
                crouching = false;
            }
            else
            {
                wasCrouched = false;
                CrouchAniDone = false;
                CrouchAni();
                crouching = true;
            }
        }
        if(crouching)
        {
            if((Direction == "Left" && body_muscle.restRotation == -90) || (Direction == "Right" && body_muscle.restRotation == 90))
            {
                CrouchAni();
            }
        }
        if(Input.GetKeyUp(KeyCode.D) && Right)
        {
            Right = false;
            GetUpAni();
        }
        if(Input.GetKeyUp(KeyCode.A) && Left)
        {
            Left = false;
            GetUpAni();
        }
        if(rArm.transform.GetChild(0).gameObject.tag == "Gun" || grabbingR || holdingR || NowGrabbingR)
        {
            disableR = false;
        }
        else
        {
            disableR = true;
        }
        if(lArm.transform.GetChild(0).gameObject.tag == "Gun" || grabbingL || holdingL || NowGrabbingL)
        {
            disableL = false;
        }
        else
        {
            disableL = true;
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            //Jump();
            //jumping = true;

            //if (NowPosturingL)
            //{
                //StopHold(lLeg, "LeftL");
            //}
            //if (NowPosturingR)
            //{
                //StopHold(rLeg, "RightL");
            //}
        //}
        if (Input.GetMouseButtonDown(0))
        {
            shooting = true;
        }

        if (Input.GetKeyDown(KeyCode.R)){
            GetUpAni();
            flying = false;
        }
        if(Input.GetKeyDown(KeyCode.Space) && HasCollided)
        {
            //foreach(_Muscle muscle in upperLeg)
            //{
                //muscle.restRotation = 90;
            //}
            Invoke("Jump", 0f);
        }
        // legit no idea why he keeps falling down
        // movement of arm is great, but character is like dead with it
        // almost like there is no activation of muscles but console log proves otherwise
        // seems like maybe i've hardcoded values for muscles or something?

        //doesn't work, not sure how to keep in , might just delete as a bit useless anyway
        if(Input.GetKeyDown(KeyCode.Q) && flying == false)
        {   if (Direction == "Right")
            {
                Frontflip();
            }
            if (Direction == "Left")
            {
                Backflip();
            }
            flying = true;
        }
        if(Input.GetKeyDown(KeyCode.E)&& flying == false)
        {
            if (Direction == "Right")
            {
                Backflip();
            }
            if (Direction == "Left")
            {
                Frontflip();
            }
            flying = true;
        }
        if(Input.GetKeyDown(KeyCode.F) && flying == false)
        {
            Fly();
            flying = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            SlowDown();
        }

        if (Time.timeScale < 1)
        {
            if (Time.time - time > 0.5)
            {
                Time.timeScale = 1f;
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (NowGrabbingL)
            {
                DropGun(lArm);
                NowGrabbingL = false;
            }
            else if (NowHoldingL)
            {
                StopHold(lArm, "LeftA");
            }
            // maybe want to move with both hands
            else
            {
                if(!grabbingL)
                {
                    grabbingL = true;
                }
                else
                {
                    grabbingL = false;
                }
                holdingL = false;
                holdingR = false;
                grabbingR = false;
                posturingL = false;
                posturingR = false;
            }
        }
        // can't use holdingL because i reset it to false once collides
        if (Input.GetKeyDown(KeyCode.X)) {
            if (NowHoldingL)
            {
                StopHold(lHand, "LeftA");
            }
            else if(NowGrabbingL)
            {
                DropGun(lArm);
                NowGrabbingL = false;
            }
            else
            {
                if(!holdingL)
                {
                    holdingL = true;
                }
                else
                {
                    holdingL = false;
                }
                grabbingL = false;
                grabbingR = false;
                holdingR = false;
                posturingL = false;
                posturingR = false;
            }
        }

        if(NowHoldingL)
        {
            foreach(_Muscle muscle in muscles)
            {
                // this is decent balance between strength to hold up
                // and light enought to not collapse
                // gonna have try to hit a sweet spot, as right now slightly too heavy
                // and prob will snap at high speeds, could try decrease leg mass
                // also have to reset all values after
                if(muscle.bone.gameObject.tag == "lArm")
                {
                    muscle.bone.mass = 10f;
                    lArmRigid.mass = 10f;
                    muscle.bone.gravityScale = 0f;
                }
                else if(muscle.bone.gameObject.tag != "lLeg" && muscle.bone.gameObject.tag != "rLeg" && muscle.bone.gameObject.tag != "rArm" )
                {
                    muscle.bone.mass = 0.1f;
                }
                else if(muscle.bone.gameObject.tag == "rArm")
                {
                    muscle.bone.mass = 0.001f;
                }
            }
        }
        if(NowHoldingR)
        {
            foreach(_Muscle muscle in muscles)
            {
                if(muscle.bone.gameObject.tag == "rArm")
                {
                    muscle.bone.mass = 100;
                }
                else
                {
                    muscle.bone.mass = 0.00001f;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.G) && legs[0].restRotation != 70)
        {
            Invoke("Slide1", 0f);
            Sliding = true;
        }

        if (Input.GetKeyDown(KeyCode.C)) {
            if(NowGrabbingR)
            {
                DropGun(rArm);
                NowGrabbingR = false;
            }
            else if (NowHoldingR) 
            {
                StopHold(rHand, "RightA");
            }
            else
            {
                if(!grabbingR)
                {
                    grabbingR = true;
                }
                else
                {
                    grabbingR = false;
                }
                holdingR = false;
                grabbingL = false;
                holdingL = false;
                posturingL = false;
                posturingR = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {   if(NowHoldingR)
            {
                StopHold(rHand, "RightA");
            }   
            else if(NowGrabbingR)
            {
                DropGun(rArm);
                NowGrabbingR = false;
            } 
            else
            {
                if(!holdingR)
                {
                    holdingR = true;
                }
                else
                {
                    holdingR = false;
                }
                grabbingR = false;
                grabbingL = false;
                holdingL = false;
                posturingL = false;
                posturingR = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if(NowPosturingL)
            {
                StopHold(lLeg, "LeftL");
            }   
            else
            {
                posturingL = true;
                holdingR = false;
                grabbingR = false;
                posturingR = false;
                grabbingL = false;
                holdingL = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (NowPosturingR)
            {
                StopHold(rLeg, "RightL");
            }
            else
            {
                posturingR = true;
                posturingL = false;
                holdingR = false;
                grabbingR = false;
                grabbingL = false;
                holdingL = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            pivoting = true;
            dir = "Up";
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            pivoting = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            pivoting = true;
            dir = "Down";
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            pivoting = false;
        }
        

        if (pivoting)
        {  
            //Body.transform.position = Vector3.MoveTowards(Body.transform.position,rHand.transform.position,0.1f);
            if (NowHoldingL)
            {
                pivotedL = true;
                if (lArm.transform.position.x < Body.transform.position.x)
                {
                    Invoke("pullUp", 0.01f);
                    way = "Left";
                    arm = lArm;
                }

                else if(lArm.transform.position.x > Body.transform.position.x)
                {
                    Invoke("pullUp", 0.01f);
                    way = "Right";
                    arm = lArm;
                }
            }

            else if (NowHoldingR)
            {
                pivotedR = true;
                if (rArm.transform.position.x < Body.transform.position.x)
                {
                    Invoke("pullUp", 0.01f);
                    way = "Left";
                    arm = rArm;
                }

                else if(rArm.transform.position.x > Body.transform.position.x)
                {
                    Invoke("pullUp", 0.01f);
                    arm = rArm;
                    way = "Right";
                }
            }
        }

        if (disableL == false) 
        {
            Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - lArm.transform.position).normalized;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            // angle shouldn't be negative but otherwise it pretty much goes in opposite direction
            foreach(_Muscle muscle in muscles)
            {
                if(muscle.bone.gameObject.tag == "lArm")
                {
                    muscle.restRotation = -angle + 180;
                }
            
            }
            //lArm.transform.eulerAngles = new Vector3(0, 0, -angle + 270);
            //lArmRigid.freezeRotation = true;
        }

        if (disableR == false)
        {
            disable = false;
            Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - lArm.transform.position).normalized;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            // angle shouldn't be negative but otherwise it pretty much goes in opposite direction
            //Debug.Log("ballinlikeim24");
            int j = 0;
            foreach(_Muscle muscle in muscles)
            {
                j++;
                if(muscle.bone.tag == "rArm")
                {
                    //muscle.bone.freezeRotation = true;
                    //muscle.bone.simulated = false; 
                    muscle.restRotation = - angle + 180;
                    //muscle.bone.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -angle + 90); // = new Vector3(muscle.bone.transform.rotation.x, muscle.bone.transform.rotation.y, angle);
                    //Debug.Log(muscle.restRotation);
                    //muscle.bone.freezeRotation = true;
                }
            
            }
            // could remove this call to get gun other places
            Transform transGun = rArm.transform.GetChild(rArm.transform.childCount - 1);
            if(transGun.childCount > 0)
            {
                transGun = transGun.GetChild(0);
            }
            if(transGun.gameObject.tag == "Gun")
            {
                transGun.rotation = Quaternion.Euler(0.0f,0.0f,-angle + 90);
                // arm is perfect now, walking still trip bit
                // now move onto picking stuff up and climbing
            }
            //rArm.transform.eulerAngles = new Vector3(0, 0, -angle + 90);
            //rArmRigid.freezeRotation = true;
        }
        else
        {
            disable = true;
        }

        if(posturingL)
        {
            Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - lArm.transform.position).normalized;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            // angle shouldn't be negative but otherwise it pretty much goes in opposite direction
            lLeg.transform.eulerAngles = new Vector3(0, 0, -angle + 180);
            lLeg.GetComponent<Rigidbody2D>().freezeRotation = true;
        }

        if(posturingR)
        {
            Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - lArm.transform.position).normalized;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            // angle shouldn't be negative but otherwise it pretty much goes in opposite direction
            rLeg.transform.eulerAngles = new Vector3(0, 0, -angle + 180);
            rLeg.GetComponent<Rigidbody2D>().freezeRotation = true;
        }

        if (!posturingL && !NowPosturingL)
        {
            rbLLeg.freezeRotation = false;
        }

        if (!posturingR && !NowPosturingR)
        {
            rbRLeg.freezeRotation = false;
        }
        // freezing rotation of arm stop the sort of bit when it get's stuck
        if(rArm.transform.childCount == 1)
        {
            if (grabbingR || holdingR) 
            {
                DropGun(rArm);
            }

           rArmRigid.freezeRotation = true;
        }

        else if((NowHoldingR  && pivotedR) || NowGrabbingR || grabbingR || holdingR)
        {
            rArmRigid.freezeRotation = true;
        }

        else if(!pivotedR)
        {
            rArmRigid.freezeRotation = false;
        }

        if(lArm.transform.childCount == 1)
        {
            if (grabbingL || holdingL) 
            {
                DropGun(lArm);
            }

            lArmRigid.freezeRotation = true;
        }

        else if((NowHoldingL && pivotedL)  || NowGrabbingL|| grabbingL || holdingL)
        {
            lArmRigid.freezeRotation = true;
        }

        else if(!pivotedL)
        {
            lArmRigid.freezeRotation = false;
        }



 
        if (Input.GetKeyDown(KeyCode.A))
        {
            Left = true;
            Direction = "Left";
        }
 
        if (Input.GetButtonUp("Horizontal"))
        {
            Left = false;
            Right = false;
        }
        int e = 0;
        _Muscle muscl = new _Muscle();
        for(int i=0;i<muscles.Length;i++)
        {
            if (muscles[i].bone.gameObject.tag == "rLeg")
            {
                muscl = muscles[i]; 
                break;
            }
                
        }
        if (!Sliding && Right == true && Left == false  && Time.time - tim > delay && HasCollided)
        {
            //foreach(_Muscle muscle in muscles)
            //{
                //if(muscle.bone.gameObject.tag != "rLeg" && muscle.bone.gameObject.tag != "lLeg")
                //{
                    //muscle.restRotation = 15;
                //}
            //}
            if(HasCollided && stepR == "right")
            {
                Invoke("Step1Right", 0f);
            }
            else if(HasCollided && stepR == "left")
            {
                Invoke("Step2Right", 0f);
            }
        }

        // figure out why no collision script isn't working
        // once sorted, a few tweaks to vectors and ***REMOVED*** should make walking good
        // none of left movement has config
 
        else if (!Sliding && Left == true && Right == false && Time.time - tim > delay)
        {
            //foreach(_Muscle muscle in muscles)
            //{
                //if(muscle.bone.gameObject.tag != "rLeg" && muscle.bone.gameObject.tag != "lLeg")
                //{
                   // muscle.restRotation = -15;
                //}
            //}
            // i should really add in ability to climb up slopes and ***REMOVED*** instead of just jumping
            if(HasCollided && stepL == "left")
            {
                Invoke("Step1Left", 0f);
            }
            else if(HasCollided && stepL == "right")
            {
                Invoke("Step2Left", 0f);
            }
        }
        if(Time.time - tim > 1)
        {
            stepR = "right";
            stepL = "left";
        }
        //else if(!flying && muscles[0].restRotation == 70)
        //{
            //muscles[0].restRotation = 0;
        //}
        //else
        //{
            //foreach(_Muscle muscle in muscles)
            //{
               // muscle.restRotation = 0;
            //}
            //count = 0;
        //}
        Debug.Log("thismadcityirunma***REMOVED***");
        collided = false;
    }
    public void Step1Right()
    {
        int num = 0;
        Debug.Log("***REMOVED***offf");
        if(HasCollided)
        {
            // could experiment with adding forces from different places i.e knee to stop the tripping
            muscleR.bone.AddForce(WalkRightVector, ForceMode2D.Impulse);
        //rbRIGHT[rbRIGHT.Length / 2 ].AddForce(WalkRightVector, ForceMode2D.Impulse);
            //foreach (_Muscle muscle in muscles)
            //{
                //if(muscle.bone.gameObject.tag == "rLeg" && num < 6)
                //{
                    //muscle.restRotation = 90;
                    //num++;
                //}
            //}
            Debug.Log("die");
            tim = Time.time;
            stepR = "left";
        }
        
        Vector2 origin = muscleR.bone.gameObject.transform.position;
        origin.x = origin.x + leg_changex;
        origin.y = origin.y + leg_changey;
        Vector2 dir = new Vector2(origin.x,5f);
        rayR = Physics2D.Raycast(origin,dir,500f);
        Debug.DrawRay(origin,dir);
        if(rayR.collider)
        {
            //Debug.Log(rayR);
            Debug.Log("ray");
            Vector3 direction = (rayR.point - new Vector2(lArm.transform.position.x , lArm.transform.position.y)).normalized;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            foreach (_Muscle muscle in muscles)
            {
                if (muscle.bone.gameObject.tag == "rLeg")
                {
                    //muscle.restRotation = angle;
                }
            }
        }
        //Debug.Log("wow");
    }
 
    public void Step2Right()
    {
        int num = 0;
        if(HasCollided)
        {
        muscleL.bone.AddForce(WalkRightVector, ForceMode2D.Impulse);
            //rbLEFT[rbLEFT.Length / 2 ].AddForce(WalkRightVector * 0.9f, ForceMode2D.Impulse);
        //foreach (_Muscle muscle in muscles)
            //{
                //if(muscle.bone.gameObject.tag == "lLeg" && num < 6)
                //{
                    //muscle.restRotation = 90;
                    //num++;
                //}
            //}
             tim = Time.time;
             stepR = "right";
        }
    }
 
     public void Step1Left()
    {
        if(HasCollided)
        {
            muscleL.bone.AddForce(WalkLeftVector, ForceMode2D.Impulse);
            tim = Time.time;
            stepL = "right";
        }
        //muscleR.bone.AddForce(-WalkLeftVector * 0.5f, ForceMode2D.Impulse);
    }
 
 
    public void Step2Left()
    {
        //muscleL.bone.AddForce(-WalkLeftVector * 0.5f, ForceMode2D.Impulse);
        if(HasCollided)
        {
            muscleR.bone.AddForce(WalkLeftVector * 0.9f, ForceMode2D.Impulse);
            tim = Time.time;
            stepL = "left";
        }
    }

    public void pullUp()
    {
        Debug.Log("hi");
        int number = 0;
        if (way == "Right")
        {
            if(dir == "Up")
            {
                number = -3;
            }
            else
            {
                number = 3;
            }
        }

        else
        {
            if(dir == "Up")
            {
                number = 3;
            }
            else
            {
                number = -3;
            }
        }
        Debug.Log(number);
        Debug.Log(arm.tag);
        //arm.transform.eulerAngles = new Vector3(0, 0,
        //arm.transform.eulerAngles.z + number);
        foreach(_Muscle muscle in muscles)
        {
            if(way == "Right" && muscle.bone.gameObject.tag == "rArm")
            {
                muscle.restRotation += number;
            }
            else if(way == "Left" && muscle.bone.gameObject.tag == "lArm")
            {
                muscle.restRotation += number;
            }
        }
        Debug.Log(arm.transform.eulerAngles);
    }

    public void Jump()
    {
        // could add vectors as variables to edit in editor
        if (HasCollided)
        {
            foreach(_Muscle muscle in legs)
            {
<<<<<<< HEAD
                muscle.bone.AddForce(JumpVector, ForceMode2D.Impulse);
=======
                muscle.bone.AddForce(new Vector2(0, 50f), ForceMode2D.Impulse);
>>>>>>> 242da17ffddb7cca4e9364343fad6ca7e8037683
            }
        }
    }

    public void Slide1() {
        int value = 1;
        if (Direction == "Left")
        {
            value = -1;
        }
        foreach(_Muscle muscle in legs)
        {
            muscle.restRotation = muscle.restRotation + value;
        }
        if (legs[0].restRotation != (value * 25))
        {
            Invoke("Slide1", 0.01f);
        }
        else
        {
            Debug.Log("false");
            Sliding = false;
        }
    }

    public void DropGun(GameObject Arm) {
        GameObject weapon;
        if (Arm.tag == "rArm")
        {
            weapon = rWeapon;
            lWeapon = null;
        }
        else
        {
            weapon = lWeapon;
            lWeapon = null;
        }
        Rigidbody2D body = weapon.gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        if(weapon.gameObject.tag == "Gun")
        {
            Destroy(weapon.GetComponent<FollowPos>());
            Destroy(weapon.GetComponent<NoCollision>());
            weapon.GetComponent<shooting>().grabbed = false;
        }
        else
        {
            weapon.transform.SetParent(null);
        }
    }

    public void StopHold(GameObject Arm, string Direction ) {
        Component[] Hinges = Arm.GetComponents(typeof(HingeJoint2D));
        if (Hinges.Length > 1)
        {
            Destroy(Hinges[1]);
        }
        else
        {
            Component Distance = Arm.GetComponent<DistanceJoint2D>();
            Destroy(Distance);
        }
        if (Direction == "RightA") {
            NowHoldingR = false;
            pivotedR = false;
        }

        else if(Direction == "LeftA"){
            NowHoldingL = false;
            pivotedL = false;
        }

        else if(Direction == "RightL")
        {
            NowPosturingR = false;
        }

        else
        {
            NowPosturingL = false;
        }

<<<<<<< HEAD
        WalkLeftVector = CopiedWalkLeftVector;
        WalkRightVector = CopiedWalkRightVector;
        ResetMass();
    }
    public void ResetMass()
    {
        foreach(_Muscle muscle in muscles)
        {
            if(muscle.bone.gameObject.tag == "rArm" || muscle.bone.gameObject.tag == "lArm")
            {
                muscle.bone.mass = 1/arm_length;
            }
            else if(muscle.bone.tag == "Body")
            {
                muscle.bone.mass = body_mass;
            }
            else if(muscle.bone.tag == "Head")
            {
                muscle.bone.mass = head_mass;
            }
            else if(muscle.bone.tag == "rLeg" || muscle.bone.tag == "lLeg")
            {
                muscle.bone.mass = leg_mass;
            }
            else
            {
                muscle.bone.mass = foot_mass;
            }
            muscle.bone.drag = 0f;
        }
        rArmRigid.mass = 1/arm_length;
        lArmRigid.mass = 1/arm_length;
        JumpVector = ogJumpVector;
=======
        foreach(_Muscle muscle in muscles)
        {
            if(muscle.bone.gameObject.tag == Arm.tag)
            {
                muscle.bone.mass = 1/arm_length;
            }
        }
>>>>>>> 242da17ffddb7cca4e9364343fad6ca7e8037683
    }
    // if you feel like this goes too far, look at bottom where there is code
    // that makes it feel more grounded by pushing head
    public void Backflip()
    {
        foreach (_Muscle muscle in legs)
        {

            muscle.restRotation -=5;
        }
        if(legs[0].restRotation > -355)
        {
            Invoke("Backflip", 0.001f);
        }
        else
        {
            GetUpAni();
        }
        Debug.Log("back");
    }

    public void Frontflip()
    {
        foreach (_Muscle muscle in legs)
        {
            muscle.restRotation +=5;
        }
        if(legs[0].restRotation != 355)
        {
            Invoke("Frontflip", 0.001f);
        }
        else
        {
            GetUpAni();
        }
        Debug.Log("front");
    }
    // could also change the force to make it more smooth
    // also add propulsion from head
    public void Fly()
    {
        if (Direction == "Right")
        {
            //if (rbLLeg)
            //{
                 //rbLLeg.AddForce(new Vector2(10f,5f), ForceMode2D.Impulse);
            //}

            //if (rbRLeg)
            //{
                //rbRLeg.AddForce(new Vector2(10f,5f), ForceMode2D.Impulse);
            //}
            //Invoke("StraightAni", 0f);
            if (muscles[0].restRotation == -20)
            {
            
                foreach(_Muscle muscle in legs)
                {
                    muscle.bone.AddForce(new Vector2(5f,7f), ForceMode2D.Impulse);
                }
                Invoke("StraightAni", 0f);
            }

            else 
            {
                Invoke("Propel", 0f);
            }
        }

        if(Direction == "Left")
        {

            if (muscles[0].restRotation == 20)
            {
                    foreach(_Muscle muscle in legs)
                    {
                        muscle.bone.AddForce(new Vector2(-2f,3f), ForceMode2D.Impulse);
                    }
                Invoke("StraightAni", 0f);
            }

            else 
            {
                Invoke("Propel", 0f);
            }
        }
    }
    public void SlowDown()
    {
        Time.timeScale = 0.1f;
        time = Time.time;
    }
    public void CrouchAni()
    {
        float interval = 0;
        int increment = 0;
        int target = 0;
        bool swivel = false;
        int multi = 1;
        if(((body_muscle.restRotation == 90 && Direction == "Right") || (body_muscle.restRotation == -90 && Direction == "Left")))
        {
            swivel = true;
        }
        if(!wasCrouched || swivel)
        {
            if(swivel)
            {
                interval = 0.001f;
            }
            else
            {
                interval = 0.01f;
            }
            if(Direction == "Left")
            {
                increment = 1 * multi;
                target = 90;
            }
            else
            {
                increment = -1 * multi;
                target = -90;
            }
        }
        else
        {
            if(Direction == "Left")
            {
                increment = -1;
                target = 0;
            }
            else
            {
                increment = 1;
                target = 0;
            }
            interval = 0.01f;
        }
        if(body_muscle.restRotation != target)
        {
            body_muscle.restRotation += increment;
            Debug.Log(crouching);
            Invoke("CrouchAni",interval);
        }
        else
        {
            CrouchAniDone = true;
        }
    
    }

    public void GetUpAni()
    {   int counter = 0;
        if(!crouching)
        {
            foreach (_Muscle muscle in muscles)
            {   
                if(muscle.restRotation > 0)
                {
                    muscle.restRotation = muscle.restRotation - 1 ;
                }

                else if(muscle.restRotation < 0)
                {
                    muscle.restRotation = muscle.restRotation + 1 ;
                }

                else
                {
                    counter = counter + 1;
                }    
            }
        }

            if (counter != muscles.Length )
            {
                Invoke("GetUpAni", 0.01f);
            }
    }

    public void StraightAni() {
        if (Direction == "Left")
        {

        
            foreach(_Muscle muscle in muscles)
            {
                    muscle.restRotation = muscle.restRotation + 1;
            }
            if (muscles[0].restRotation != 90)
            {
                Invoke("StraightAni", 0.02f);
            }
        }
        else
        {
            foreach(_Muscle muscle in muscles)
            {
                    muscle.restRotation = muscle.restRotation - 1;
            }
            if (muscles[0].restRotation != -90)
            {
                Invoke("StraightAni", 0.01f);
            }
        }
    }

    public void Propel()
    {
        if(Direction == "Right")
        {

            
            foreach(_Muscle muscle in muscles)
                {
                        muscle.restRotation = muscle.restRotation - 1;
                }
                if (muscles[0].restRotation != -20)
                {
                    Invoke("Propel", 0.01f);
                }
                else
                {
                    Fly();
                }
        }

        else
        {
            foreach(_Muscle muscle in muscles)
                {
                        muscle.restRotation = muscle.restRotation + 1;
                }
                if (muscles[0].restRotation != 20)
                {
                    Invoke("Propel", 0.01f);
                }

                else 
                {
                    Fly();
                }
        }
    }

    public void Recoil()
    {
    }
}

[System.Serializable]
public class _Muscle
{
    public Rigidbody2D bone;
    public float restRotation;
    public float force;
    public bool activated = true;
 
    public void ActivateMuscle()
    { 
        GameObject gameobj = bone.gameObject;
        Stickman stick = gameobj.transform.root.gameObject.GetComponent<Stickman>();
        if ((gameobj.tag != "rLeg" || (!stick.posturingR && !stick.NowPosturingR)) && (gameobj.tag != "lLeg" || (!stick.posturingL && !stick.NowPosturingL) ) && activated )
        {
             if (restRotation > 0 || restRotation < 0 || (restRotation == 0 && stick.flying == false) )
             {
                if (bone.gameObject.tag == "rLeg")
                {
                    //Debug.Log("dodastankyleg");
                    //Debug.Log(bone.rotation);
                    //Debug.Log(restRotation);
                    //Debug.Log(force);

                }
                if(bone.gameObject.tag == "LrLeg")
                {
                    Debug.Log("gogetemchamp");
                }
                if(bone.gameObject.tag == "LlLeg")
                {
                    Debug.Log("zoowemama");
                }
                if(bone.gameObject.tag == "lLeg")
                {
                    Debug.Log("gobaby");
                }
                if(bone.IsSleeping())
                {
                    Debug.Log("immmslleeep");
                }
                bone.MoveRotation(Mathf.LerpAngle(bone.rotation, restRotation, force * Time.deltaTime));
            }
        }
    }
    //if (Direction == "Right")
        //{
        //rbLLeg.AddForce(new Vector2(-10f,5f), ForceMode2D.Impulse);
        //rbRLeg.AddForce(new Vector2(-10f,5f), ForceMode2D.Impulse);
        //rbHead.AddForce(new Vector2(20f,0f), ForceMode2D.Impulse);
        //}

        //if(Direction == "Left")
        //{
        //rbLLeg.AddForce(new Vector2(10f,5f), ForceMode2D.Impulse);
        //rbRLeg.AddForce(new Vector2(10f,5f), ForceMode2D.Impulse);
        //rbHead.AddForce(new Vector2(-20f,0f), ForceMode2D.Impulse);
        //}
}
