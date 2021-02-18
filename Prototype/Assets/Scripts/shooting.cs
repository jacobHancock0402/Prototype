using UnityEngine;
using System.IO;

public class shooting : MonoBehaviour {
public bool shoot;
public bool wasGrabbed;
public GameObject BulletSpawner;
public GameObject BulletPrefab;
public GameObject EmptyObject;
public int BulletSpeed = 50;
public bool A = false;
public Sprite sprite;
public GameObject chainPrefab;
public Vector3 NewestChild;
public Rigidbody2D NewestBody;
public bool grabbed;
public Stickman stick;
public GameObject lArm;
public GameObject rArm;
public GameObject arm;
public GameObject body;
public GameObject hand;
public GameObject particle;
public ControlFlashEmission control;
public AudioSource source;
public SpriteRenderer Sprite;

void Start()
{
    source = GetComponent<AudioSource>();
    Sprite = GetComponent<SpriteRenderer>();
    particle = transform.GetChild(transform.childCount-1).gameObject;
    control = particle.GetComponent<ControlFlashEmission>();
    stick = gameObject.transform.root.GetComponent<Stickman>();
    rArm = stick.rArm;
    lArm = stick.lArm;
}

void Update()
{
    if (transform.parent != null)
    {
        grabbed = true;
        transform.SetSiblingIndex(0);
        if(transform.parent.parent.gameObject.tag == "rArm")
        {
            arm = rArm;
            hand = stick.rHand;
        }
        else
        {
            arm = lArm;
            hand = stick.lHand;
        }
    }
    else
    {
        grabbed = false;
    }
    if(grabbed)
    {
        Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position).normalized;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        //gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -angle + 90);
        if (Sprite)
        {
            if (-angle + 90 > 90)
            {
                //Sprite.flipY = true;
            }

            else
            {
                //Sprite.flipY = false;
            }
        }
       // foreach(_Muscle muscle in stick.muscles)
       // {
            //if(muscle.bone.gameObject.tag == arm.tag)
           // {
                //muscle.bone.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -angle + 90);
                //if(muscle.bone.transform.parent.GetChild(0).tag != "Body")
                //{
                    //Vector3 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - muscle.bone.gameObject.transform.position).normalized;
                    //float ang = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
                    //muscle.restRotation = -angle + 180;
               // }
                //else
                //{
                    //muscle.bone.simulated = false;
                //}
                //muscle.bone.GetComponent<HingeJoint2D>().enableCollision = true;                //muscle.bone.freezeRotation = true;
                //break;
            //}
        //}
        //arm.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -angle + 90);
        //arm.GetComponent<Rigidbody2D>().simulated = false;
        if(!wasGrabbed)
        {
            transform.position = transform.parent.position;
        }
        if(Input.GetMouseButtonDown(0))
        {
            Fire(angle,direction);
        }
    }
    wasGrabbed = grabbed;
}

