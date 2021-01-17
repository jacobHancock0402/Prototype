

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Stickman: MonoBehaviour
{
    // this ***REMOVED*** works like functionally
    // but you still can't actually climb
    // mutliple reasons
    // shoulders don't rotate = hard to get other arm over head, and arm above head in general
    // could lengthen arms
    // could consider letting arms go through walls when trying to hold
    // this avoids one of biggest problem, where arms that starts climb
    // is pretty much ***REMOVED***ing clamped to wall even when joints removed
    // this means no space to move it upwards and pivot again
    // this would work by ignoring collisions until a button is pressed, then running checkGrab
    // decently large rewrite with all controls and ***REMOVED***, although checkgrab should be pretty simple
    // as just change to call on button press, not coll
    static Stickman(){

    }
    public _Muscle[] muscles;
    public Dictionary<string, AudioSource[]> Audios;
    public Dictionary<string, int> Audio_Map; // = //new  Dictionary<string, Dictionary<string, AudioClip>> {
        //"Metallic", new Dictionary<string, AudioClip> {
           // {"Loud Bullets", Loud_Bullets},
            //{"Soft Bullets", Soft_Bullets},
            
        //}
   // } 
    public AudioSource Soft_Bullets;
    public bool swinging;
    public bool swingingL;
    public bool swingingR;
    public float AirTime;
    public float rLegRestRotation = 0;
    public float lLegRestRotation = 0;
    public AudioSource Loud_Bullets;
    public Vector3 direction;
    public Rigidbody2D BodyRigid;
    public float angle;
    public AudioSource defaultBulletHit;
    //public Dictionary<string, AudioClip> = new Dictionary<string, AudioClip>();
    public AudioSource[] AudioSources;
    public string currentPivotArm;
    public GameObject hand;
    public bool wasCrouched;
    public GameObject lWeapon;
    public GameObject rWeapon;
    public _Muscle body_muscle;
    public string lastDir;
    public bool moving;
    public Rigidbody2D rbBody;
    public bool Sprinting;
    public float WalkMultiplier;
    // climbing is very functinonal now
    // still slight inconsistencies like left arm looking like ignoring collisions when pregrab but right doesn't
    // still can't vault things as the legs are over edge on narrow beams
    // = when grip released, the legs are over edge so fall off
    // the walker actually seems to crouch auto when trying to under things
    // force of pull should prob be adjusted, and rate at which distance increases
    // walking rhythm is pretty bad with long arms as a lot of sway
    // should prob fix this walking and clean climbing then make game actually fun
    // walking is key to this as right now way too imbalanced and ***REMOVED*** for fun
    // although perhaps game could be like funny?
    public bool crouching;
    // this might cause problems if die midair, like you can't jump after spawn. = make reset
    public bool jumping = false;
    public float head_mass;
    public float body_mass;
    public bool CrouchAniDone;
    public _Muscle[] upperLeg;
    public int pivotingForceMultiplier;
    public float leg_mass;
    public float foot_mass;
    // jump vector aight, just fix walking rhythm and when you can actually jump , right now infintiely
    public bool Right;
    public int counter;
    public bool Left;
    public float arm_length;
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
    public bool HasCollidedWalk;
    public bool HasCollidedJump;
    public string stepR = "right";
 
    public Vector2 WalkRightVector;
    public Vector2 WalkLeftVector;
    public Vector2 SprintRightVector;
    public Vector2 SprintLeftVector;

    // i don't think these values are correct , ywis think was 600 then maybe x=1000 for wlak and 1400 for sprint or sum
 
    private float MoveDelayPointer;
    public bool PreHoldingL = false;
    public bool PreHoldingR = false;
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
    public GameObject heavyObj;
    public float tim = 0;
    public bool found1 = false;
    public bool found2 = false;
    public _Muscle muscleL;
    public Vector2 ogJumpVector;
    public Vector2 ogWalkLeftVector;
    public Vector2 ogWalkRightVector;
    public _Muscle muscleR;
    public float SprintMultiplier;
    public AudioSource[] AudioClips;
 
 
    // Update is called once per frame

    // the movement from gun is caused by collider but , without it, arm goes weird
    // and start going inwards to body
    // this is caused by the rigidbody trying to push back
    // minimising mass seems to get rid of unnauathorised movement but still jank
    // gravity scale = 0 means jank is minimal so it's clear rigid body is at fault
    public void Start()
    {
        Audios = new Dictionary<string, AudioSource[]>();
        Audio_Map = new Dictionary<string, int>();
        AudioSource[] metalAudio = {Soft_Bullets, Loud_Bullets, defaultBulletHit};
        Audios.Add("Metallic", metalAudio);
        Audio_Map.Add("Soft Bullets", 0);
        Audio_Map.Add("Loud Bullets", 1);
        Audio_Map.Add("defaultBulletHit",2);

        GameObject[] metals = GameObject.FindGameObjectsWithTag("Metallic");
        foreach(GameObject metal in metals)
        {
            AudioSource source = metal.AddComponent(typeof(AudioSource)) as AudioSource;
            source.clip = AudioSources[3].clip;
            //AudioSource die = metal.GetComponent<AudioSource>();
            //die = AudioSources[3];
        }
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
            ogWalkLeftVector = WalkLeftVector;
            ogWalkRightVector = WalkRightVector;
            BodyRigid = Body.GetComponent<Rigidbody2D>();

        ti = Time.time;
        heavyObj = transform.GetChild(0).gameObject;
    }
    // add animation for backflip and frontflip by rotating 360 over set time
    private void Update()
    {
        direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - lArm.transform.position).normalized;
        angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        //WalkRightVector = direction * 2000;
        //WalkLeftVector = direction * 2000;
        //WalkRightVector = WalkRightVector 
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
        if((swingingR || swingingL) && !HasCollidedWalk && !HasCollidedJump && (Time.time - AirTime > 1f))
        {
           swinging = true;
        }
        else
        {
            swinging = false;
        }
        if(Input.GetKeyUp(KeyCode.D) && Right)
        {
            Right = false;
            //GetUpAni();
        }
        if(Input.GetKeyUp(KeyCode.A) && Left)
        {
            Left = false;
            //GetUpAni();
        }
        if(grabbingR || holdingR || NowGrabbingR)
        {
            disableR = false;
        }
        else
        {
            disableR = true;
        }
        if(grabbingL || holdingL || NowGrabbingL)
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //foreach(_Muscle muscle in upperLeg)
            //{
                //muscle.restRotation = 90;
            //}
            if(HasCollidedJump)
            {
                Jump();
            }
            else if(swinging)
            {
                JumpFromRope();
            }
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
                foreach(AudioSource source in AudioSources)
                {
                    source.pitch = 1f;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (NowGrabbingL)
            {
                DropGun(lHand);
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
                PreHoldingR = false;
                PreHoldingL = false;
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
                DropGun(lHand);
                NowGrabbingL = false;
            }
            else
            {
                if(!holdingL)
                {
                    holdingL = true;
                    PreHoldingL = true;
                }
                else
                {
                    holdingL = false;
                    PreHoldingR = false;
                }
                PreHoldingR = false;
                grabbingL = false;
                grabbingR = false;
                holdingR = false;
                posturingL = false;
                posturingR = false;
            }
        }

        if(NowHoldingL)
        {
            //foreach(_Muscle muscle in muscles)
            //{
                // this is decent balance between strength to hold up
                // and light enought to not collapse
                // gonna have try to hit a sweet spot, as right now slightly too heavy
                // and prob will snap at high speeds, could try decrease leg mass
                // also have to reset all values after
               // if(muscle.bone.gameObject.tag == "lArm")
                //{
                    //muscle.bone.mass = 10f;
                    //lArmRigid.mass = 10f;
                    //muscle.bone.gravityScale = 0f;
                //}
                //else if(muscle.bone.gameObject.tag != "lLeg" && muscle.bone.gameObject.tag != "rLeg" && muscle.bone.gameObject.tag != "rArm" )
                //{
                    //muscle.bone.mass = 0.1f;
                //}
                //else if(muscle.bone.gameObject.tag == "rArm")
                //{
                    //muscle.bone.mass = 0.001f;
                //}
            //}
        }
        if(NowHoldingR)
        {
            //foreach(_Muscle muscle in muscles)
            //{
                //if(muscle.bone.gameObject.tag == "rArm")
                //{
                   // muscle.force = 100;
                //}
                //else
               // {
                    //muscle.bone.mass = 0.00001f;
                //}
            //}
        }

        if (Input.GetKeyDown(KeyCode.G) && legs[0].restRotation != 70)
        {
            Invoke("Slide1", 0f);
            Sliding = true;
        }

        if (Input.GetKeyDown(KeyCode.C)) {
            if(NowGrabbingR)
            {
                DropGun(rHand);
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
                PreHoldingR = false;
                PreHoldingL = false;
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
                DropGun(rHand);
                NowGrabbingR = false;
            } 
            else
            {
                if(!holdingR)
                {
                    holdingR = true;
                    PreHoldingR = true;
                }
                else
                {
                    holdingR = false;
                    PreHoldingR = false;
                }
                PreHoldingL = false;
                grabbingR = false;
                grabbingL = false;
                holdingL = false;
                posturingL = false;
                posturingR = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            if(holdingR)
            {
                PreHoldingR = false;
            }
            else if(holdingL)
            {
                PreHoldingL = false;
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
            //if(NowHoldingL || NowHoldingR)
            //{
               // foreach(_Muscle muscle in muscles)
               // {
                    //if(muscle.bone.gameObject.tag == "rLeg" || muscle.bone.gameObject.tag == "lLeg")
                    //{
                        //muscle.bone.mass = 0.15f;
                    //}
                //}
                //JumpVector = JumpVector * (0.15f/leg_mass);
            //}
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
         //   if(NowHoldingL || NowHoldingR)
         //   {
                //foreach(_Muscle muscle in muscles)
              //  {
                    //if(muscle.bone.gameObject.tag == "rLeg" || muscle.bone.gameObject.tag == "lLeg")
                   // {
                      // muscle.bone.mass = 0.15f;
                   // }
                //}
                //JumpVector = JumpVector * (0.15f/leg_mass);
            //}
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            pivoting = false;
        }
        

        if (pivoting)
        {  
            Invoke("pullUp",0.1f);
            //Body.transform.position = Vector3.MoveTowards(Body.transform.position,rHand.transform.position,0.1f);
            if (NowHoldingL)
            {
                //pivotedL = true;
                //if (lArm.transform.position.x < Body.transform.position.x)
                //{
                    //Invoke("pullUp", 0.01f);
                    //way = "Left";
                    //arm = lArm;
                //}

                //else if(lArm.transform.position.x > Body.transform.position.x)
                //{
                    //Invoke("pullUp", 0.01f);
                    //way = "Right";
                    //arm = lArm;
                //}
                arm = lArm;
                hand = lHand;
            }

            else if (NowHoldingR)
            {
                arm = rArm;
                hand = rHand;
                //pivotedR = true;
                //if (rArm.transform.position.x < Body.transform.position.x)
                //{
                    //Invoke("pullUp", 0.01f);
                    //way = "Left";
                    //arm = rArm;
                //}

                //else if(rArm.transform.position.x > Body.transform.position.x)
                //{
                    //Invoke("pullUp", 0.01f);
                    //arm = rArm;
                    //way = "Right";
               // }
            //}
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
                    //if(holdingL)
                   // {
                       // muscle.bone.AddForce(direction / 5, ForceMode2D.Impulse);
                   // }
                    muscle.restRotation = -angle + 180;
                }
            
            }

            //Transform transGun = lArm.transform.GetChild(rArm.transform.childCount - 1);
            //if(transGun.childCount > 0)
            //{
                //transGun = transGun.GetChild(0);
            //}
            //if(transGun.gameObject.tag == "Gun")
            //{
                //transGun.rotation = Quaternion.Euler(0.0f,0.0f,-angle + 90);
                //transGun.position = transform.position;
               // transGun.parent = lHand.transform;
               // Debug.Log("***REMOVED******REMOVED***s");
                // arm is perfect now, walking still trip bit
                // now move onto picking stuff up and climbing
           // }/
            //rArm
            //lArm.transform.eulerAngles = new Vector3(0, 0, -angle + 270);
            //lArmRigid.freezeRotation = true;
        }

        if (disableR == false)
        {
            disable = false;
            Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - lArm.transform.position).normalized;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            // angle shouldn't be negative but otherwise it pretty much goes in opposite direction
            Debug.Log("ballinlikeim24");
            int j = 0;
            foreach(_Muscle muscle in muscles)
            {
                j++;
                if(muscle.bone.tag == "rArm")
                {
                    muscle.restRotation = - angle + 180;
                    //if(holdingR)
                   // {
                        //muscle.bone.AddForce(direction / 1000000, ForceMode2D.Impulse);
                  //  }
                    //muscle.bone.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -angle + 90); // = new Vector3(muscle.bone.transform.rotation.x, muscle.bone.transform.rotation.y, angle);
                    Debug.Log(muscle.restRotation);
                    //muscle.bone.freezeRotation = true;
                }
            }
            
            //}
            // could remove this call to get gun other places
            //Transform transGun = rArm.transform.GetChild(rArm.transform.childCount - 1);
            //if(transGun.childCount > 0)
            //{
               // transGun = transGun.GetChild(0);
            //}
            //if(transGun.gameObject.tag == "Gun")
            //{
                //transGun.rotation = Quaternion.Euler(0.0f,0.0f,-angle + 90);
                //transGun.position = transform.position;
                //transGun.parent = rHand.transform;
                //Debug.Log("***REMOVED******REMOVED***s");
                // arm is perfect now, walking still trip bit
           //     // now move onto picking stuff up and climbing
           //}
            //rArm.transform.eulerAngles = new Vector3(0, 0, -angle + 90);
            //rArmRigid.freezeRotation = true;
        }
       // else
       // {
            //disable = true;
        //}

        if(posturingL)
        {
            direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - lArm.transform.position).normalized;
            angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            // angle shouldn't be negative but otherwise it pretty much goes in opposite direction
            lLeg.transform.eulerAngles = new Vector3(0, 0, -angle + 180);
            lLeg.GetComponent<Rigidbody2D>().freezeRotation = true;
        }

        if(posturingR)
        {
            direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - lArm.transform.position).normalized;
            angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
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
                DropGun(rHand);
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
                DropGun(lHand);
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



        // a lot of drag ***REMOVED*** here to try and make more stable
        // just makes too slow and awkward
        if (Input.GetKeyDown(KeyCode.A))
        {
            Left = true;
            Direction = "Left";
        }
        Vector3 pos1 = muscleR.bone.gameObject.transform.position - muscleL.bone.gameObject.transform.position;
        Vector3 pos2 = muscleL.bone.gameObject.transform.position - muscleR.bone.gameObject.transform.position;
        Debug.Log(pos1.magnitude);
        // this one's fine as shouldn't really affect anything negative and is at right distance to reset
        // looks real nice
        if(pos1.magnitude > 2.5 & !moving && !NowGrabbingR && !NowGrabbingL)
        {
            muscleL.bone.AddForce(pos1.normalized * 300, ForceMode2D.Impulse);
            muscleR.bone.AddForce(pos2.normalized * 300, ForceMode2D.Impulse);
            //Jump();
        }
        // this one might be problematic as haven't tested
        // will more than likely kick feet back and mess up rythm as feet in air
        // not horiz vector like is on ground
        // otherwise walking is real nice
        // only problem is how the body sometimes falls behind
        // but quite stable and little over extension
        //on to making an actual fun game next
        // although there are still few nocoll and ***REMOVED*** issues with climbing
        if(pos1.magnitude > 4 && !NowGrabbingR && !NowGrabbingL)
        {
            muscleL.bone.AddForce(pos1.normalized * 300, ForceMode2D.Impulse);
            muscleR.bone.AddForce(pos2.normalized * 300, ForceMode2D.Impulse);
        }

        if(moving)
        {
            if(Sprinting)
            {
               // rbBody.drag = 100;
                //rbRLeg.drag = 100;
                //rbLLeg.drag = 100;
            }
            else
            {
                //rbBody.drag = 10;
                //rbRLeg.drag = 10;
                //rbLLeg.drag = 10;
            }
            moving = true;
        }
 
        if (Input.GetButtonUp("Horizontal"))
        {
            Left = false;
            Right = false;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && HasCollidedWalk)
        {
            if(Sprinting)
            {
                Sprinting = false;
            }
            else
            {
                Sprinting = true;
            }
        }
        if(Sprinting)
        {
            WalkMultiplier = SprintMultiplier;
        }
        else
        {
            WalkMultiplier = 1f;
        }
        if(!moving && !NowGrabbingR && !NowGrabbingL)
        {
            //rbBody.drag = 0;
            //rbRLeg.drag = 0;
            //rbLLeg.drag = 0;
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
        if((swinging) && (Right || Left))
        {
            Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - Body.transform.position).normalized;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            rbRLeg.AddForce(direction * 100, ForceMode2D.Impulse);
            rbLLeg.AddForce(direction * 100, ForceMode2D.Impulse);
            tim = Time.time;
        }
        if (!Sliding && Right == true && Left == false  && (Time.time - tim) > delay && HasCollidedWalk)
        {
            moving = true;
            //foreach(_Muscle muscle in muscles)
            //{
                //if(muscle.bone.gameObject.tag != "rLeg" && muscle.bone.gameObject.tag != "lLeg")
                //{
                    //muscle.restRotation = 15;
                //}
            //}
            if(swinging)
            {
                Swivel();
            }
            else
            {
                if(lastDir == "left")
                {
                    Jump();
                    tim = Time.time;
                }
                else
                {
                    if(HasCollidedWalk && stepR == "right")
                    {
                        Invoke("Step1Right", 0f);
                    }
                    else if(HasCollidedWalk && stepR == "left")
                    {
                        Invoke("Step2Right", 0f);
                    }
                }
            }
            lastDir = "right";
        }

        // figure out why no collision script isn't working
        // once sorted, a few tweaks to vectors and ***REMOVED*** should make walking good
        // none of left movement has config
        // way to little mass on dude to effect rope
        // could increase? or add force to rope although this would look weird
        // could try and simulate a weigth at teh end of the rope
        else if (!Sliding && Left == true && Right == false && (Time.time - tim) > delay && HasCollidedWalk)
        {
            moving = true;
            if(swinging)
            {
                Swivel();
            }
            else
            {
                if(lastDir == "right")
                {
                    Jump();
                    tim = Time.time;
                }
                //foreach(_Muscle muscle in muscles)
                //{
                    //if(muscle.bone.gameObject.tag != "rLeg" && muscle.bone.gameObject.tag != "lLeg")
                    //{
                    // muscle.restRotation = -15;
                    //}
                //}
                // i should really add in ability to climb up slopes and ***REMOVED*** instead of just jumping
                else
                {
                    if(HasCollidedWalk && stepL == "left")
                    {
                        Invoke("Step1Left", 0f);
                    }
                    else if(HasCollidedWalk && stepL == "right")
                    {
                        Invoke("Step2Left", 0f);
                    }
                }
                lastDir = "left";
            }
        }
        if(Time.time - tim > 2)
        {
            stepR = "right";
            stepL = "left";
            lastDir = "";
            moving = false;
        }
        else
        {
            moving = true;
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
    public void Swivel()
    {
        Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - Body.transform.position).normalized;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        rbRLeg.AddForce(direction * 250, ForceMode2D.Impulse);
        rbLLeg.AddForce(direction * 250, ForceMode2D.Impulse);
        tim = Time.time;

    }
    public void Step1Right()
    {
        int num = 0;
        Debug.Log("***REMOVED***offf");
        if(HasCollidedWalk)
        {
            // could experiment with adding forces from different places i.e knee to stop the tripping
            muscleR.bone.AddForce(WalkRightVector * WalkMultiplier, ForceMode2D.Impulse);
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
        if(HasCollidedWalk)
        {
        muscleL.bone.AddForce(WalkRightVector * WalkMultiplier * 1.1f, ForceMode2D.Impulse);
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
        if(HasCollidedWalk)
        {
            muscleL.bone.AddForce(WalkLeftVector * WalkMultiplier, ForceMode2D.Impulse);
            tim = Time.time;
            stepL = "right";
        }
        //muscleR.bone.AddForce(-WalkLeftVector * 0.5f, ForceMode2D.Impulse);
    }
 
    // pull up not working on rope, the legs sort of go downwards like cahgning mass or sum
    public void Step2Left()
    {
        //muscleL.bone.AddForce(-WalkLeftVector * 0.5f, ForceMode2D.Impulse);
        if(HasCollidedWalk)
        {
            muscleR.bone.AddForce(WalkLeftVector * WalkMultiplier * 1.1f, ForceMode2D.Impulse);
            tim = Time.time;
            stepL = "left";
        }
    }

    public void pullUp()
    {
        //Debug.Log("hi");
        //int number = 0;
        //if (way == "Right")
        //{
            //if(dir == "Up")
            //{
                //number = -3;
            //}
            //else
            //{
                //number = 3;
            ///}
        //}

        //else
        //{
            //if(dir == "Up")
            //{
                //number = 3;
            //}
            //else
            //{
                //number = -3;
            //}
        //}
        //Debug.Log(number);
        Debug.Log(arm.tag);
        float value = 0.1f;
        if (dir == "Up")
        {
            value = -0.1f;
        }
        Vector3 directionVector = (hand.transform.position - Body.transform.position).normalized;
        Body.GetComponent<Rigidbody2D>().AddForce(directionVector * pivotingForceMultiplier, ForceMode2D.Impulse);
        
        Component[] Distance = Body.GetComponents(typeof(DistanceJoint2D));
        DistanceJoint2D realJoint = null;
        foreach(DistanceJoint2D distance in Distance)
        {
            if(distance.connectedBody.gameObject.tag == currentPivotArm)
            {
                            //realJoint = distance;
                            //break;
            }
            distance.distance += value;
        }
        // it works now although looks a little weird
        // will have to figure out how to actually climb though
        // like arm doesn't stretch over head properly
        // could fix by increasing length of arm
        // walking rhythm could be adjusted as often step in on eahcother
        //SpringJoint2D Distance = Body.GetComponent<SpringJoint2D>();

        //arm.transform.eulerAngles = new Vector3(0, 0,
        //arm.transform.eulerAngles.z + number);
        //foreach(_Muscle muscle in muscles)
        //{
            //if(way == "Right" && muscle.bone.gameObject.tag == "rArm")
            //{
                //muscle.restRotation += number;
            //}
            //else if(way == "Left" && muscle.bone.gameObject.tag == "lArm")
            //{
                //muscle.restRotation += number;
            //}
        //}
        Debug.Log(arm.transform.eulerAngles);
    }

    public void Jump()
    {
        // could add vectors as variables to edit in editor
        if (HasCollidedJump)
        {
            foreach(_Muscle muscle in legs)
            {
                muscle.bone.AddForce(JumpVector, ForceMode2D.Impulse);
            }
        }
    }
    public void JumpFromRope()
    {
        if(swingingR)
        {
            StopHold(rHand, "RightA");
        }
        if(swingingL)
        {
            StopHold(lHand, "LeftA");
        }
        Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - Body.transform.position).normalized;
        //Body.GetComponent<Rigidbody2D>().AddForce(direction * 5000, ForceMode2D.Impulse);
        rbRLeg.GetComponent<Rigidbody2D>().AddForce(direction * 1000, ForceMode2D.Impulse);
        rbLLeg.GetComponent<Rigidbody2D>().AddForce(direction * 1000, ForceMode2D.Impulse);
        // jump values ok 
        // but looks weird as whole body ***REMOVED***s
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

    public void DropGun(GameObject Hand) {
        GameObject weapon;
        //Debug.Log(Hand.transform.GetChild(0).GetChild(0).gameObject.tag);
        Transform child = Hand.transform.GetChild(0);
        weapon = child.GetChild(0).gameObject;
        Rigidbody2D body = weapon.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        if(weapon.tag == "Gun")
        {
            Destroy(weapon.GetComponent<FollowPos>());
            Destroy(weapon.GetComponent<NoCollision>());
            weapon.GetComponent<shooting>().grabbed = false;
        }
        weapon.transform.SetParent(null);
        
    }

    public void StopHold(GameObject Arm, string Direction ) {
        GameObject otherArm = new GameObject();
        //Debug.LogError("biggggie");
        if(swinging)
        {
               // Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - Body.transform.position).normalized;
                //float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            //Body.GetComponent<Rigidbody2D>().AddForce(new Vector3(1000f, 2000f), ForceMode2D.Impulse);
            //Debug.LogError("bigerror");
            //Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - Body.transform.position).normalized;
            //Body.GetComponent<Rigidbody2D>().velocity = direction * 1000;
        }
        if(Arm.tag == "rArm")
        {
            otherArm = lArm;
            hand = rHand;
            swingingR = false;
        }
        else
        {
            otherArm = rArm;
            hand = lHand;
            swingingL = false;
        }
        Component[] Hinges = Arm.GetComponents(typeof(HingeJoint2D));
        if (Hinges.Length > 1)
        {
            Destroy(Hinges[1]);
        }
        else
        {
            Component Distance = Arm.GetComponent<DistanceJoint2D>();
            Component[] Distances = otherArm.GetComponents(typeof(DistanceJoint2D));
            Destroy(Distance);
        }
        
        Component[] distance = Body.GetComponents(typeof (DistanceJoint2D));
        DistanceJoint2D actualJoint = null;
        if (distance.Length > 1)
        {
            Debug.LogError("multiple");
            foreach(DistanceJoint2D d in distance)
            {
                if (d.connectedBody.gameObject.tag == Arm.tag )
                {
                    actualJoint = d;
                }
            }
        }
        else
        {
            Debug.LogError("minmax");
         
            foreach(DistanceJoint2D d in distance)
            {
                actualJoint = d;
            }
        }
        Destroy(actualJoint);
        bool stillHolding = false;
        if (Direction == "RightA") {
            NowHoldingR = false;
            pivotedR = false;
            if (NowHoldingL)
            {
                stillHolding = true;
            }
        }

        else if(Direction == "LeftA"){
            NowHoldingL = false;
            if (NowHoldingR)
            {
                stillHolding = true;
            }
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
        if(!stillHolding)
        {
            //WalkLeftVector = ogWalkLeftVector;
            //WalkRightVector = ogWalkRightVector;
            ResetMass();
        }
        //else
        //{
            if(stillHolding)
            {
                CheckGrab grab = hand.GetComponent<CheckGrab>();
                Debug.LogError(Direction);
                if(Direction == "LeftA")
                {
                    Debug.LogError("***REMOVED***");
                    grab.ScaleRightMasses(); 
                }
                else
                {
                    Debug.LogError("die");
                    grab.ScaleLeftMasses();
                }
            }
            // idk why this doesn't work
            // jump might not work
            // elasticity seems fine although heavy,
            // check climbing up rope
            // mass of reg rope is different
           // if(swinging)
           // {
               // Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - Body.transform.position).normalized;
                //float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
              // Body.GetComponent<Rigidbody2D>().AddForce(new Vector3(1000f, 2000f), ForceMode2D.Impulse);
               //Debug.LogError("bigerror");
           //}
        //}
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
        //JumpVector = ogJumpVector;
        Debug.Log("whatdidyoudoonthissaturaday");
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
                    muscle.bone.AddForce(direction * 100, ForceMode2D.Impulse);
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
                        muscle.bone.AddForce(direction * 100, ForceMode2D.Impulse);
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
        foreach(AudioSource source in AudioSources)
        {
            source.pitch = 0.2f;
        }
        //AudioSources[0].pitch = 0.2f;
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
                if (bone.gameObject.tag == "rLeg" || bone.gameObject.tag == "rFoot")
                {
                    //Debug.Log("dodastankyleg");
                    //Debug.Log(bone.rotation);
                    //Debug.Log(restRotation);
                    //Debug.Log(force);
                    restRotation = -stick.rLegRestRotation ;

                }
                if(bone.gameObject.tag == "LrLeg")
                {
                    //Debug.Log("gogetemchamp");
                }
                if(bone.gameObject.tag == "LlLeg")
                {
                    //Debug.Log("zoowemama");
                }
                if(bone.gameObject.tag == "lLeg" || bone.gameObject.tag == "lFoot" || bone.gameObject.tag == "Body")
                {
                    //Debug.Log("gobaby");
                    restRotation = -stick.lLegRestRotation;
                    if(bone.gameObject.tag == "Body")
                    {
                        //restRotation = -stick.lLegRestRotation - 90;
                    }
                }
                if(bone.IsSleeping())
                {
                    //Debug.Log("immmslleeep");
                }
                bone.MoveRotation(Mathf.LerpAngle(bone.rotation, restRotation, force * Time.deltaTime));
                //bone.angularVelocity *= 0.05f;
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
