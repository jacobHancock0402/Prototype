
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
//[ExecuteInEditMode]
public class Rope : MonoBehaviour {
    public float length;
    public bool wasGrabbed = false;
    public int oldLength;
    public GameObject gunSetup;
    public GameObject chainPrefab;
    public Transform NewestChild;
    public Rigidbody2D NewestBody;
    public Stickman stick;
    public AudioSource audio;
    //public Gun Gun;
    public float leg_changex = 5f;
    public float leg_changey = 5f;
    public GameObject footPrefab;
    public Rigidbody2D oldestBody;
    public Rigidbody2D body;
    public Rigidbody2D head;
    public Color limb_colour;
	void Start() {
        if(stick)
        {
            gunSetup = new GameObject("spawner");
            body = stick.transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>();
            head = stick.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>();
            body.mass = stick.body_mass;
            head.mass = stick.head_mass;
            stick.rbBody = gameObject.transform.root.GetChild(0).GetComponent<Rigidbody2D>();
            stick.rArmList = new List<GameObject>();
            stick.lArmList = new List<GameObject>();
            stick.rLegList = new List<GameObject>();
            stick.lLegList = new List<GameObject>();
            audio = GetComponent<AudioSource>();
        }

        if(gameObject.transform.childCount == 0)
        {
            for(int i=0;i<length;i++)
            
            {  
                int count = gameObject.transform.childCount - 1;
                if (!NewestChild || NewestChild.transform.parent.gameObject.GetInstanceID() != gameObject.GetInstanceID())
                {
                    NewestChild = gameObject.transform;
                    NewestBody = gameObject.GetComponent<Rigidbody2D>();
                }
                GameObject link = null;
                Debug.LogError("distraction");
                Debug.LogError("itsmyfault");
                if((gameObject.tag == "rLeg" || gameObject.tag == "lLeg" ) && i == length - 1)
                {
                    link = Instantiate(footPrefab) as GameObject;
                    if(gameObject.tag == "rLeg")
                    {
                        link.tag = "rFoot";
                        stick.rFoot = link;
                    }
                    else
                    {
                        link.tag = "lFoot";
                        stick.lFoot = link;
                    }
                }
                else
                {
                    link = Instantiate(chainPrefab) as GameObject;
                    link.GetComponent<SpriteRenderer>().color = limb_colour;
                }
                BodyColl bColl = link.AddComponent<BodyColl>();
                Debug.LogError("thinktimeithinkitsforreal");
                bColl.stick = stick;
                BoxCollider2D collider = link.GetComponent<BoxCollider2D>();
                float multiplier = 0.5f;
                if((gameObject.tag == "rLeg" || gameObject.tag == "lLeg" ) && i == length - 1)
                {
                    multiplier = 1f;
                }
                link.transform.position = new Vector3(
                gameObject.transform.position.x, 
                NewestChild.position.y -(multiplier * Mathf.Abs(collider.bounds.max[1] - collider.bounds.min[1])), 
                gameObject.transform.position.z);
                link.transform.SetParent(gameObject.transform);
                Rigidbody2D body = link.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
                body.mass = 0.1f;
                body.interpolation = RigidbodyInterpolation2D.Interpolate;
                if(i==0)
                {
                    oldestBody = body;
                }
                HingeJoint2D connection = link.AddComponent(typeof(HingeJoint2D)) as HingeJoint2D;
                connection.connectedBody = NewestBody;
                // DistanceJoint2D connect = link.AddComponent(typeof(DistanceJoint2D)) as DistanceJoint2D;
                // connect.connectedBody = gameObject.GetComponent<Rigidbody2D>();
                // connect.maxDistanceOnly = true;
                // connect.autoConfigureDistance = false;
                if(gameObject.tag == "rLeg" || gameObject.tag == "lLeg" || gameObject.tag == "Rope")
                {
                    connection.enableCollision = true;
                }
                if(gameObject.tag == "lArm" || gameObject.tag == "rArm")
                {
                    connection.enableCollision = true;
                    //body.freezeRotation = true;
                    //JointAngleLimits2D limit = connection.limits;
                    //limit.min = -90;
                    //limit.max = 0;
                    //connection.limits = limit;
                    //connection.useLimits = false;
                    //body.inertia = 0.0000001f;
                }
                // aight so looking much better like actual legs collapsing from knees
                // but still collapsing = bad
                // stance too narrow = inbalance?
                // increasing width of prefab will fix the issue but makes legs look weird
                // increasing mass and making even might help i.e 1 mass on every fab
                // feet don't seem to do much
                // if keep, still need to figure out how to do walking with
                NewestChild = link.transform;
                NewestBody = body;
                // not adding to the arra
                // not walking right direction
                // too large force
                // like flying but might be because vector too large as said above
                if(stick)
                {
                    _Muscle muscle = new _Muscle();
                    muscle.restRotation = 0;
                    float value = 1/length;
                    Debug.Log(value);
                    Debug.Log(1/length);
                    Debug.Log(length);
                    if(gameObject.tag == "rArm" || gameObject.tag == "lArm") 
                    {
                        //muscle.restRotation = 90;
                        muscle.force = 100;
                        body.mass = 1 / length;
                        gameObject.GetComponent<Rigidbody2D>().mass = 1/length;
                        Debug.Log(1/length);
                        body.gravityScale = 0;
                        if(gameObject.tag == "rArm")
                        {
                            //Debug.LogError("ohhshitherewegoagain");
                            stick.rArmList.Add(link);
                        }
                        else
                        {
                            stick.lArmList.Add(link);
                        }
                        //freezing rotation bad as means muscle doesn't work as it moves rotationaaa
                        //body.freezeRotation = true;
                    }
                    else
                    {
                        muscle.force = 150;
                        if(link.tag == "rFoot" || link.tag == "lFoot")
                        {
                            body.mass = stick.foot_mass;
                            if(link.tag == "rFoot")
                            {
                                stick.rFootBody = body;
                                stick.rLegList.Add(link);
                            }
                            else
                            {
                                stick.lFootBody = body;
                                stick.lLegList.Add(link);
                            }

                        }
                        else
                        {
                            if(gameObject.tag == "rLeg")
                            {
                                stick.rLegList.Add(link);
                            }
                            else
                            {
                                stick.lLegList.Add(link);
                            }
                            link.tag = gameObject.tag;
                            body.mass = stick.leg_mass;
                            gameObject.GetComponent<Rigidbody2D>().mass = stick.leg_mass;
                        }
                        // leaning him forward might cause instability as his legs hit floor = trips, perhaps lean back?
                        // increase these values for more stability but less smoothness
                        // also do same for body as i have already done
                        // remember that must increase vectors if do though
                        // decent stability right now, not perfect but like the idea of player having to look at how walking and b patient?
                        // a lot of cleaning up with arms reset and shits
                        // hold break is fine, and setting parent is aight , not sure if works with gun tho
                        // after that just have to fix the pulling up shit and try get moves to work
                        // also lot of fine tunign, grapple gun and crouch?
                    }
                    muscle.bone = body;
                    muscle_holder refer = link.AddComponent(typeof(muscle_holder)) as muscle_holder;
                    refer.muscle = muscle;
                    // if(link.tag == "rFoot")
                    // {
                    //     stick.muscleR = muscle ;
                    // }
                    // else if(link.tag == "lFoot")
                    // {
                    //     stick.muscleL = muscle;
                    // }
                    Array.Resize(ref stick.muscles, stick.muscles.Length + 1);
                    stick.muscles[stick.muscles.Length - 1] = muscle;
                    if(gameObject.tag == "rLeg" || gameObject.tag == "lLeg") 
                    {
                        Array.Resize(ref stick.legs, stick.legs.Length + 1);
                        stick.legs[stick.legs.Length - 1] = muscle;
                    }
                    if(i == length - 1)
                {
                    if(gameObject.tag == "rLeg")
                    {
                        stick.muscleR = muscle ;
                    }
                    else if(gameObject.tag == "lLeg")
                    {
                        stick.muscleL = muscle;
                    }
                }

                    NoCollision nocoll = link.AddComponent(typeof(NoCollision)) as NoCollision;
                
                    if(gameObject.tag == "rLeg")
                    {
                        stick.rLegMuscleList.Add(muscle);
                        Array.Resize(ref stick.rbRIGHT, stick.rbRIGHT.Length + 1);
                        stick.rbRIGHT[stick.rbRIGHT.Length - 1] = body;
                        if(i<(length/2))
                        {
                            Array.Resize(ref stick.upperLeg, stick.upperLeg.Length + 1);
                            stick.upperLeg[stick.upperLeg.Length - 1] = muscle;
                        }
                        if(i==0)
                        {
                            //stick.muscleR = muscle;
                        }
                    }
                    else if(gameObject.tag == "lLeg")
                    {
                        stick.lLegMuscleList.Add(muscle);
                        Array.Resize(ref stick.rbLEFT, stick.rbLEFT.Length + 1);
                        stick.rbLEFT[stick.rbLEFT.Length - 1] = body;
                        //if(i<(length/2))
                        //{
                            //link.tag = "LlLeg";
                        //}
                        if(i==0)
                        {
                            //stick.muscleL = muscle;
                        }
                    }
                    else if(gameObject.tag == "rArm")
                    {
                        Array.Resize(ref stick.rbARIGHT, stick.rbARIGHT.Length + 1);
                        stick.rArmMuscleList.Add(muscle);
                        stick.rbARIGHT[stick.rbARIGHT.Length - 1] = body;
                        link.tag = "rArm";
                        if(i == length - 1)
                        {
                            CheckGrab grab = link.AddComponent(typeof(CheckGrab)) as CheckGrab;
                            grab.Stick = stick;
                            //grab.Gun = Gun;
                            stick.rHand = link;
                        }
                    }
                    else if(gameObject.tag == "lArm")
                    {
                        stick.lArmMuscleList.Add(muscle);
                        Array.Resize(ref stick.rbALEFT, stick.rbALEFT.Length + 1);
                        stick.rbALEFT[stick.rbALEFT.Length - 1] = body;
                        link.tag = "lArm";
                        if(i == length - 1)
                        {
                            CheckGrab grab = link.AddComponent(typeof(CheckGrab)) as CheckGrab;
                            grab.Stick = stick;
                            //grab.Gun = Gun;
                            stick.lHand = link;
                        }
                    }
                    if(i==0)
                    {
                        if(gameObject.tag == "lArm")
                        {
                            stick.LShoulderMuscle = muscle;
                        }
                        if(gameObject.tag == "rArm")
                        {
                            stick.RShoulderMuscle = muscle;
                        }
                    }
                }
                // this is not in use i believe
                if (i == length - 1) 
                {
                    //DistanceJoint2D distance = link.AddComponent(typeof(DistanceJoint2D)) as DistanceJoint2D;
                    //distance.connectedBody = oldestBody;
                    //distance.autoConfigureDistance = true;
                    //distance.maxDistanceOnly = true;
                    gunSetup.transform.position = new Vector3(link.transform.position.x, link.transform.position.y, link.transform.position.z);
                    gunSetup.transform.parent = link.transform;
                    if(gameObject.tag == "rLeg" || gameObject.tag == "lLeg")
                    {
                    //DistanceJoint2D d = link.AddComponent<DistanceJoint2D>();
                    //d.connectedBody = gameObject.GetComponent<Rigidbody2D>();
                        Vector2 origin = gameObject.transform.position;
                        origin.x = origin.x + leg_changex;
                        origin.y = origin.y + leg_changey;
                        Vector2 dir = new Vector2(origin.x,5f);
                        RaycastHit2D ray = Physics2D.Raycast(origin,dir,500f);
                        Debug.DrawRay(origin,dir);
                        Collided collide = link.AddComponent(typeof(Collided)) as Collided;
                        collide.Audio = audio;
                        // change accepted tags here to get other guns accepted
                        collide.stick = stick;
                        
                        if (gameObject.tag == "rLeg")
                        {
                            stick.rayL = ray;
                            stick.rFootCollided = collide;
                        }
                        else
                        {
                            stick.rayR = ray;
                            stick.lFootCollided = collide;
                        }
                    }
                    else if(gameObject.tag == "rArm" || gameObject.tag == "lArm")
                    {
                        GameObject child = gameObject.transform.GetChild(0).gameObject;
                        if(child.tag == "Gun")
                        {
                            //child.transform.SetParent(link.transform,false);
                            //FollowPos pos = child.AddComponent(typeof(FollowPos)) as FollowPos;
                            //pos.target = link;
                            child.transform.parent = gunSetup.transform;
                        }
                    }
            }

    	}
    }
}

    void Update()
    {
    }
}

