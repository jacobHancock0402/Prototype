

using UnityEngine;
using System;
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
    public bool pathFinding;
    public Dictionary<string, AudioSource[]> Audios;
    public GameObject playerBody;
    public Vector3 playerPos;
    public Dictionary<string, int> Audio_Map; // = //new  Dictionary<string, Dictionary<string, AudioClip>> {
        //"Metallic", new Dictionary<string, AudioClip> {
           // {"Loud Bullets", Loud_Bullets},
            //{"Soft Bullets", Soft_Bullets},
            
        //}
   // } 
    public AudioSource Soft_Bullets;
    public Stickman playerStick;
    public Vector3 thisHeadToPlayerChest;
    public bool deadBodySet;
    public bool firing = false;
    public shooting shoot;
    public bool propelling;
    public bool swinging;
    public function propelFunct;
    public string climbingArm = "";
    public float ClimbingTimer;
    public float someTimer1 = 0f;
    public bool swingingL;
    public Vector3 lastPos;
    public bool Alerted;
    public GameObject Head;
    public string proneDir;
    public bool dead;
    public float health = 100f;
    public bool LastFrameHasCollidedWalk;
    public bool oneLegHasCollided;
    public float HasCollidedWalkTimer;
    public float canStandFromClimbTimer;
    public bool VaultPhase;
    public bool swingingR;
    public List<Node> path;
    public List<function> animations;
    public bool resettingRot;
    public Node wallNode;
    public Grid Grid;
    public bool sameArm;
    public bool proning = false;
    public string playerDir = "right";
    public bool ClimbingPhase;
    public _Muscle LShoulderMuscle;
    public _Muscle RShoulderMuscle;
    public Rigidbody2D body_rigid;
    public bool animating = false;
    public bool Flying;
    public GameObject rFoot;
    public string crouchDir;
    public Vector3 shoulderToWallDir;
    public int pathNodeCounter = 0;
    public GameObject lFoot;
    public Rigidbody2D lFootBody;
    public Rigidbody2D rFootBody;
    public Node firstNode;
    public Node currentNode;
    public Node pathNode;
    public int pathCounter;
    public float AirTime;
    public float rLegRestRotation = 0;
    public float lLegRestRotation = 0;
    public AudioSource Loud_Bullets;
    public Vector3 direction;
    public Rigidbody2D BodyRigid;
    public float grabTimer = 0f;
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
    public bool tryingToGrab;
    public string lastDir;
    public bool moving;
    public Rigidbody2D rbBody;
    public bool Sprinting;
    public Node maxNode;
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
    public bool crouching = false;
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
    public GameObject legs_armsPrefab;
    public BoxCollider2D legs_armsPrefabColl;
    public float exactArmLength;
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
    public float timeSinceClimb = 0f;
 
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
    public bool freefall = false;
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
    public float someOtherTimer = 0f;
    public GameObject rLeg;
    public GameObject lLeg;
    public GameObject arm;
    public bool pivoting;
    public string way;
    public bool priorityAnimating;
    public Rigidbody2D rArmRigid;
    public Rigidbody2D lArmRigid;
    public string dir;
    public bool pivotedR = false;
    public bool pivotedL = false;
    public float oldLegMass;
    public Rigidbody2D head;
    public GameObject heavyObj;
    public string alphabet;
    public Dictionary<string,bool> simPress;
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
    public float timeAtLastShot = 0f;
    public float timeSinceLastShot = 0f;
    public bool gunInR;
    public bool gunInL;
    public _Muscle head_muscle;
    public bool floating = false;
    public bool Player;
    public Vector3 dirLArmToMouse;
    public Vector3 dirRArmToMouse;
    public AudioSource[] dyingAudio;
    public AudioSource[] headShotAudio;
    public AudioSource headShot1;
    public AudioSource headShot2;
    public AudioSource headShot3;
    public AudioSource dying1;
    public AudioSource dying2;
    public AudioSource dying3;
    public AudioSource dying4;
    public AudioSource dying5;
    public float angleR;
    public float angleL;
    // in general seems to work in terms of moving to player
    // he shoots himself in end as his gun is in wrong pos, maybe nocoll with self, or raycast to see if will hit self
    // not sure about transitions, but next is to have actual states anyways so work on that
    // really should get sprinting up and running, should be fairly easy
 
 
    // Update is called once per frame

    // the movement from gun is caused by collider but , without it, arm goes weird
    // and start going inwards to body
    // this is caused by the rigidbody trying to push back
    // minimising mass seems to get rid of unnauathorised movement but still jank
    // gravity scale = 0 means jank is minimal so it's clear rigid body is at fault
    public void Start()
    {
        legs_armsPrefabColl = legs_armsPrefab.GetComponent<BoxCollider2D>();
        body_rigid = Body.GetComponent<Rigidbody2D>();
        playerDir = "right";
        alphabet = "ABCDEFGHIJKLMNOQRSTUVWXYZ";
        exactArmLength = arm_length * (Mathf.Abs(legs_armsPrefabColl.bounds.max[1] - legs_armsPrefabColl.bounds.min[1]));
        simPress = new Dictionary<string, bool>();
        propelFunct = new function();
        animations = new List<function>();
        Audios = new Dictionary<string, AudioSource[]>();
        Audio_Map = new Dictionary<string, int>();
        AudioSource[] metalAudio = {Soft_Bullets, Loud_Bullets, defaultBulletHit};
        Audios.Add("Metallic", metalAudio);
        Audio_Map.Add("Soft Bullets", 0);
        Audio_Map.Add("Loud Bullets", 1);
        Audio_Map.Add("defaultBulletHit",2); 
        //Debug.LogError("senpai");
        dyingAudio = new AudioSource[5];
        dyingAudio[0] = dying1;
        dyingAudio[1] = dying2;
        dyingAudio[2] = dying3;
        dyingAudio[3] = dying4;
        dyingAudio[4] = dying5;
        headShotAudio = new AudioSource[3];
        headShotAudio[0] = headShot1;
        headShotAudio[1] = headShot2;
        headShotAudio[2] = headShot3;
        Debug.LogError("hello");
        //Debug.logger.logEnabled = false;

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
                else if(muscle.bone.tag == "Head")
                {
                    head_muscle = muscle;
                }
            }
            ogJumpVector = JumpVector;
            ogWalkLeftVector = WalkLeftVector;
            ogWalkRightVector = WalkRightVector;
            BodyRigid = Body.GetComponent<Rigidbody2D>();

        ti = Time.time;
        heavyObj = transform.GetChild(0).gameObject;
        for(int i=0;i<alphabet.Length;i++)
        {
            simPress[Char.ToString(alphabet[i])] = false;
        }
        playerStick = playerBody.transform.root.gameObject.GetComponent<Stickman>();
    }
    // add animation for backflip and frontflip by rotating 360 over set time
    private void Update()
    {
        playerPos = playerBody.transform.position;
        if(!dead)
        {
        dirRArmToMouse = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - rArm.transform.position).normalized;
        angleR = Mathf.Round(Mathf.Atan2(dirRArmToMouse.x, dirRArmToMouse.y) * Mathf.Rad2Deg);
        dirLArmToMouse = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - lArm.transform.position).normalized;
        angleL = Mathf.Round(Mathf.Atan2(dirLArmToMouse.x, dirLArmToMouse.y) * Mathf.Rad2Deg);
        //WalkRightVector = direction * 2000;
        //WalkLeftVector = direction * 2000;
        //WalkRightVector = WalkRightVector 
        int q = 0;
        foreach (_Muscle muscle in muscles)
        {   
            if(freefall)
            {
                //muscle.activated = false;
            }
            //muscle.restRotation = 90;
            if (muscle.bone)
            {

                if (collided == true && flying)
                {
                    //muscle.restRotation = 0;
                }
                // change this to shooting arm is using 2hands
                if (muscle.bone.gameObject.tag == "rArm")
                {
                        //muscle.force = 15;
                        if(disableR == false && muscle.bone.gameObject.GetComponent<muscle_holder>() != null)
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
                    if(disableL == false && muscle.bone.gameObject.GetComponent<muscle_holder>() != null)
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
        if(currentPivotArm == "lArm")
        {
            hand = lHand;
        }
        else
        {
            hand = rHand;
        }
        if(NowGrabbingR)
        {
            if(!gunInR)
            {
                GameObject gun = rHand.transform.GetChild(0).GetChild(0).gameObject;
                shoot = gun.GetComponent<shooting>();
                if(gun.tag == "Gun")
                {
                    gunInR = true;
                }
            }
        }
        if(NowGrabbingL)
        {
            if(!gunInL)
            {
                GameObject gun = lHand.transform.GetChild(0).GetChild(0).gameObject;
                shoot = gun.GetComponent<shooting>();
                if(gun.tag == "Gun")
                {
                    gunInL = true;
                }
            }
        }
        if(Player)
        {


 
        if (Input.GetKeyDown(KeyCode.D))
        {
            Right = true;
            Direction = "Right";
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            if(crouching)
            {
                function funct2 = new function();
                funct2.name = "GetUpAni";
                funct2.animations = animations;
                animations.Add(funct2);
                crouching = false;
               // Debug.LogError("cameinforthecheddar");
            }
            else
            {
                //Debug.LogError("big***REMOVED***weon");
                wasCrouched = false;
                CrouchAniDone = false;
                function funct1 = new function();
                funct1.name = "CrouchAni";
                funct1.animations = animations;
                animations.Add(funct1);
                crouching = true;
            }
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            if(proning)
            {
                function funct2 = new function();
                funct2.name = "GetUpAni";
                funct2.animations = animations;
                animations.Add(funct2);
                crouching = false;
            }
            else
            {
                function funct1 = new function();
                funct1.name = "StraightAni";
                funct1.animations = animations;
                animations.Add(funct1);
            }
        }
        if(crouching)
        {
            if((playerDir == "left" && body_muscle.restRotation == -90) || (playerDir == "right" && body_muscle.restRotation == 90))
            {
                function funct = new function();
                funct.name = "CrouchAni";
                funct.animations = animations;
                animations.Add(funct);
            }
        }
        if(proning)
        {
            if(!priorityAnimating)
            {
                function funct = new function();
                funct.animations = animations;
                funct.name = "BodyToMouse";
                funct.notPriority = true;
                animations.Add(funct);
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
            function funct = new function();
            funct.name = "GetUpAni";
            funct.animations = animations;
            animations.Add(funct);
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
        

        

        if (disableL == false && !priorityAnimating) 
        {
            foreach(_Muscle muscle in muscles)
            {
                if(muscle.bone.gameObject.tag == "lArm")
                {
                    muscle.restRotation = -angleL + 180;
                }
            }
            // angle shouldn't be negative but otherwise it pretty much goes in opposite direction
            

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

        if (disableR == false && !priorityAnimating)
        {
            disable = false;
            // angle shouldn't be negative but otherwise it pretty much goes in opposite direction
            Debug.Log("ballinlikeim24");
            int j = 0;
            // this makes game run like ***REMOVED*** at times 
            // also has problems with having multiple not priority anis at same time
            //function funct = new function();
            //funct.name = "aim";
            //funct.notPriority = true;
            //funct.animations = animations;
            //animations.Add(funct);

            foreach(_Muscle muscle in muscles)
            {
                if(muscle.bone.gameObject.tag == "rArm")
                {
                    //Vector3 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - muscle.bone.transform.position).normalized;
                    //angleR = Mathf.Round(Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg);
                    muscle.restRotation = -angleR + 180;
                    //muscle.bone.AddForce(dirRArmToMouse * 0.4f, ForceMode2D.Impulse);
                    //muscle.bone.AddForce(direction * 0.2f, ForceMode2D.Impulse);
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
    if (Input.GetKeyDown(KeyCode.Z) && (!Player && simPress["Z"]))
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
        if (Input.GetKeyDown(KeyCode.X) || (!Player && simPress["X"])) {
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
                    Debug.LogError("***REMOVED***ishappening");
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
            simPress["X"] = false;
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            muscleL.bone.AddForce(new Vector2(0f, 1500f), ForceMode2D.Impulse);
            muscleR.bone.AddForce(new Vector2(0f, 1500f), ForceMode2D.Impulse);
            rbBody.AddForce(new Vector2(-1000f,0f), ForceMode2D.Impulse);
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

        if (Input.GetKeyDown(KeyCode.C) || (!Player && simPress["C"]))  {
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
            simPress["C"] = false;
        }

        if (Input.GetKeyDown(KeyCode.V) || (!Player && simPress["V"]))
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
            simPress["V"] = false;
        }
        if(Input.GetKeyDown(KeyCode.T) || (!Player && simPress["T"]))
        {
            if(holdingR)
            {
                PreHoldingR = false;
            }
            else if(holdingL)
            {
                PreHoldingL = false;
            }
            simPress["T"] = false;
        }
        if(holdingR)
        {
            Debug.LogError("big***REMOVED***,small***REMOVED***");
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

           //rArmRigid.freezeRotation = true;
        }

        else if((NowHoldingR  && pivotedR) || NowGrabbingR || grabbingR || holdingR)
        {
            //rArmRigid.freezeRotation = true;
        }

        else if(!pivotedR)
        {
           // rArmRigid.freezeRotation = false;
        }

        if(lArm.transform.childCount == 1)
        {
            if (grabbingL || holdingL) 
            {
                DropGun(lHand);
            }

            //lArmRigid.freezeRotation = true;
        }

        else if((NowHoldingL && pivotedL)  || NowGrabbingL|| grabbingL || holdingL)
        {
            //lArmRigid.freezeRotation = true;
        }

        else if(!pivotedL)
        {
           //lArmRigid.freezeRotation = false;
        }
        // if i dont freeze rot of shoulder like this, it rotates around whole body which looks weird



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
        //Debug.LogError(body_muscle.restRotation);
        if (Input.GetButtonUp("Horizontal") && Player)
        {
            Left = false;
            Right = false;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && HasCollidedWalk)
        {
            MultiplyWalkVector();
        }
        // if(Sprinting)
        // {
        //     WalkMultiplier = SprintMultiplier;
        // }
        // else
        // {
        //     WalkMultiplier = 1f;
        // }
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
        if (!Sliding && Right == true && (Time.time - tim) > delay)
        {
            if(HasCollidedWalk)
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
                        if(crouching)
                        {
                            CrouchAni();
                        }
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
            }
            // could make this else statement and let you fly
            else if(oneLegHasCollided)
            {
                body_muscle.bone.AddForce(new Vector2(100f,0f), ForceMode2D.Impulse);
            }
            playerDir = "right";
            lastDir = "right";
        }

        // figure out why no collision script isn't working
        // once sorted, a few tweaks to vectors and ***REMOVED*** should make walking good
        // none of left movement has config
        // way to little mass on dude to effect rope
        // could increase? or add force to rope although this would look weird
        // could try and simulate a weigth at teh end of the rope
        else if (!Sliding && Left == true && (Time.time - tim) > delay)
        {
            if(HasCollidedWalk)
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
                        if(crouching)
                        {
                            CrouchAni();
                        }
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
                }
            }
            else if(oneLegHasCollided)
            {
                body_muscle.bone.AddForce(new Vector2(-100f,0f), ForceMode2D.Impulse);
            }
            playerDir = "left";
            lastDir = "left";
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
        // for whatever reason, it doens't drop gun before climbing
        // it seems to be linked with how i'm simming?, wait no because even without doens't work
        // like if you drop the gun reg, then you can climb left and right, otherwise won't work
        // yeah so it all works bar this, just testing now and then either expand upon this with like jumping and non 
        // vertical movements, or get on to actual behaviour and ***REMOVED***

        if(oneLegHasCollided && !LastFrameHasCollidedWalk)
        {
            HasCollidedWalkTimer = Time.time;
        }
        else if(!oneLegHasCollided)
        {
            HasCollidedWalkTimer = 0f;
        }
        if(!ClimbingPhase)
        {
            if(NowHoldingL || holdingL)
            {
                simPress["X"] = true;
            }
            else if(NowHoldingR || holdingR)
            {
                simPress["V"] = true;
            }
        }
        if(ClimbingPhase && !tryingToGrab)
        {
            if(ClimbingTimer == 0f)
            {
                ClimbingTimer = Time.time;
            }
            //Debug.LogError("merlinwhatthe***REMOVED***!");
            LayerMask mask = LayerMask.GetMask("Climbable");
            // int theValue = -10;
            // if(playerDir == "right")
            // {
            //     theValue = 10;
            // }
            // Vector3 horizToHead = Head.transform.position - new Vector3(Head.transform.position.x + theValue, Head.transform.position.y, Head.transform.position.z);
            // RaycastHit2D notAtTop = Physics2D.Raycast(Head.transform.position,horizToHead,5f,mask);
            // // if(!notAtTop)
            // // {
            // //     Debug.DrawRay(Head.transform.position, horizToHead*100);
            // //     Debug.LogError("iamthebigmama");
            // //     ClimbingPhase = false;
            // //     VaultPhase = true;
            // // }

            grabTimer = 0f;
            someOtherTimer = 0f;
            checkClimbing();    
            if(climbingArm == "")
            {
                climbingArm = playerDir;
                StopAnyLeftArm();
                StopAnyRightArm();
            }
            else if(climbingArm == "right")
            {
                climbingArm = "left";
                //hand = lHand;
                StopAnyLeftArm();
            }
            else
            {
                climbingArm = "right";
               // hand = rHand;
                StopAnyRightArm();
            }
            float maxDistance = 0;
            int thisCounter = 0;
            float distance = 0;
            Vector3 headToNode = new Vector3(0f,0f,0f);
            Vector3 maxShoulderToNDir = new Vector3(0f,0f,0f);
            Vector3 shoulderToNDir = new Vector3(0f,0f,0f);
            Right = false;
            Left = false;
            // do everything in here
            // have a phase of climbing the wall by attaching to nodes
            // then when at top, where head y lines up with nodes that move horiz
            // then try to attach to those nodes
            // maybe try to raycast or sum to see if you can reach the node ,then pick furthest one
            Node currentNode = Grid.GetCurrentNode(Head.transform.position);
            Node otherNode = new Node();
            string keyPress = "";
            if(climbingArm == "right")
            {
                otherNode = Grid.GetCurrentNode(rArm.transform.position);
                hand = rHand;
                keyPress = "V";
            }
            else
            {
                otherNode = Grid.GetCurrentNode(lArm.transform.position);
                hand = lHand;
                keyPress = "X";
            }
            foreach(Node n in path)
            {
                //Debug.LogError("hello");
               //if(thisCounter > pathNodeCounter)
               // {
                    //shoulderToNDir = (n.position - otherNode.position).normalized;
                    headToNode = (n.position - otherNode.position).normalized;
                    distance = (n.position - otherNode.position).magnitude;
                //}
                //else
                //{
                    //thisCounter++;
                    //continue;
                //}
                //Debug.LogError(exactArmLength + "  " +  distance);
                // should work but setting the wallNode is legit ***REMOVED***ing ***REMOVED***
                // can't see itself halfthetime when reach top, = doesn't realize on top
                    // also doesn't realize anyway, if it's on top 
                if((distance > maxDistance && distance < exactArmLength) &&(n.y > currentNode.y || ((n.x > currentNode.x && playerDir == "right") || (n.x < currentNode.x&& playerDir == "left")))) //&& thisCounter > pathNodeCounter)
                {
                        shoulderToNDir = (n.position - currentNode.position);
                        RaycastHit2D checkColl = Physics2D.Raycast(currentNode.position, shoulderToNDir.normalized, shoulderToNDir.magnitude - 1f, mask);
                        if(checkColl.collider == null)
                        {
                            Debug.DrawRay(currentNode.position, headToNode * 100);
                            maxDistance = distance;
                            maxNode = n;
                            //Debug.LogError("maxD" + maxDistance);
                            maxShoulderToNDir = shoulderToNDir;
                        }   
                }
                thisCounter++;
            }
            int change = 0;
            if(playerDir == "left")
            {
                change = -1;
            }
            else
            {
                change = 1;
            }
            //Debug.LogError("kfdijfidjfijfijdfjdsvji");
            wallNode = Grid.grid[maxNode.x + change, maxNode.y];
            shoulderToWallDir = wallNode.position - otherNode.position;
            float yrOngl = Mathf.Round(Mathf.Atan2(shoulderToWallDir.x, shoulderToWallDir.y) * Mathf.Rad2Deg);
            simPress[keyPress] = true;
            tryingToGrab = true;
            foreach(_Muscle muscala in muscles)
            {
                if((muscala.bone.gameObject.tag == "lArm" && climbingArm == "left") || (muscala.bone.gameObject.tag == "rArm" && climbingArm == "right"))
                {
                    muscala.restRotation = -yrOngl + 180;
                }
            }

        }
        // doesn't seem to pivot fully
        // just doesn't set next node to climb to , and then doesn't 

        // not sure what's happening here, as if large force sends upwards
        // will have to sim keyPresses to get arm off;
        // i think everything happens too fast, so it's still pivoting on one arm when other get's activated
        // obvs we are using force in pull up so just launches it, will have to turn off pivoting once it has reached goal, then allow next arm to come
        if(!NowHoldingL && !NowHoldingR)
        {
            pivoting = false;
        }
        if(tryingToGrab)
        {
            if((Time.time - grabTimer > 5f) && HasCollidedWalk)
            {
                if(playerDir == "right")
                {
                    Right = true;
                }
                else
                {
                    Left = true;
                }
            }
            else
            {
                Right = false;
                Left = false;
            }

            // if(HasCollidedWalk && (Time.time - HasCollidedWalkTimer) > 2f &&tartWalkTimer == 0f)
            // {
            //     startWalkTimer = Time.time; 
            //     //Debug.LogError("lolget***REMOVED***ed");
            // }
            // else
            // {
            //     startWalkTimer = 0f;
            // }
            checkClimbing();
            Vector3 handToWallDir = (wallNode.position - Grid.GetCurrentNode(hand.transform.position).position).normalized;
            Vector3 bodyToWallDir = (wallNode.position - Body.transform.position).normalized;
            float yrOngl = Mathf.Round(Mathf.Atan2(handToWallDir.x, handToWallDir.y) * Mathf.Rad2Deg);
            if(grabTimer == 0f)
            {
                grabTimer = Time.time;
            }
            if((Time.time - grabTimer) > 3f && tryingToGrab)
            {
               yrOngl = -180f;
               handToWallDir = (new Vector3(hand.transform.position.x, hand.transform.position.y - 10, hand.transform.position.z) - hand.transform.position).normalized;
            }
            foreach(_Muscle muscala in muscles)
            {
                if((muscala.bone.gameObject.tag == "lArm" && climbingArm == "left") || (muscala.bone.gameObject.tag == "rArm" && climbingArm == "right"))                
                {
                    float multiplier = 0.3f * (muscala.bone.mass / (1/arm_length));
                    muscala.restRotation = -yrOngl + 180;
                    muscala.bone.AddForce(handToWallDir * multiplier, ForceMode2D.Impulse);

                }
                if(muscala.bone.gameObject.tag == "Body" && (NowHoldingL || NowHoldingR) && !sameArm)
                {
                    float multiplier = 300f * (muscala.bone.mass / body_mass);
                    if(!NowHoldingL && !NowHoldingR)
                    {
                        Debug.LogError("itsnotrugbymate");
                    }
                    if(bodyToWallDir.y < 0)
                    {
                        //multiplier = -multiplier;
                    }
                    muscala.bone.AddForce(new Vector2(0f, multiplier), ForceMode2D.Impulse);
                    Debug.LogError("hiimhere");
                }
            }
            if((Time.time - grabTimer) > 1f)
            {
                Debug.LogError("***REMOVED***yonmy***REMOVED***yyuh");
                simPress["T"] = true;
                if (((climbingArm == "right" && NowHoldingR)) || (climbingArm == "left" && NowHoldingL))
                {
                    pivoting = true;
                    dir = "Up";
                    Node currentNode = new Node();
                    if(climbingArm == "left")
                    {
                        currentNode = Grid.GetCurrentNode(lHand.transform.position);
                    }
                    else
                    {
                        currentNode = Grid.GetCurrentNode(rHand.transform.position);
                    }
                    int thisCounter = 0;
                    float leastDiff = Mathf.Infinity;
                    int ClosestPathNodeCounter = 0;
                    // foreach(Node n in path)
                    // {
                    //     if(thisCounter > pathNodeCounter)
                    //     {
                    //         int diffX = Mathf.Abs(currentNode.x - n.x);
                    //         int diffY = Mathf.Abs(currentNode.y - n.y);
                    //         if((diffX + diffY) < leastDiff)
                    //         {
                    //             leastDiff = diffX + diffY;
                    //             ClosestPathNodeCounter = thisCounter;
                    //             currentNode = n;
                    //         }
                    //     }
                    //     thisCounter++;
                    // }
                    //int outOfWall = -1;
                    Vector3 bodyToWall = wallNode.position - Body.transform.position;
                    // no idea wbat to do , so
                    //some time work sometime not
                    // i dont think the below works
                    // it works best when the arms whips around with enough force to hit the wall
                    // could try create the hinge joint before then pulling towards or something
                    if(!pivoting)
                    {
                        //body_rigid.AddForce(bodyToWall * (pivotingForceMultiplier * 0.5f), ForceMode2D.Impulse);
                    }
                    foreach(Node n in path)
                    {
                        int outOfWall = 0;
                        if(!Grid.grid[n.x,n.y].WalkAble)
                        {
                            outOfWall = -1;
                            if(playerDir == "left")
                            {
                                outOfWall = 1;
                            }
                        }
                        if (n.x == (wallNode.x + outOfWall) && n.y == wallNode.y && thisCounter > pathNodeCounter)
                        {
                            pathNodeCounter = thisCounter;
                            break;
                        }
                        thisCounter++;
                    }
                    if(someOtherTimer == 0f)
                    {
                        someOtherTimer = Time.time;
                    }
                    //Debug.LogError(climbingArm + "   " + NowHoldingL);
                    //Debug.LogError(Time.time - someOtherTimer);
                    if((Time.time - someOtherTimer)> 0.8f)
                    {
                     // int correct = 0;
                     // int sum = 0;
                     //    Component[] Distance = Body.GetComponents(typeof(DistanceJoint2D));
                     //    foreach(DistanceJoint2D dist in Distance)
                     //    {
                     //     if(dist.distance == 0.005f)
                     //    {
                     //            correct++;
                     //    }
                     //    sum++;
                     //    }
                     //    if(correct == sum)
                     //    {
                            Debug.LogError("wewonDawg");
                            sameArm = false;
                            tryingToGrab = false;
                            pivoting = false;
                            grabTimer = 0f;
                            someOtherTimer = 0f;
                        //}
                            // this almost works, but should prob extend the timer, as goes really fast
                            // i don't think it flies anymore but have to test
                            // other issue is arm extending
                            // have to do stuff for right side
                            // maybe get rid of the arm length ***REMOVED***, as it seemingly hasn't stopped extension
                    }
                }
                // if((Time.time - grabTimer) > 5f && tryingToGrab)
                // {
                //     if(climbingArm == "right" && NowHoldingL)
                //     {
                //         simPress["X"] = true;
                //     }
                //     else if(NowHoldingR && climbingArm == "left")
                //     {
                //         simPress["V"] = true;
                //     }
                //     sameArm = true;
                //     someOtherTimer = 0f;
                //     grabTimer = 0f;
                //     pivoting = false;
                // }
            }
        }
        // if(VaultPhase)
        // {
        //     if(lastDir == "right")
        //     {
        //         // muscleR.bone.AddForce(new Vector2(0f, 1500f), ForceMode2D.Impulse);
        //         // muscleL.bone.AddForce(new Vector2(0f, 1500f), ForceMode2D.Impulse);
        //         // rbBody.AddForce(new Vector2(1000f,0f), ForceMode2D.Impulse);
        //         // muscleL.bone.AddForce(new Vector2(0f, 1500f), ForceMode2D.Impulse);
        //         // muscleR.bone.AddForce(new Vector2(0f, 1500f), ForceMode2D.Impulse);
        //         float multiplier = 600f * (rbBody.mass / body_mass);
        //         rbBody.AddForce(new Vector2(0f, 800f), ForceMode2D.Impulse);
        //     }
        //     //VaultPhase = false;
        // }

        if(animations.Count > 0)
        {
            List<function> temp = animations;
            int newCounter = 0;
            function setThisFunc = animations[0];
            int index = 0;
            if(animations[0].notPriority)
            {
                foreach(function func in temp)
                {
                    if(!func.notPriority)
                    {
                        setThisFunc = func;
                        index = newCounter;
                        break;
                    }
                    if(func.name != animations[0].name)
                    {
                        setThisFunc = func;
                        index = newCounter;
                    }
                    newCounter++;
                }
                animations[0] = setThisFunc;
                if(newCounter>1)
                {
                    animations.RemoveAt(index);
                }
            }
            //Debug.LogError("fat***REMOVED***" + animations[0].name);
            //Debug.LogError("***REMOVED***" + animations[0].notPriority + animations[0].name + animations[0].started + animations[0].finished);
            if(!animations[0].started)
            {
                if(!animations[0].notPriority)
                {
                    priorityAnimating = true;
                }
                else
                {
                    priorityAnimating = false;
                }
                Invoke(animations[0].name,0f);
                animations[0].started = true;
            }
            //Debug.LogError("howyoudo" + animations[0].name + animations[0].finished);
            if(animations[0].finished)
            {
                //priorityAnimating = false;
                //Debug.LogError("iamhere" + animations[0].name + animations[0].finished);
                animations.RemoveAt(0);
            }
        }

        else
        {
            priorityAnimating = false;
        }
                //Debug.Log("hello");
        // this ***REMOVED*** is stupid as ***REMOVED***ing balls
        // it detects at such weird distances
        // and then like it won't detect after moving close like the f
        
        //Debug.DrawRay(Body.transform.position, new Vector3(rbHead.transform.position.x + 5, rbHead.transform.position.y, rbHead.transform.position.z) - rbHead.transform.position);
        if(!Player)
        {
            LayerMask mask = LayerMask.GetMask("Player");
            thisHeadToPlayerChest = playerPos - Head.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(Head.transform.position, thisHeadToPlayerChest, thisHeadToPlayerChest.magnitude, ~mask);
            //Debug.LogError("pl" + hit.collider.gameObject.tag);
            float othAngle = Vector3.Angle(Head.transform.right, thisHeadToPlayerChest);
            //Debug.LogError("othang" + othAngle);
            if(hit.collider == null && !playerStick.dead && othAngle < 35)
            {
                Debug.LogError("***REMOVED***as***REMOVED***" + thisHeadToPlayerChest.magnitude);
                if(!firing)
                {
                    timeAtLastShot = Time.time;
                }
                if(thisHeadToPlayerChest.magnitude > 10f && !Alerted)
                {
                    path = Grid.PathFind(Grid.GetCurrentNode(rFoot.transform.position), Grid.GetCurrentNode(playerStick.rFoot.transform.position));
                    Alerted = true;
                    lastPos = playerStick.rFoot.transform.position;
                }
                else
                {
                    Alerted = false;
                    firing = true;
                }
            }
            else if(hit.collider.gameObject.tag == "World")
            {
                firing = false;
            }
        }
        // no ***REMOVED***ng iidea what's happening here
        // on 4th ai, it never sets flying to true, implying player is true , but it's not
        // there is weird errors beofre it but on removal still no work
        // seriously no idea
        if(firing && !playerStick.dead)
        {
            timeSinceLastShot = Time.time - timeAtLastShot;
            lastPos = playerStick.rFoot.transform.position;
            float angle = Mathf.Round(Mathf.Atan2(thisHeadToPlayerChest.x, thisHeadToPlayerChest.y) * Mathf.Rad2Deg);
            Debug.LogError("cangle" + angle);
            foreach(_Muscle muscle in muscles)
            {
                if(gunInL && muscle.bone.gameObject.tag == "lArm" || gunInR && muscle.bone.gameObject.tag == "rArm")
                {
                    muscle.restRotation = -angle + 180;
                }
            }
            if(timeSinceLastShot > 0.8f)
            {
                shoot.Fire(angle,playerPos, false);
                timeAtLastShot = Time.time;
            }
        }
        // if(Alerted)
        // {
        //     if(thisHeadToChest)
        // }
        if(!pathFinding && !Player);
        {
            path = Grid.PathFind(Grid.GetCurrentNode(rFoot.transform.position), Grid.GetCurrentNode(lastPos));
        }
        if(!Player && !ClimbingPhase && !VaultPhase && (Time.time - timeSinceClimb > 3f))
        {
            // maybe use this actualOrigin, as means still work when close
            LayerMask mask = LayerMask.GetMask("World");
            LayerMask climbMask = LayerMask.GetMask("Climbable");
            Vector2 origin = new Vector2(Body.transform.position.x, Body.transform.position.y);
            Vector2 actualOriginDirLeft = new Vector2(Body.transform.position.x + 1, Body.transform.position.y);
            Vector2 actualOriginDirRight = new Vector2(Body.transform.position.x - 1, Body.transform.position.y);
            Vector2 origin2 = new Vector2(rFoot.transform.position.x, rFoot.transform.position.y + 2);
            Vector2 origin3 = new Vector2(Body.transform.position.x, rFoot.transform.position.y);;
            Vector2 origin4 = new Vector2(Body.transform.position.x, rFoot.transform.position.y + 2);
            Vector2 origin5 = new Vector2(Head.transform.position.x + 2, rFoot.transform.position.y);
            Vector2 origin6 = new Vector2(Head.transform.position.x + 2, rFoot.transform.position.y + 2);
            Vector2 di = ((new Vector2(Body.transform.position.x + 10, Body.transform.position.y)) - actualOriginDirRight).normalized;
            Vector2 di2 = ((new Vector2(Body.transform.position.x, Body.transform.position.y + 10)) - origin).normalized;
            Vector2 di3 = ((new Vector2(rFoot.transform.position.x + 10, rFoot.transform.position.y + 2)) - origin2).normalized;
            Vector2 di4 = ((new Vector2(Body.transform.position.x - 10, Body.transform.position.y)) - actualOriginDirLeft).normalized;
            Vector2 di5 = ((new Vector2(lFoot.transform.position.x - 10, rFoot.transform.position.y + 2)) - origin2).normalized;
            Vector2 di6 = (origin4 - origin3).normalized;
            Vector2 di7 = (origin6 - origin5).normalized;

            //float ang = Mathf.Atan2(di.x, di.y) * Mathf.Rad2Deg;
            //Debug.LogError("ang" + ang);
            //Vector3 dire = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position).normalized;
            //float ange = Mathf.Atan2(dire.x, dire.y) * Mathf.Rad2Deg;
            //Debug.LogError("ange" + ange);


            RaycastHit2D hit1 = Physics2D.CircleCast(origin,1.5f,di,3f,mask);
            RaycastHit2D hit2 = Physics2D.Raycast(origin,di2,6f,mask);
            RaycastHit2D hit3 = Physics2D.Raycast(origin2,di3,7f,mask);
            RaycastHit2D hit4 = Physics2D.CircleCast(origin,1.5f,di4,3f,mask);
            RaycastHit2D hit5 = Physics2D.Raycast(origin2,di5,7f,mask);
            RaycastHit2D hit6 = Physics2D.Raycast(origin3,di6,2f,mask);
            RaycastHit2D hit7 = Physics2D.Raycast(origin5,di7,2f,mask);
            RaycastHit2D hit1WithClimb = Physics2D.CircleCast(actualOriginDirRight,1.5f,di,2f,climbMask);
            RaycastHit2D hit4WithClimb = Physics2D.CircleCast(actualOriginDirLeft,1.5f,di4,2f,climbMask);
            Debug.DrawRay(origin,di4);
            Debug.DrawRay(origin, di4);

            // trouble getting up incline without the angle
            // 


            //Debug.Log(hit.collider.gameObject.tag);
            // it keeps rotating because doesn't know which way to go when getting called multiple times

            //have to implement way to get up afterwards, and should prob find a way to implement the hit2 again

            // also need to repeat all these raycasts in opp direction
            bool enteredAboveLoop = false;
            if(hit6.collider == null)
            {

                Debug.LogError("fatman");
            }
            else
            {
                Debug.LogError("blackman");
            }
            if((hit3.collider != null && playerDir == "right" )|| (hit5.collider != null && playerDir == "left") || hit6.collider != null)
            {
               Debug.LogError("flying"); 
                if(!proning)
                {
                    Debug.LogError("proning");
                    if(crouching)
                    {
                        //wasCrouched = true;
                        function funct1 = new function();
                        funct1.name = "GetUpAni";
                        funct1.animations = animations;
                        animations.Add(funct1);
                        crouching = false;  
                    }
                    proneDir = playerDir;
                    function funct2 = new function();
                    funct2.name = "StraightAni";
                    funct2.animations = animations;
                    animations.Add(funct2);
                }
                enteredAboveLoop = true;
            }
            else if(proning && hit6.collider == null)
            {
                Debug.LogError("entering");
                proning = false;
                function funct = new function();
                funct.name = "GetUpAni";
                funct.animations = animations;
                animations.Add(funct);
            }
            if(((hit1.collider != null && playerDir == "right" ) || (hit4.collider != null && playerDir == "left") || (hit2.collider != null )) && !proning && !enteredAboveLoop){
                Debug.LogError("lhi");
                    //Debug.LogError("lhiddd");
                    //Debug.LogError("crouch" + crouching);
                if(!crouching)
                {
                        //Debug.LogError("***REMOVED***");
                    wasCrouched = false;
                    CrouchAniDone = false;
                    function funct = new function();
                    funct.name = "CrouchAni";
                    funct.animations = animations;
                    animations.Add(funct);
                    crouching = true;
                }
                    //CrouchAni();
            }
            else if(crouching && hit2.collider == null )
            {
                wasCrouched = true;
                function funct = new function();
                funct.name = "CrouchAni";
                funct.animations = animations;
                animations.Add(funct);
                crouching = false;            
                //Debug.LogError("saint***REMOVED***an");
            }
            //Debug.LogError(path[0].x);
            if(Input.GetKeyDown(KeyCode.O))
            {
                firstNode = Grid.GetCurrentNode(rFoot.transform.position);
                path = Grid.PathFind(firstNode, Grid.GetCurrentNode((Camera.main.ScreenToWorldPoint(Input.mousePosition))));
                pathNode = path[1];
                currentNode = path[0];
                pathNodeCounter = 1;
                //Debug.LogError(path.Coude = path[0];
                counter = 0;
            }
            currentNode = Grid.GetCurrentNode(Body.transform.position);
            Node lFootNode = Grid.GetCurrentNode(lFoot.transform.position);
            Node rFootNode = Grid.GetCurrentNode(rFoot.transform.position);
            Node forwardFootNode = new Node();
            if(playerDir == "right")
            {
                forwardFootNode = rFootNode;
            }
            else
            {
                forwardFootNode = lFootNode;
            }
            if(!lFootNode.WalkAble)
            {
                lFootNode = Grid.grid[lFootNode.x, lFootNode.y + 1];
                //Debug.LogError("whomamamdis");
            }
            if(!rFootNode.WalkAble)
            {
                rFootNode = Grid.grid[rFootNode.x, rFootNode.y + 1];
                //Debug.LogError("whosondis");
            }
            pathNode = path[pathNodeCounter];
            // Debug.LogError("forward" + forwardFootNode.x);
            // Debug.LogError("path" + pathNode.x);
            // Debug.LogError("something" + path[pathNodeCounter].x);
            if(path.Count > 0)
            {
                pathFinding = true;



                if((path[path.Count - 1].x == currentNode.x || path[path.Count - 1].x == rFootNode.x || path[path.Count - 1].x == lFootNode.x) && (path[path.Count - 1].y == rFootNode.y || path[path.Count - 1].y == lFootNode.y || path[path.Count - 1].y == rFootNode.y + 1 || path[path.Count - 1].y == lFootNode.y + 1))
                {
                    pathNodeCounter = path.Count;
                }
                //Debug.LogError("Nandead");
                int counter2 = 0;
                // this ***REMOVED*** is actually ***REMOVED***
                // all horiz is fine apart from the fact it can have problems on end of weird geometry because can't make path because rules
                // but this start like 20 metres out from jump for whatever ***REMOVED***ing reason
                // limiting restirciton does legit nothing  
                // sometimes it just doesn't jump at all tho so weird
                // also the walking kinda weird now as has collided walk only has to be true on one fooit
                if(rFootNode.WalkAble && lFootNode.WalkAble)
                {


                    if(pathNodeCounter < path.Count)
                    {
                        //Debug.LogError(path.Count);
                        counter2 = 0;
                        if(pathNode.x < forwardFootNode.x || (forwardFootNode.x == pathNode.x && lastDir == "left"))
                        {
                            Right = false;
                            Left = true;
                        }
                        else if (pathNode.x > forwardFootNode.x || (forwardFootNode.x == pathNode.x && lastDir == "right"))
                        {
                            Right = true;
                            Left = false;
                        }
                        else
                        {
                            Right = false;
                            Left = false;
                        }
                        if(pathNodeCounter < path.Count - 1)
                        {


                            if(path[pathNodeCounter + 1].y > (rFootNode.y + 1) || path[pathNodeCounter + 1].y > (lFootNode.y + 1))
                            {
                                //jump = true;
                                //Debug.LogError("Pathnod" + pathNode.y);
                                //Debug.LogError("PlayerNode" + rFootNode.y + " " + lFootNode.y);
                                foreach(Node nodes in path)
                                {
                                    // these raycast are prob too long
                                    //nodes.x == pathNode.x && nodes.y >= currentNode.y &&
                                    if((hit1WithClimb.collider != null || hit4WithClimb.collider != null))
                                    {
                                        ClimbingPhase = true;
                                        counter2++;
                                    }

                                }
                                if(counter2 == 0 && HasCollidedJump && !pathNode.Incline && Time.time - someTimer1 > 3f)
                                {
                                    someTimer1 = Time.time;
                                    if(lastDir == "right")
                                    {
                                        muscleR.bone.AddForce(new Vector2(0f, 1500f), ForceMode2D.Impulse);
                                        muscleL.bone.AddForce(new Vector2(0f, 1500f), ForceMode2D.Impulse);
                                        rbBody.AddForce(new Vector2(1000f,0f), ForceMode2D.Impulse);
                                    }
                                    else
                                    {
                                        muscleL.bone.AddForce(new Vector2(0f, 1500f), ForceMode2D.Impulse);
                                        muscleR.bone.AddForce(new Vector2(0f, 1500f), ForceMode2D.Impulse);
                                        rbBody.AddForce(new Vector2(-1000f,0f), ForceMode2D.Impulse);
                                    }
                                    ClimbingPhase = false;
                                }
                                Flying = true;

                                //Flying = true;
                            }
                            else
                            {
                                ClimbingPhase = false;
                            }
                        }
                        else
                        {
                            Flying = false;
                            ClimbingPhase = false;
                        }
                    }
                    else
                    {
                        Debug.LogError("dieonmtgrave");
                        Right = false;
                        //jump = false;
                        Left = false;
                    }
                    int count = 0;
                    foreach(Node nodes in path)
                    {
                        if((nodes.x == rFootNode.x || nodes.x == lFootNode.x)&& (nodes.y == lFootNode.y || nodes.y == rFootNode.y || nodes.y == lFootNode.y + 1 || nodes.y == rFootNode.y + 1|| nodes.y == lFootNode.y - 1|| nodes.y == rFootNode.y - 1))
                        {
                            if(count  < path.Count - 1 && count + 1 > pathNodeCounter && nodes.WalkAble)
                            {
                                pathNode = path[count + 1];
                                pathNodeCounter = count + 1;
                            }
                        }
                        count++;
                    }
                }
        }
        else
        {
            pathFinding = false;
        }
        // all ai ***REMOVED*** seems fine now, can go under and crawl and crouch(i think)
        // now that foot has angle it's doing the weird not walk thing again on inclines, when transitioning from 2 diff surface
        // otherwise after that fixed just sure up the walking and then do the jumping

            // solution above is stupid af, but don#t know ho;else to fix
            // the problem is that he sometimes just stops moving after dropping down, depending on the height, leading me to believe that the node is getting set to the right  one or
            // otherwise everything should be good, so now climbing
            // doesn't even work lol ***REMOVED*** off you little ***REMOVED***ing ***REMOVED***, why does it depend on height, but then allowing leniency changes nothing, ***REMOVED***

            
            //nodes.x == rFootNode.x || nodes.x == lFootNode.x )

            // so all horiz good, except still stick when moving off stuff
            // i think when doing y, we should check if > nodeoffeet.y but not < body.y, then just jump forward and fly
            // else if > body then start some sort of climb by putting hands on the nodes point
            // also have to figure out crouching and ***REMOVED***
            // aw ***REMOVED*** this might not work because the y would start at feet
            // maybe check if there's a node at x and body y, if not then can do the jump ***REMOVED***, otherwise climb
        
            // x movement should be ok
            // but now for the real challenge of y movement
            // the problem is that i dont know how to refernce the body's y position, as the pathfind is obvsiously right at feet when walking
            // but when climbing, they'd be like parallel
            // maybe minus a little from body y when not climbing, then remove this when you are
          //  if(jump)
          //  {
              //  if(player.y == currentNode.y + 3)
              //  {
                   // counter++;
                   // pathNode = path[counter + 1];
                   // currentNode = path[counter];
                //}
            //}
        }
    }
    // works pretty well, could delay it slightly, as might change to force at which pushed by bullet
    // remeber bullet impact sounds are off because ***REMOVED***
    // next get ai to shoot, and maybe fix walking sounds
    else if(!deadBodySet)
    {
        rFootBody.mass = 1;
        lFootBody.mass = 1;
        BodyRigid.mass = 1000;
        BodyRigid.gravityScale = 1f;
        rFootBody.gravityScale = 1f;
        lFootBody.gravityScale = 1f;
        foreach(_Muscle musc in muscles)
        {
            musc.bone.gravityScale = 1f;
            musc.bone.drag = 0f;
        }
        deadBodySet = true;
    }
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
        // so the iddea will be to check once the body's distance has hit it's min(have to check this)
        // once this happens, we stop pivoting and start with next arm
        // we'd do this above, not here tin the function
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
        Debug.LogError("wisemanoncetoldmeputyourfootonthegas");
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
                if(muscle.bone)
                {
                    muscle.bone.AddForce(JumpVector, ForceMode2D.Impulse);
                }
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
        //Debug.Log("whatdidyoudoonthissaturaday");
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
        if(!crouching)
        {
            if (playerDir == "right")
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
                if(propelFunct.finished)
                {
                    foreach(_Muscle muscle in legs)
                    {
                        muscle.bone.AddForce(new Vector2(240f, 190f), ForceMode2D.Impulse);
                        floating = true;
                    }
                    function funct1 = new function();
                    proneDir = playerDir;
                    funct1.name = "StraightAni";
                    funct1.animations = animations;
                    animations.Add(funct1);
                    propelling = false;
                }
                else if(!propelling)
                {

                
                    Debug.LogError("loldawg");
                    propelFunct = new function();
                    propelFunct.name = "Propel";
                    propelFunct.animations = animations;
                    animations.Add(propelFunct);
                }
            }

            else if(playerDir == "left")
            {
                if(propelFunct.finished)
                {
                    foreach(_Muscle muscle in legs)
                    {
                        muscle.bone.AddForce(new Vector2(-240f, 190f), ForceMode2D.Impulse);
                        floating = true;
                    }
                    function funct2 = new function();
                    proneDir = playerDir;
                    funct2.name = "StraightAni";
                    funct2.animations = animations;
                    animations.Add(funct2);
                    propelling = false;
                }
                    

                else if(!propelling)
                {

                    propelFunct = new function();
                    propelFunct.name = "Propel";
                    propelFunct.animations = animations;
                    animations.Add(propelFunct);
                }
            }           
        }
    }
    //uppper body face direction of the mouse?
    // so test ai on more complex dips like box to below dip
    // still don't have a prone feature on control
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
        Debug.LogError("Indisbith");
        if(!proning)
        {
            Debug.LogError("***REMOVED***ass " + wasCrouched);
            if(((body_muscle.restRotation == 90 && playerDir == "right") || (body_muscle.restRotation == -90 && playerDir == "left")))
            {
                swivel = true;
            }
            if((!wasCrouched || swivel))
            {
                //Debug.LogError("***REMOVED***");
                animating = true;
                if(swivel)
                {
                    interval = 0.00001f;
                }
                else
                {
                    interval = 0.0001f;
                }
                if(playerDir == "left")
                {
                    increment = 1;
                    target = 90;
                }
                else
                {
                    increment = -1;
                    target = -90;
                }
            }
            else
            {
                if(playerDir == "left")
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
            if(body_muscle.restRotation != target || head_muscle.restRotation != target)
            {
                if(body_muscle.restRotation != target)
                {
                    body_muscle.restRotation += increment;
                }
                if(head_muscle.restRotation != target)
                {
                    head_muscle.restRotation += increment;
                }
                
                //RShoulderMuscle.restRotation += increment;
                //LShoulderMuscle.restRotation += increment;
                //Debug.LogError("target" + target);
                Invoke("CrouchAni",interval);
            }
            else
            {
                CrouchAniDone = true;
                animating = false;
                animations[0].finished = true;
            }
        }


    }
    // doesn't siwvel across, check for crouch on change direction

    // legs get crossed = hard to walk
    // otherwise seems good, but definely doesn't get real low

    public void GetUpAni()
    {   
        int counter1 = 0;
        int counter2 = 0;
        Debug.LogError("***REMOVED***afato;nee");
        resettingRot = true;
        animations = new List<function>();
            foreach (_Muscle muscle in muscles)
            {   
                if(muscle.bone.gameObject.tag != "rArm" && muscle.bone.gameObject.tag != "lArm")
                {
                    if(muscle.bone.gameObject.tag == "rFoot" || muscle.bone.gameObject.tag == "lFoot")
                    {
                        muscle.restRotation = 0;
                        counter1++;
                    }
                    else if(muscle.restRotation > 0)
                    {
                        muscle.restRotation = muscle.restRotation - 1;
                    }

                    else if(muscle.restRotation < 0)
                    {
                        muscle.restRotation = muscle.restRotation + 1 ;
                    }

                    else
                    {
                        counter1++;
                    }    
                    counter2++;
                }
            }
            
            if (counter1 != counter2 )
            {
                Invoke("GetUpAni", 0.01f);
            }
            else
            {
                resettingRot = false;
                proning = false;
                crouching = false;

            }
        //animations[0].finished = true;
    }

    public void StraightAni() {
        int counter1 = 0;
        int counter2 = 0;
        animating = true;
        proning = true;
        if(animations.Count > 0)
        {
                Debug.LogError("neek" +animations[0].name + animations[0].finished);
            if(animations[0].name == "StraightAni")
            {
                if (proneDir == "left")
                {

                
                    foreach(_Muscle muscle in muscles)
                    {
                            if(muscle.bone.gameObject.tag != "rArm" && muscle.bone.gameObject.tag != "lArm")
                            {
                                if(Mathf.Round(muscle.restRotation) == 90)
                                {
                                    counter1++;
                                }
                                else
                                {
                                    muscle.restRotation += 1;
                                }
                                counter2++;
                            }
                    }
                    if (counter1 != counter2)
                    {
                        Invoke("StraightAni", 0.01f);
                    }
                    else
                    {
                        animations[0].finished = true;
                    }
                                
                }
                else
                {
                    foreach(_Muscle muscle in muscles)
                    {
                            if(muscle.bone.gameObject.tag != "rArm" && muscle.bone.gameObject.tag != "lArm" )
                            {
                                if(Mathf.Round(muscle.restRotation) == -90)
                                {
                                    counter1++;
                                }
                                else
                                {
                                    muscle.restRotation -= 1;
                                }
                                counter2++;
                            }
                    }
                    if (counter1 != counter2)
                    {
                        Invoke("StraightAni", 0.01f);
                    }
                    else
                    {
                        animations[0].finished = true;
                    }
                }
            }
            Debug.LogError("weidiit");
            //animations[0].finished = true;
        }
    }

    public void Propel()
    {
        int counter2 = 0;
        int counter1 = 0;
        Debug.LogError("propeller");
        propelling = true;
        if(playerDir == "right")
        {
            Debug.LogError("yes");

            
            foreach(_Muscle muscle in muscles)
            {
                // consider this for leaning the body
                // the body violently snaps toward the object when pronin and trying to stand, mayebl less force on muscles or sum, as it's not from ani as all muscles already at 0
                // also could try re-imp the collider2
                //body_muscle.bone.AddForce(new Vector2(100f,0f), ForceMode2D.Impulse);
                Debug.LogError("ye");
                if(muscle.bone.gameObject.tag != "rArm" && muscle.bone.gameObject.tag != "lArm")
                {

                    if(muscle.restRotation == -20)
                    {
                        counter1++;
                    }
                    else
                    {
                        if(muscle.restRotation > -20)
                        {
                            muscle.restRotation -= 1;
                        }
                        else
                        {
                            muscle.restRotation += 1;
                        }
                    }
                    counter2++;
                }
            }
            if (counter1 != counter2)
            {
                Invoke("Propel", 0.0000000001f);
            }

            else 
            {
                 Debug.LogError("gonkms");
                animations[0].finished = true;
                //propelling = false;
                Fly();
            }
        }

        else
        {
            //Debug.LogError("y");
            foreach(_Muscle muscle in muscles)
                {
                    if(muscle.bone.gameObject.tag != "rArm" && muscle.bone.gameObject.tag != "lArm")
                    {
                        if(muscle.restRotation == 20)
                        {
                            counter1++;
                        }
                        else
                        {
                            if(muscle.restRotation > 20)
                            {
                                muscle.restRotation -= 1;
                            }
                            else
                            {
                                muscle.restRotation += 1;
                            }
                        }
                        counter2++;
                    }
                }
                if (counter1 != counter2)
                {
                    Invoke("Propel", 0.0001f);
                }

                else 
                {
                    //propelling = false;
                    animations[0].finished = true;
                    Fly();
                }
        }
    }

    public void Recoil()
    {
    }
    public void BodyToMouse()
    {
        if(animations.Count > 0)
        {
            if(animations[0].name == "BodyToMouse")
            {
                if(body_muscle.restRotation < -angle)
                {
                    body_muscle.restRotation += 1;
                }
                else if(body_muscle.restRotation > -angle)
                {
                    body_muscle.restRotation -= 1;
                }
                if(body_muscle.restRotation != -angle)
                {
                    Invoke("BodyToMouse", 0.1f);
                }
                else
                {
                    animations[0].finished = true;
                }
            }
        }
    }
public void aim()
{
        int counter1 = 0;
        int counter2 = 0;
        if(animations.Count > 0)
        {
            if(animations[0].name == "aim")
            {
                foreach(_Muscle muscle in muscles)
                {
                    if(muscle.bone.gameObject.tag == "rArm")
                    {
                        //if(holdingL)
                       // {
                           // muscle.bone.AddForce(direction / 5, ForceMode2D.Impulse);
                       // }
                        if(muscle.restRotation == angle)
                        {
                            counter1++;
                        }
                        else
                        {
                            if(muscle.restRotation > -angle + 180)
                            {
                                muscle.restRotation -= 1;
                            }
                            else
                            {
                                muscle.restRotation += 1;
                            }
                        }
                        counter2++;
                    }
                }
                if(counter1 != counter2)
                {
                    Invoke("aim", 0.1f);
                }
                else
                {
                    animations[0].finished = true;
                }
            }
        }
    }

    public void checkClimbing()
    {
        Vector3 FeetForward;
        RaycastHit2D coll;
        LayerMask mask = LayerMask.GetMask("Climbable");
        if(playerDir == "right")
        {
            FeetForward = new Vector3(rFoot.transform.position.x + 10, rFoot.transform.position.y, rFoot.transform.position.z) - rFoot.transform.position;
            coll = Physics2D.Raycast(rFoot.transform.position, FeetForward,2f,mask);
        }
        else
        {
            FeetForward = new Vector3(lFoot.transform.position.x - 10, lFoot.transform.position.y, rFoot.transform.position.z) - lFoot.transform.position;
            coll = Physics2D.Raycast(lFoot.transform.position, FeetForward, 2f, mask);
        }
        if(HasCollidedWalk && coll.collider == null) //&& (Time.time - ClimbingTimer > 8f))
        {
            if(canStandFromClimbTimer == 0f)
            {
                canStandFromClimbTimer = Time.time;
            }
            if(Time.time - canStandFromClimbTimer > 1f)
            {
                timeSinceClimb = Time.time;
                canStandFromClimbTimer = 0f;
                tryingToGrab = false;
                ClimbingTimer = 0f;
                pivoting = false;
                if(NowHoldingR || holdingR)
                {
                    simPress["V"] = true;
                }
                else if(NowHoldingL || holdingL)
                {
                    simPress["X"] = true;
                }
                foreach(_Muscle musc in muscles)
                {
                    if(musc.bone.gameObject.tag == "rArm" || musc.bone.gameObject.tag == "lArm")
                    {
                        musc.restRotation = 0;
                    }
                }
                ClimbingPhase = false; 
            }                 
        }
        else
        {
            canStandFromClimbTimer = 0f;
        }

    }
    public void StopAnyRightArm()
    {
        if(NowGrabbingR)
        {
            DropGun(rHand);
            NowGrabbingR = false;
            Debug.LogError("BALLISLIFE");
        }
        if(NowHoldingR)
        {
            StopHold(rHand, "RightA");
        }
    }
    public void StopAnyLeftArm()
    {
        if(NowGrabbingL)
        {
            DropGun(lHand);
            NowGrabbingL = false;
            //Debug.LogError("BALLISLIFE");
        }
        if(NowHoldingL)
        {
            StopHold(lHand, "LeftA");
        }
    }
    public void MultiplyWalkVector()
    {
        if(Sprinting)
        {
            Sprinting = false;
            WalkRightVector.x = WalkRightVector.x / SprintMultiplier;
            WalkLeftVector.x =  WalkLeftVector.x / SprintMultiplier ;
        }
        else
        {
            Sprinting = true;
            WalkRightVector.x *= SprintMultiplier;
            WalkLeftVector.x *= SprintMultiplier;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(wallNode.position, Vector3.one * (1));
        Gizmos.color = Color.red;
        Gizmos.DrawCube(path[pathNodeCounter].position, Vector3.one);
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(Grid.GetCurrentNode(rFoot.transform.position).position, Vector3.one);
        Gizmos.DrawCube(Grid.GetCurrentNode(lFoot.transform.position).position, Vector3.one);
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
                    //restRotation = -stick.rLegRestRotation ;

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
                    //restRotation = -stick.lLegRestRotation;
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

public class function
{
    public string name;
    public bool finished = false;
    public bool started = false;
    public List<function> animations;
    public bool notPriority;
}
