using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System.IO;

public class shooting : MonoBehaviour {
public bool shoot;
public AudioSource fleshHitAudio;
public bool wasGrabbed;
public BulletManager bulletManager;
public GameObject BulletSpawner;
public GameObject BulletPrefab;
public GameObject EmptyBulletPrefab;
public GameObject EmptyObject;
public int BulletSpeed = 50;
public bool A = false;
public Sprite spriteFlash;
public GameObject chainPrefab;
public Vector3 NewestChild;
public Rigidbody2D NewestBody;
public bool grabbed;
public Stickman stick;
public StressReceiver stress;
public float shakeStress;
public GameObject lArm;
public GameObject rArm;
public GameObject arm;
public GameObject body;
public GameObject hand;
public GameObject bloodParticleObj;
public GameObject flashParticleObj;
public GameObject bloodCloudObj;
public ControlFlashEmission flashControl;
public AudioSource source;
public SpriteRenderer GunSprite;
public ControlBloodEmission bloodControl;
//public CameraShake shake;
public ScreenFlashEffect screenF;
public GameObject rightBulletPosObj;
public GameObject leftBulletPosObj;
public Vector3 bulletSpawn;
public Light2D flashlight;
public bool test;
public float maxIntensity = 2f;
public float testAngle;
public bool anid = false;
public GameObject backGripObj;
public GameObject frontGripObj;
public Vector3 backGrip;
public Vector3 frontGrip;
public FixedJoint2D rArmFixedJoint;
public FixedJoint2D lArmFixedJoint;
public string gunSide = "Right";
public Rigidbody2D thisRigid;
public float timeAtLastShot = 0f;
public float timeSinceLastShot = 1f;
public bool auto;
public Vector3 pos1;
public Vector3 pos2;
public Vector3 pos3;
public float aniStart;
public float aniTime;
public bool reload;
public GameObject mag;
public Rigidbody2D mag_rigid;
public GameObject mag_pos_obj;
public Vector3 mag_pos;
public GameObject mag_prefab;
public FixedJoint2D j;
public AudioSource reload1;
public AudioSource reload2;
public AudioSource emptyAudio;
public bool anid2 = false;
public GameObject loadingHand;
public GameObject shoulderGrip;
public Vector3 shoulderPos;
public int maxRoundsInMag;
public int roundsInMag;
public float RPM;
public float autoDelay;
public bool firstEmptyClick = false;
public float shellLoadStart = 0f;
public AudioSource[] pumps;
public GameObject loadingBullet;
public float lastFireTime = 0f;
public bool switchedFace;
public GameObject flashPosRight;
public GameObject flashPosLeft;
public Vector3 flashRightPos;
public Vector3 flashLeftPos;
public AudioSource fireModeSwitch;
public bool twoHanding = true;

void Start()
{
    source = GetComponent<AudioSource>();
    if(gameObject.tag == "Gun")
    {
        GunSprite = GetComponent<SpriteRenderer>();
    }
    //flashParticleObj = transform.GetChild(transform.childCount-3).gameObject;
    //bloodCloudObj = transform.GetChild(transform.childCount-2).gameObject;
    //bloodParticleObj = transform.GetChild(transform.childCount-1).gameObject;
    flashControl = flashParticleObj.GetComponent<ControlFlashEmission>();
    bloodControl = bloodParticleObj.GetComponent<ControlBloodEmission>();
    if(transform.parent != null)
    {
        //Debug.LogError("hai");
        if(transform.root.gameObject.tag == "Player" || transform.root.gameObject.tag == "AI" )
        {
            stick = gameObject.transform.root.GetComponent<Stickman>();
            rArm = stick.rArm;
            lArm = stick.lArm;
        }
    }
    //Debug.LogError("USEEMPRTECTIOOMYNGGA");
    thisRigid = GetComponent<Rigidbody2D>();
    backGrip = backGripObj.transform.localPosition;
    frontGrip = frontGripObj.transform.localPosition;
    flashLeftPos = flashPosLeft.transform.localPosition;
    flashRightPos = flashPosRight.transform.localPosition;
    //mag = transform.GetChild(1).gameObject;
    mag_pos = mag_pos_obj.transform.localPosition;
    //shoulderPos = shoulderGrip.transform.localPosition;
    roundsInMag = maxRoundsInMag;
    autoDelay = 1f/(RPM / 60f);
}

void Update()
{
    // if((Time.time - lastFireTime) < 1f && !switchedFace)
    // {
    //     stick.ChangeFace("Angry");
    //     switchedFace = true;
    // }
    // if((Time.time - lastFireTime) >= 1f && switchedFace)
    // {
    //     stick.ChangeFace("Normal");
    //     switchedFace = false;
    // }

    // janked enters at wrong times
    // yeah idk what's going on, it doesn't shoot on right side after dropping one hand, maybe change the control from mouse 2 as that's for slowdown
    // then doesn't reload, nor do guns drop
    stick.TwoHanding = twoHanding;
    if(gameObject.tag == "Pistol")
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(twoHanding)
            {
                Debug.LogError("lovingyou");
                twoHanding = false;
                GameObject hand = stick.lHand;
                if(gunSide == "Right")
                {
                    Destroy(stick.rHand.GetComponent<FixedJoint2D>());
                    stick.NowGrabbingR = false;
                }
                else 
                {
                    hand = stick.rHand;
                    Destroy(stick.lHand.GetComponent<FixedJoint2D>());
                    stick.NowGrabbingL = false;
                }
                transform.parent = hand.transform;
            }
            else 
            {
                twoHanding = true;
                stick.disableR = false;
                stick.disableL = false;
                stick.NowGrabbingR = true;
                stick.NowGrabbingL = true;
                stick.gunInL = true;
                stick.gunInR = true;
                if(!stick.rHand.GetComponent<FixedJoint2D>())
                {
                    FixedJoint2D joint1 = stick.rHand.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
                    joint1.autoConfigureConnectedAnchor = false;
                    joint1.connectedAnchor = frontGrip;
                    joint1.connectedBody = thisRigid;
                }
                else 
                {
                    FixedJoint2D joint2 = stick.lHand.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
                    joint2.autoConfigureConnectedAnchor = false;
                    joint2.connectedAnchor = backGrip;
                    joint2.connectedBody = thisRigid;
                }
            }
            
        }
        if(Input.mouseScrollDelta.y > 0f && auto)
        {
            auto = false;
            fireModeSwitch.PlayOneShot(fireModeSwitch.clip);
        }
        else if(Input.mouseScrollDelta.y < 0f && !auto)
        {
            auto = true;
            fireModeSwitch.PlayOneShot(fireModeSwitch.clip);
        }
    }
    timeSinceLastShot = Time.time - timeAtLastShot;
    aniTime = Time.time - aniStart;
    if (transform.parent != null)
    {
        // backGrip = backGripObj.transform.position;
        // frontGrip = frontGripObj.transform.position;
        grabbed = true;
        transform.SetSiblingIndex(0);
       // if(gameObject.tag == "Gun")
        //{
        Debug.LogError("showgoesonallnight");
            if(transform.parent.parent.gameObject.tag == "rArm")
            {
                arm = stick.rArm;
                hand = stick.rHand;
            }
            else
            {
                arm = stick.lArm;
                hand = stick.lHand;
            }
       // }
        if(!wasGrabbed)
        {
            thisRigid.bodyType = RigidbodyType2D.Static;
            thisRigid.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    else
    {
        grabbed = false;
    }
    if(grabbed && !stick.dead)
    {
        Debug.LogError("imaeatmyownflo");
        Vector3 endPoint = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Vector3 angularDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        float angle = Mathf.Atan2(angularDirection.x, angularDirection.y) * Mathf.Rad2Deg;
        Debug.LogError("hannibalburess" + arm);
        if(!stick.Player)
        {
            angle = stick.angleL;
            if(arm.tag == "rArm")
            {
                angle = stick.angleR;
            }
        }
        Debug.LogError("swaelee");
        Vector3 flashPos = new Vector3(0,0,0);
        //Debug.LogError("disanglebedis" + angle);
        //gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -angle + 90);
        //gameObject.transform.rotation = Quaternion.Euler(0,0, 90);
        // this looks quite good now, but only works on one side,
        // cant really flip because rb = rotation controlled
        // so might have to the swap the joint anchors or something
        // also the grip pos's arent great and the shooting doesn't work properly because is only parented to one arm
         Debug.LogError("imaeatmyownflo" + angle);
         Debug.LogError("ericandreshow");
        if (GunSprite)
        {
            if (-angle + 90 > 90)
            {
                GunSprite.flipY = true;
                bulletSpawn = leftBulletPosObj.transform.position;
                flashPos = flashPosLeft.transform.localPosition;
            }

            else
            {
                GunSprite.flipY = false;
                bulletSpawn = rightBulletPosObj.transform.position;
                flashPos = flashPosRight.transform.localPosition;
                // idk, arms look weird just there, jump needs adjust, ar no shoot, should do pistol as well, then start instantiating enemies
            }
        }
        else if(!reload)
        {
            thisRigid.bodyType = RigidbodyType2D.Dynamic;
            bulletSpawn = rightBulletPosObj.transform.position;
            string newGunSide = "Right";
            float sumAngle = -angle + 90;
            if(gameObject.tag == "AR")
            {
                Debug.LogError("imaeatmyownflow" + sumAngle);
            }
            if (-angle + 90 > 90)
            {
                newGunSide = "Left";
            }
            if(newGunSide != gunSide && twoHanding)
            {
                FixedJoint2D jointR = stick.rHand.GetComponent<FixedJoint2D>();
                FixedJoint2D jointL = stick.lHand.GetComponent<FixedJoint2D>();
                Vector3 copy = jointR.connectedAnchor;
                jointR.connectedAnchor = jointL.connectedAnchor;
                jointL.connectedAnchor = copy;
            }
            gunSide = newGunSide;
        }
        if(flashlight)
        {
            float intensity = flashlight.intensity + (0.1f *Input.mouseScrollDelta.y);
            flashlight.intensity = Mathf.Max(Mathf.Min(intensity, maxIntensity) , 0);
        }
        // switches arms on ai, like the pivot point relative to mouse
        // arms still discoloured, not crouching anymore, although legs change pivot
        // head rot is right now, but looks weird as arms pointed downwards, have a flag to only do when focused// or might be fixed now as just set rest when 0
        // just set to 0 now, but have to be diff for difff dir
        flashParticleObj.transform.localPosition = flashLeftPos;
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
        stick.reload = reload;
        if(reload)
        {
            thisRigid.freezeRotation = true;
            // if(aniTime > 3f)
            // {
            //     stick.rHand.GetComponent<Rigidbody2D>().MovePosition(pos3);
            //     reload = false;
            // }
            // else if(aniTime > 2f)
            // {
            //     stick.rHand.GetComponent<Rigidbody2D>().MovePosition(pos2);
            // }
            // else if(aniTime > 1f)
            // {
            //     stick.rHand.GetComponent<Rigidbody2D>().MovePosition(pos1);
            // }
            //thisRigid.simulated = false;
            if(gameObject.tag == "AR" || gameObject.tag == "Pistol")
            {
                if(aniTime < 1f)
                {
                    if(loadingHand.tag == "rArm")
                    {
                        foreach(_Muscle m in stick.rArmMuscleList)
                        {
                            m.restRotation = 40;
                            m.bone.drag = 0f;
                        }
                        foreach(_Muscle m in stick.lArmMuscleList)
                        {
                            m.restRotation = stick.angleR;
                            m.bone.drag = stick.arm_drag;
                        }
                    }
                    else
                    {
                        foreach(_Muscle m in stick.lArmMuscleList)
                        {
                            m.restRotation = 320;
                            m.bone.drag = 0f;
                        }
                        foreach(_Muscle m in stick.rArmMuscleList)
                        {
                            m.restRotation = stick.angleR;
                            m.bone.drag = stick.arm_drag;
                        }
                    }
                    // thsi doeesn't seem to fix limp gun on pulling mag, maybe muscle not activated

                }
                if(aniTime > 1f && !anid)
                {
                    mag_rigid = mag.AddComponent<Rigidbody2D>();
                    BoxCollider2D coll = mag.AddComponent<BoxCollider2D>();
                    mag.transform.SetParent(null);
                    //mag.transform.localScale = new Vector3(-25, 15, 44);
                    mag_rigid.AddForce(new Vector2(-2f, 0f));
                    anid = true;

                }
                else
                {
                    if(loadingHand.tag == "rArm")
                    {
                        foreach(_Muscle m in stick.rArmMuscleList)
                        {
                            m.bone.drag = stick.arm_drag;
                        }
                    }
                    else
                    {
                        foreach(_Muscle m in stick.lArmMuscleList)
                        {
                            m.bone.drag = stick.arm_drag;
                        }
                    }
                }
                if(aniTime > 2f && !anid2)
                {
                    j = loadingHand.AddComponent<FixedJoint2D>();
                    j.autoConfigureConnectedAnchor = false;
                    j.connectedAnchor = mag_pos;
                    j.connectedBody = thisRigid;
                    mag = Instantiate(mag_prefab) as GameObject;
                    mag.transform.SetParent(transform);
                    mag.transform.localPosition = mag_pos;
                    mag.transform.rotation = transform.GetChild(0).rotation;
                    if(transform.localScale.x < 0)
                    {
                        mag.transform.localScale = new Vector3(-mag.transform.localScale.x, mag.transform.localScale.y, mag.transform.localScale.z);
                    }
                    stick.PlayAudio(reload1, gameObject);
                    anid2 = true;
                }
                if(aniTime > 3f)
                {
                    j.connectedAnchor =   backGrip;
                    stick.PlayAudio(reload2, gameObject);
                    roundsInMag = maxRoundsInMag;
                    reload = false;          
                }
            }
            else if(gameObject.tag == "Shotgun")
            {
                //if(aniTime < 1.5 )
                if(aniTime > 3.7f || ((roundsInMag == maxRoundsInMag) && aniTime > 0.9f))
                {
                    roundsInMag = maxRoundsInMag;
                    FixedJoint2D Joint = loadingHand.GetComponent<FixedJoint2D>();
                    Joint.connectedAnchor = frontGrip;
                    //reload1.Stop();
                    //stick.PlayAudioSource(reload3, gameObject);
                    int index = Random.Range(0, 3);
                    stick.PlayAudio(pumps[index], gameObject);
                    Destroy(loadingBullet);
                    // FixedJoint2D[] joints = loadingHand.GetComponents<FixedJoint2D>();
                    // for(int i=joints.Length - 1;i>0;i++)
                    // {
                    //     Destroy(joints[i]);
                    // }
                    reload = false;
                }
                if(aniTime > 0.5f)
                {
                    if((Time.time - shellLoadStart) > 0.2f && loadingBullet != null )
                    {
                        Destroy(loadingBullet);
                        //Destroy(loadingHand.GetComponents<FixedJoint2D>()[1]);
                        loadingBullet = null;
                    }
                    if((Time.time - shellLoadStart) > 0.4f && roundsInMag < maxRoundsInMag)
                    {
                        shellLoadStart = Time.time;
                        FixedJoint2D joint = loadingHand.GetComponent<FixedJoint2D>();
                        joint.autoConfigureConnectedAnchor = false;
                        joint.connectedBody = thisRigid;
                        joint.connectedAnchor = mag_pos;
                        GameObject shell = Instantiate(EmptyBulletPrefab) as GameObject;
                        shell.transform.SetParent(loadingHand.transform);
                        shell.transform.position = new Vector3(loadingHand.transform.position.x,loadingHand.transform.position.y, -10);
                        Debug.LogError("yeahmane" + shell.tag);
                        loadingBullet = shell;
                        stick.PlayAudio(reload2, gameObject);
                        roundsInMag++;
                    }
                }
            }
            // uh yeah so reloads pretty much done , could do with some tuning on both, no buulet in hands on shotgun
            // don't think dropping guns works ,also they go limp when reloading, does with both but shotgun pivots on hand so not as bad
            // redo pistol now
            // prob new bullets for ar, as they like shells
            // bullet in hands of shtgun has bad prefab, created a new empty one so just assign to it 
        }
        else
        {
            thisRigid.freezeRotation = false;
        }
        // no idea what's happening, dont care though lol suck me dick
        // but no i think the idea's decent, just rotationg the arm backwards and throwing the mag, then coming back in and connecting with a mag back in
        // but obvs doesn't work right now
        if(!wasGrabbed)
        {
            transform.position = transform.parent.position;
        }
        if((Input.GetKeyDown(KeyCode.R) && !reload && roundsInMag < maxRoundsInMag) || (!stick.Player && roundsInMag == 0f ))
        {
            mag.transform.rotation = Quaternion.Euler(0f,0f,0f);
            reload = true;
            aniStart = Time.time;
            loadingHand = stick.lHand;
            GameObject otherHand = stick.rHand;
            if(transform.localScale.x < 0)
            {
                loadingHand = stick.rHand;
                otherHand = stick.lHand;
            }
            Destroy(loadingHand.GetComponent<FixedJoint2D>());
            mag.transform.SetParent(loadingHand.transform);
            stick.PlayAudio(reload1, gameObject);
            anid = false;
            anid2 = false;
            if(loadingHand.tag == transform.parent.gameObject.tag)
            {
                transform.SetParent(otherHand.transform);
            }
            if(gameObject.tag == "Shotgun")
            {
                FixedJoint2D joint = loadingHand.AddComponent<FixedJoint2D>();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedBody = thisRigid;
                joint.connectedAnchor = mag_pos;
                GameObject shell = Instantiate(EmptyBulletPrefab) as GameObject;
                shell.transform.SetParent(loadingHand.transform);
                shell.transform.position = new Vector3(loadingHand.transform.position.x,loadingHand.transform.position.y, -10);
                Debug.LogError("yeahmane" + shell.tag);
                loadingBullet = shell;
                Debug.LogError("swaggyforreal" + loadingBullet);
                // can now see this, but doesn't create multiple i dont think
                // just have to delete it at a certain point, then reinstantiate
            }
        }
        // this looks decent , apart from mag just flying, maybe set pos to hand
        // next make it functional
        // also arm's dont seem to aim when reloading
        if(roundsInMag > 0 && (!reload || gameObject.tag == "Shotgun"))
        {
            firstEmptyClick = true;
            if(Input.GetMouseButtonDown(0) && stick.Player && !auto)
            {
                reload = false;
                reload1.Stop();
                reload2.Stop();
                roundsInMag--;
                Fire(angle, endPoint, true);
            }
            else if(auto && Input.GetMouseButton(0) && timeSinceLastShot > autoDelay && stick.Player)
            {
                roundsInMag--;
                timeAtLastShot = Time.time;
                Fire(angle, endPoint, true);         
            }
        }
        else if(Input.GetMouseButtonDown(0) && stick.Player || (Input.GetMouseButton(0) && firstEmptyClick && auto))
        {
            stick.PlayAudio(emptyAudio, gameObject);
            firstEmptyClick = false;
        }
    }
    else if(test)
    {
        //angle = testAngle;
        if(Input.GetKeyDown(KeyCode.M))
        {
            Fire(testAngle, transform.position + transform.right,false );
        }
    }
    wasGrabbed = grabbed;
}

public void Fire(float angle, Vector3 endPoint, bool player)
    {
        Debug.LogError("lookmalnourished");
        lastFireTime = Time.time;
        stick.ChangeFace("Angry");
       // Debug.Log(gameObject.layer);
        Debug.LogError("ohblockedbyjames");
        Vector3 direct = endPoint - gameObject.transform.position;
        //StartCoroutine(shake.Shake());
        if(player)
        {
            stress.InduceStress(shakeStress);
            // maybe force should move body, but otherwise pretty good
        }
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
    //Debug.LogError("bigdickyonmyblicky");
        // gunplay is very smooth when fps is ok
        // everything more stable now
        // actual right directiion
        // gun sometimes rotates or something permeananetlly, though position not in editor
        // problem with position of bullet and flash when flip gun
        // no idea how to fix
        // also need to limit rate of shooting and reloading and shit
        float distance = direct.magnitude;
         //Vector3 handDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.parent.parent.position).normalized;
        //float handAngle = Mathf.Atan2(direct.x, direct.y) * Mathf.Rad2Deg;
        Vector2 way = transform.right;
        //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.right);
        //direct = (Vector2)(Quaternion.Euler(angle,0,0) * Vector2.right);
        direct = direct.normalized;
        // what i was doing was using way instead of direct to just shoot out front of gun
        float bulletAngle = angle;
        int bullets = 1;
        if(gameObject.tag == "Shotgun")
        {
            bullets = 10;
        }
        //direct = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)); //Mathf.Atan2(direct.x, direct.y) * Mathf.Rad2Deg;
        if (shoot == false)
        {
            for(int i=0;i<bullets;i++)
            {
                // this feels explosive, but not very good as one bullet always pointed upwards
                // needs to do visual like particles and sound for the shotgun
                // walking and all movement is pretty good
                // maybe make the revolver, or new pistol in this system
                // maybe redesign climbing with this fixed joint stuff, where climb quick or sum
                // even if i remove spread, still one rotated upwards, as if not rotating
                float maxSpread = 10f;
                // if(bullets > 0)
                // {
                //     maxSpread = 0.1f;
                // }
                GameObject b = Instantiate(BulletPrefab) as GameObject;
                shoot = false;
                b.tag = "Bullet";
                Rigidbody2D body = b.GetComponent<Rigidbody2D>();
                Bullet script = b.GetComponent<Bullet>();
                script.prefab = false;
                //PhysicsMaterial2D bouncy = new PhysicsMaterial2D();
                //bouncy.friction = 0;
                //bouncy.bounciness = 0.8f;
               // bouncy.frictionCombine = PhysicsMaterialCombine.Average;
               // bouncy.bounceCombine = PhysicsMaterialCombine.Average;
                //b.GetComponent<CapsuleCollider2D>().sharedMaterial = bouncy;
                script.thisRigid = body;
                script.screenF = screenF;
                flashControl.ActivateEmission();
                script.control = bloodControl;
                script.BloodParticle = bloodParticleObj;
                script.BloodCloud = bloodCloudObj;
                script.fleshHitAudio = fleshHitAudio;
                GameObject obj = gameObject;
                Debug.LogError("ALLTHISMONEY" + obj.tag);
                // def too little force
                // but prob just incease vector
                body.mass = 0.1f;
                body.simulated = true;
                Debug.LogError("swaggingonyoubitch");
                body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                body.interpolation = RigidbodyInterpolation2D.Interpolate;
                b.transform.position = bulletSpawn;
                script.BulletSpeed = BulletSpeed;
                Debug.LogError("yeetnigga");
                script.stick = stick;
                //direct = b.transform.forward;
                //Debug.LogError("theVelocity" + direct);
                b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -bulletAngle  + Random.Range(-maxSpread, maxSpread));
                body.velocity = (b.transform.up * BulletSpeed);
                if(i == 0)
                {
                    stick.PlayAudio(source, obj);
                    if(gameObject.tag == "Shotgun")
                    {
                        thisRigid.AddForce(-body.velocity * 0.4f, ForceMode2D.Impulse);
                        stick.BodyRigid.AddForce(-body.velocity * 500f);
                        hand = stick.rHand;
                    }
                    if(gameObject.tag == "AR")
                    {
                        thisRigid.AddForce(-body.velocity * 0.1f, ForceMode2D.Impulse);
                        stick.BodyRigid.AddForce(-body.velocity * 100f);
                        //hand = stick.rHand;
                    }
                    hand.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,2f), ForceMode2D.Impulse);
                } //+ new Vector3(Random.Range(-maxSpread, maxSpread), Random.Range(-maxSpread, maxSpread), Random.Range(-maxSpread, maxSpread));
                //bulletAngle = -bulletAngle;//Mathf.Atan2(body.velocity.x, body.velocity.y) * Mathf.Rad2Deg;
                //b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -bulletAngle  + Random.Range(-maxSpread, maxSpread));
                script.dir =  body.velocity;
                bulletManager.bullets.Add(b);
            } //((direct * BulletSpeed).normalized)* BulletSpeed;
            // i fixed this issue, but the direction isn't right now, is only correct to north east
        }
        //control.startTim = 1000f;
        
        }
    }
}