public void Fire(float angle, Vector3 direct)
    {
        Debug.Log(gameObject.layer);
        // no idea why every layer is seen as 9
        // as if i'm setting somewhere else
        if (gameObject.layer == 9)
        {
        Debug.DrawRay(gameObject.transform.position, direct);
        GameObject initObject = null;
        LayerMask mask = LayerMask.GetMask("World");
        Vector2 dir = new Vector2(direct.x, direct.y);
        Vector2 origin = new Vector2(gameObject.transform.parent.position.x, gameObject.transform.parent.position.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, dir, 500000f, mask);
        //Debug.Log(hit.collider.gameObject.tag);
        if(hit.collider != null){
            Debug.Log("lhi");
            if (hit.collider.gameObject.tag == "World")
            {
                Debug.Log("Boo");
                Rigidbody2D rigid = gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>();
                Vector3 EndPoint = new Vector3(hit.point.x, hit.point.y, gameObject.transform.position.z);
                Vector3 StartPoint = gameObject.transform.parent.parent.position;
                float diffX = EndPoint.x - StartPoint.x;
                float diffY = EndPoint.y - StartPoint.y;
                float numNeeded;
                BoxCollider2D collider = chainPrefab.GetComponent<BoxCollider2D>();

            
        
                //BoxCollider2D collider = link.AddComponent<BoxCollider2D>();
                numNeeded = Mathf.Abs(Mathf.Round(diffX / Mathf.Abs((collider.bounds.max[0] - collider.bounds.center[0])) + Mathf.Abs(diffY / (collider.bounds.max[1] - collider.bounds.center[1]))));
                Debug.Log(numNeeded);
                Debug.Log((EndPoint - StartPoint).magnitude);
                float diffXEach = diffX / numNeeded;
                float diffYEach = diffX / numNeeded;
                stick.NowHoldingR = true;
                for (int i=0;i<numNeeded;i++)
                {
                    int count = gameObject.transform.childCount - 1;
                    GameObject link = Instantiate(chainPrefab) as GameObject;
                    if (i==0)
                    {
                        NewestChild = new Vector3(hit.point.x, hit.point.y, 0);
                        NewestBody = collider.gameObject.GetComponent<Rigidbody2D>();
                        initObject = link;
                    }
                    link.transform.position = new Vector3(
                    NewestChild.x - diffXEach, 
                    NewestChild.y - diffYEach, 
                    gameObject.transform.position.z);
                    link.transform.SetParent(hit.collider.gameObject.transform);
                    Rigidbody2D body = link.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
                    body.mass = 10f;
                    //body.drag = 10f;
                    //body.gravityScale = 0f;
                    if (i!=0)
                    {
                        HingeJoint2D connection = link.AddComponent(typeof(HingeJoint2D)) as HingeJoint2D;
                        //connection.enableCollision = true;
                        connection.connectedBody = NewestBody;
                        //DistanceJoint2D connect = link.AddComponent(typeof(DistanceJoint2D)) as DistanceJoint2D;
                        //connect.connectedBody = initObject.GetComponent<Rigidbody2D>();
                        //connect.maxDistanceOnly = true;
                        //connect.autoConfigureDistance = false;
                    }
                    else
                    {
                        FixedJoint2D connection = link.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
                        connection.connectedBody = NewestBody;
                    }
                    NewestChild = link.transform.position;
                    NewestBody = body;
                    //Debug.Log(transform.parent.parent.gameObject.tag);
                    //stick.DropGun(stick.rHand);
                    shoot = false;
                    if(A == false)
                    {
                        //link.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -angle + 90);
                    }
                    //else
                    //{
                        //link.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -angle + 90);
                    //}
                    if(i == numNeeded - 1)
                    {
                        CheckColl coll = link.AddComponent(typeof(CheckColl)) as CheckColl;
                        //DistanceJoint2D distance = link.AddComponent(typeof(DistanceJoint2D )) as DistanceJoint2D ;
                        // distance.autoConfigureDistance = true;
                        // distance.autoConfigureConnectedAnchor = true; 
                        // distance.connectedBody = initObject.GetComponent<Rigidbody2D>();
                        coll.Player = gameObject.transform.parent.parent.gameObject;
                        DistanceJoint2D Distance = new DistanceJoint2D();
                        // GameObject heavyObj = stick.heavyObj;
                        // DistanceJoint2D heavyJoint = heavyObj.AddComponent(typeof(DistanceJoint2D)) as DistanceJoint2D;
                        // heavyJoint.connectedBody = body;
                        // heavyObj.transform.position = stick.Body.transform.position;
                        // heavyJoint.distance = 0.05f;
                        // heavyJoint.maxDistanceOnly = true;
                        // heavyJoint.autoConfigureDistance = true;
                        // heavyJoint.autoConfigureConnectedAnchor = true; 
                        //if(hand.tag == "rArm")
                        //{
                            //Distance = rArm.AddComponent(typeof(DistanceJoint2D)) as DistanceJoint2D;
                        //}
                        //else
                        //{
                            Distance = stick.Body.AddComponent(typeof(DistanceJoint2D)) as DistanceJoint2D;
                        //}
                        //Debug.Log(Distance.gameObject.tag);
                        
                        Distance.connectedBody = transform.parent.parent.gameObject.GetComponent<Rigidbody2D>();
                        Distance.autoConfigureDistance = false;
                        Distance.maxDistanceOnly = true;
                        Distance.enableCollision = true;
                    }
                    // no idea why not instanciating distance joint on the body
                    //body.velocity = way * BulletSpeed;
                    Debug.Log(transform.parent.parent.gameObject.tag);
                }
                if(transform.parent.parent.gameObject.tag == "rArm")
                {
                        //stick.NowGrabbingR = true;
                        stick.currentPivotArm = "rArm";
                        stick.DropGun(hand);
                        //if (stick.NowHoldingR) 
                        //{
                           // stick.StopHold(stick.rHand, "RightA");
                        //}
                        stick.NowHoldingR = true;
                        stick.NowGrabbingR = false;
                        stick.swingingR = true;

                }
                else
                {
                    //if (stick.NowHoldingL) 
                    //{
                        //stick.StopHold(stick.lHand, "LeftA");
                    //}
                    stick.currentPivotArm = "lArm";
                    stick.DropGun(hand);
                    stick.NowHoldingL = true;
                    stick.NowGrabbingL = false;
                    stick.swingingL = true;
                }
    


            
                // ok now, but  still hard to move as change of masses
                // dude get's pulled cause he little, and sometimes instantiates slighlty off
                // could prob decrease mass to 1f
        }
        }
}
else
{
        // gunplay is very smooth when fps is ok
        // everything more stable now
        // actual right directiion
        // gun sometimes rotates or something permeananetlly, though position not in editor
        // problem with position of bullet and flash when flip gun
        // no idea how to fix
        // also need to limit rate of shooting and reloading and ***REMOVED***
        float distance = direct.magnitude;
         //Vector3 handDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.parent.parent.position).normalized;
        //float handAngle = Mathf.Atan2(direct.x, direct.y) * Mathf.Rad2Deg;
        Vector2 way = transform.right;
        direct.Normalize();
        // what i was doing was using way instead of direct to just shoot out front of gun
        float bulletAngle = Mathf.Atan2(direct.x, direct.y) * Mathf.Rad2Deg;
        if (shoot == false)
        {
            GameObject b = Instantiate(BulletPrefab) as GameObject;
            shoot = false;
            b.tag = "Bullet";
            Rigidbody2D body = b.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
            Bullet script = b.GetComponent<Bullet>();
            //PhysicsMaterial2D bouncy = new PhysicsMaterial2D();
            //bouncy.friction = 0;
            //bouncy.bounciness = 0.8f;
           // bouncy.frictionCombine = PhysicsMaterialCombine.Average;
           // bouncy.bounceCombine = PhysicsMaterialCombine.Average;
            //b.GetComponent<CapsuleCollider2D>().sharedMaterial = bouncy;
            script.thisRigid = body;
            body.mass = 0.1f;
            body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            body.interpolation = RigidbodyInterpolation2D.Interpolate;
            b.transform.position = gameObject.transform.GetChild(0).position;
            script.stick = stick;
            if(A == false)
            {
                b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -bulletAngle);
            }
            else
            {
                 b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -bulletAngle);
            }
            body.velocity = direct * BulletSpeed;
        }
        control.ActivateEmission();
        source.PlayOneShot(source.clip);
        hand.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,2f), ForceMode2D.Impulse);
        //control.startTim = 1000f;
        
        }
    }
}