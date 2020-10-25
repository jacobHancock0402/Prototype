
using UnityEngine;
using System.Collections;
using System;

public class Rope : MonoBehaviour {
    public int length;
    public int oldLength;
    public GameObject chainPrefab;
    public Transform NewestChild;
    public Rigidbody2D NewestBody;
    public Stickman stick;
    public Gun Gun;
    public float leg_changex = 5f;
    public float leg_changey = 5f;
    public GameObject footPrefab;
    public Rigidbody2D oldestBody;
	void Start() {
        for(int i=0;i<length;i++)
        
        {   
            int count = gameObject.transform.childCount - 1;
            if (!NewestChild)
            {
                NewestChild = gameObject.transform;
                NewestBody = gameObject.GetComponent<Rigidbody2D>();
            }
            GameObject link = null;
            if((gameObject.tag == "rLeg" || gameObject.tag == "lLeg" ) && i == length - 1)
            {
                link = Instantiate(footPrefab) as GameObject;
            }
            else
            {
                link = Instantiate(chainPrefab) as GameObject;
            }
            BoxCollider2D collider = link.GetComponent<BoxCollider2D>();
            link.transform.position = new Vector3(
            gameObject.transform.position.x, 
            NewestChild.position.y - Mathf.Abs(collider.bounds.max[1] - collider.bounds.center[1]), 
            gameObject.transform.position.z);
            link.transform.SetParent(gameObject.transform);
            Rigidbody2D body = link.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
            body.mass = 0.1f;
            if(i==0)
            {
                oldestBody = body;
            }
            HingeJoint2D connection = link.AddComponent(typeof(HingeJoint2D)) as HingeJoint2D;
            connection.connectedBody = NewestBody;
            if(gameObject.tag == "rLeg" || gameObject.tag == "lLeg")
            {
                connection.enableCollision = true;
            }
            if(gameObject.tag == "lArm" || gameObject.tag == "rArm")
            {
                //connection.enableCollision = true;
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
                if(gameObject.tag == "rArm") 
                {
                    //muscle.restRotation = 90;
                    muscle.force = 100;
                    body.mass = 1 / length;
                    body.gravityScale = 0;
                    //freezing rotation bad as means muscle doesn't work as it moves rotationaaa
                    //body.freezeRotation = true;
                }
                else
                {
                    muscle.force = 150;
                    if(length - i < length - (length - 5))
                    {
                        body.mass = 1f;
                    }
                    else
                    {
                        body.mass = 1f;
                    }
                }
                muscle.bone = body;
                Array.Resize(ref stick.muscles, stick.muscles.Length + 1);
                stick.muscles[stick.muscles.Length - 1] = muscle;
                if(gameObject.tag == "rLeg" || gameObject.tag == "lLeg") 
                {
                Array.Resize(ref stick.legs, stick.legs.Length + 1);
                stick.legs[stick.legs.Length - 1] = muscle;
                }

                NoCollision nocoll = link.AddComponent(typeof(NoCollision)) as NoCollision;
            
                if(gameObject.tag == "rLeg")
                {
                    Array.Resize(ref stick.rbRIGHT, stick.rbRIGHT.Length + 1);
                    stick.rbRIGHT[stick.rbRIGHT.Length - 1] = body;
                    //if(i<(length/2))
                    //{
                        //link.tag = "LrLeg";
                    //}
                    link.tag = "rLeg";
                }
                else if(gameObject.tag == "lLeg")
                {
                    Array.Resize(ref stick.rbLEFT, stick.rbLEFT.Length + 1);
                    stick.rbLEFT[stick.rbLEFT.Length - 1] = body;
                    //if(i<(length/2))
                    //{
                        //link.tag = "LlLeg";
                    //}
                    link.tag = "lLeg";
                }
                else if(gameObject.tag == "rArm")
                {
                    Array.Resize(ref stick.rbARIGHT, stick.rbARIGHT.Length + 1);
                    stick.rbARIGHT[stick.rbARIGHT.Length - 1] = body;
                    link.tag = "rArm";
                    if(i == length - 1)
                    {
                        CheckGrab grab = link.AddComponent(typeof(CheckGrab)) as CheckGrab;
                        grab.Stick = stick;
                        grab.Gun = Gun;
                    }
                }
                else if(gameObject.tag == "lArm")
                {
                    Array.Resize(ref stick.rbALEFT, stick.rbALEFT.Length + 1);
                    stick.rbALEFT[stick.rbALEFT.Length - 1] = body;
                    link.tag = "lArm";
                    if(i == length - 1)
                    {
                        CheckGrab grab = link.AddComponent(typeof(CheckGrab)) as CheckGrab;
                        grab.Stick = stick;
                        grab.Gun = Gun;
                    }
                }
            }
            // this is not in use i believe
            if (i == length - 1) 
            {
                DistanceJoint2D distance = link.AddComponent(typeof(DistanceJoint2D)) as DistanceJoint2D;
                distance.connectedBody = oldestBody;
                distance.autoConfigureDistance = true;
                distance.maxDistanceOnly = true;
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
                    // change accepted tags here to get other guns accepted
                    if(gameObject.tag == "rLeg")
                    {
                        collide.otherleg = stick.rbRIGHT[stick.rbRIGHT.Length - 1].gameObject.GetComponent<Collided>();
                    }
                    else
                    {
                        collide.otherleg = stick.rbLEFT[stick.rbLEFT.Length - 1].gameObject.GetComponent<Collided>();
                    }
                    collide.stick = stick;
                    
                    if (gameObject.tag == "rLeg")
                    {
                        stick.rayL = ray;
                    }
                    else
                    {
                        stick.rayR = ray;
                    }
                }
                else if(gameObject.tag == "rArm" || gameObject.tag == "lArm")
                {
                    GameObject child = gameObject.transform.GetChild(0).gameObject;
                    if(child.tag == "Gun")
                    {
                        //child.transform.SetParent(link.transform,false);
                        FollowPos pos = child.AddComponent(typeof(FollowPos)) as FollowPos;
                        pos.target = link;
                    }
                }
        }

	}
}

    void Update()
    {
    }
}

