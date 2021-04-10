using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;
//using UnityEngine.ParticleSystemModule;
using System.IO;

public class Bullet : MonoBehaviour {
    public float time = 0;
    public Rigidbody2D thisRigid;
    public Collider2D thisCollider;
    public Stickman stick;
    public GameObject BloodParticle;
    public GameObject BloodCloud;
    public ControlBloodEmission control;
    public Vector3 dir;
    public bool hasBloodedThisPlayer;
    public int idIndex = 0;
    public Transform playerBody;
    public ScreenFlashEffect screenF;
    public float hitMultiplier = 0.4f;
    public BulletManager manager;
    public bool prefab = false;
    public int stickId;
    public bool active = true;
    public int BulletSpeed;
    public int[] ids; 

	void Start() {
        //particle = transform.GetChild(transform.childCount-1).gameObject;
        //control = particle.GetComponent<ControlFlashEmission>();
        ids = new int[100];
        //playerBody = stick.Body.transform;
        time = Time.time;
        //active = true;
        stickId = stick.gameObject.GetInstanceID();
	}

    void Update()
    {   
        thisRigid.mass = 10f;
        //thisRigid.collisionDetectionMode = collisonDetectionMode.Continuous;
        if (gameObject.tag == "Bullet" && !prefab)
        { 
            if (Time.time - time > 10)
            {
                Destroy(gameObject); 
            }
            
            void OnBecameInvisible()
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // no idea why this doesn't work
        // only sometimes it'll recognize the objects are the same
        // even when it does, it doesn't correctly set gravity scale or remove all the correct objects
        //Debug.LogError("magibra" + thisRigid.velocity.magnitude);
        //Debug.LogError("iwantmoroutoflifethanthis" + active);
        int instanceId = coll.transform.root.gameObject.GetInstanceID();
        Stickman hitStick = coll.transform.root.gameObject.GetComponent<Stickman>();
        if(instanceId == stickId)
        {
            Physics2D.IgnoreCollision(thisCollider, coll.collider);
        }
        else if((coll.gameObject.tag == "lArm" || coll.gameObject.tag == "rArm" || coll.gameObject.tag == "rLeg" || coll.gameObject.tag == "lLeg" || coll.gameObject.tag == "lFoot" || coll.gameObject.tag == "rFoot") && active)//&& thisRigid.velocity.magnitude > (BulletSpeed - 5))
        {
            Debug.LogError("whatusdude");
            bool removeRest = false;
            Vector3 velocity = thisRigid.velocity;
            int i = 0;
            string tag = coll.gameObject.tag;
            int counter1 = 0;
            foreach(int id in ids)
            {
                if(id == instanceId)
                {
                    hasBloodedThisPlayer = true;
                    break;
                }
                counter1++;
                if(counter1 == ids.Length)
                {
                    hasBloodedThisPlayer = false;
                    break;
                }
            }
            if(!hasBloodedThisPlayer)
            {
                drawBlood(coll, instanceId);
            }
            // so yeah the blood ***REMOVED*** works now
            // doesn't look perfect, but does the job
            // look at gunplay's blood, it's like one big thing then fades into smaller ones, like 2 anis
            // next maybe try putting blood on walls, but no idea how this is done
            // the bullet masses, forces and whatever also quite weird rn, so prob look at that
            // i feel like sound would also bring it to life, like flesh sound on hit, and general rework of the sys
            // then maybe like a groan or something
            // also remember health and ***REMOVED***, cause rn he just dies on hit
            //Debug.LogError("armref" + coll.gameObject.GetInstanceID());
            // still no work, no know why
            // no rmeove rest;
            //Debug.LogError("chilCount" + stick.lArm.transform.childCount);
            int counter = 0;
            GameObject limb = new GameObject();
            if(coll.gameObject.transform.parent != null)
            {
                //Stickman hitStick = coll.gameObject.transform.root.gameObject.GetComponent<Stickman>();
                hitStick.health -= (thisRigid.velocity.magnitude * hitMultiplier );
                if(hitStick.health <= 0 && !hitStick.dead)
                {
                    hitStick.dead = true;
                    stick.PlayRandomClip(stick.dyingAudio);
                    if(!screenF.recurse)
                    {
                        screenF.Activate();
                    }
                }
                //hitStick.dead = true;
                if(tag == "lArm")
                {
                    limb = hitStick.lArm;
                }
                if(tag == "rArm")
                {
                    limb = hitStick.rArm;
                }
                if(tag == "rLeg")
                {
                    limb = hitStick.rLeg;
                    //hitStick.body_muscle.activated = false;
                }
                if(tag == "lLeg")
                {
                    limb = hitStick.lLeg;
                    //hitStick.body_muscle.activated = false;
                }
                if(!hitStick.Player)
                {
                    hitStick.firing = true;
                    hitStick.lastPos = hitStick.playerPos;
                }
                int length = limb.transform.childCount;
                int limbIndex = coll.gameObject.transform.GetSiblingIndex();
                coll.gameObject.transform.parent = null;
                coll.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
                muscle_holder holder = coll.gameObject.GetComponent<muscle_holder>();
                holder.activated = false;
                //Destroy(holder);
                // dont know if commenting this doesn anything
                // no idea why this isn't working, just you can't set gravity scale of block that's hit to 1
                // like even doing manuallly in gui does nothing
                Destroy(coll.gameObject.GetComponent<HingeJoint2D>());
                // looks weird, might just be because of no force though
                while(i < length)
                {
                    GameObject objec = limb.transform.GetChild(i).gameObject;
                    //Debug.LogError("noderef" + objec.GetInstanceID());
                    if(i == limbIndex && !removeRest && i != length)//objec.GetInstanceID() == coll.gameObject.GetInstanceID())
                    {
                        removeRest = true;
                        Debug.LogError("swaggyp" + i);
                        //i++;
                        //continue;
                    }
                    if(removeRest)
                    {
                        // if(counter == 0)
                        // {
                            
                        // }
                        counter++;
                        //objec.GetComponent<muscle_holder>().muscle.bone = null;
                        muscle_holder thisHolder = objec.GetComponent<muscle_holder>();
                        thisHolder.activated = false;
                        Destroy(thisHolder);
                        objec.transform.parent = null;
                        Rigidbody2D body = objec.GetComponent<Rigidbody2D>();
                        body.gravityScale = 1f;
                        body.drag = 0f;
                        //hitStick.body_muscle.bone.drag = 0f;
                        body.AddForce(velocity * (0.5f * body.mass), ForceMode2D.Impulse);
                        //Destroy(objec.GetComponent<HingeJoint2D>());
                    }
                    else
                    {
                        i++;
                    }
                    Debug.LogError("myivalue" + i);
                    length = limb.transform.childCount;
                }
                Debug.LogError("numerino" + counter);
            }
            else
            {
                Destroy(coll.gameObject.GetComponent<HingeJoint2D>());
            }
            //foreach(_Muscle musc in hitStick.muscles)
        }
        else if((coll.gameObject.tag == "Body" || coll.gameObject.tag == "Head") && active) //&& thisRigid.velocity.magnitude > (BulletSpeed - 5))
        {
            //Stickman hitStick = coll.gameObject.transform.root.gameObject.GetComponent<Stickman>();
            int counter1 = 0;
            foreach(int id in ids)
            {
                if(id == instanceId)
                {
                    hasBloodedThisPlayer = true;
                    break;
                }
                counter1++;
                if(counter1 == ids.Length)
                {
                    hasBloodedThisPlayer = false;
                    break;
                }
            }
            // seemingly works when upright, not otherwise,
            // too much force on it now, decrease the lifetime or sum so goes less further out
            // also delete these object as many in hiearychy after instan
            if(!hasBloodedThisPlayer)
            {
                drawBlood(coll, instanceId);
            }
            Rigidbody2D body = coll.gameObject.GetComponent<Rigidbody2D>();
            body.AddForce(thisRigid.velocity * (0.5f * body.mass * 2), ForceMode2D.Impulse);
            body.drag = 0f;
            if(coll.gameObject.tag == "Head")
            {
                if(!screenF.recurse && !hitStick.dead)
                {
                    screenF.Activate();
                }
                // uhh seemingly doesn't work, might have messed up other stuff as well
                // don't forget the shooting at 1700 in stickman, left a little note there or something
                stick.PlayRandomClip(stick.headShotAudio);
                hitStick.dead = true;
                HingeJoint2D joint = coll.gameObject.GetComponent<HingeJoint2D>();
                Destroy(joint);
                if(UnityEngine.Random.Range(0, 3) == 0)
                {
                    Destroy(coll.gameObject);
                }
            }
            else
            {
                hitStick.health -= (thisRigid.velocity.magnitude * hitMultiplier * 3);
                if(hitStick.health <= 0 && !hitStick.dead)
                {
                    stick.PlayRandomClip(stick.dyingAudio);
                    hitStick.dead = true;
                    if(!screenF.recurse)
                    {
                        screenF.Activate();
                    }
                }
            }
            if(!hitStick.Player)
            {
                hitStick.firing = true;
                hitStick.lastPos = hitStick.playerPos;
            }
        }
        else if(coll.gameObject.tag != "Gun" && manager.canPlay)
        {
            //Destroy(gameObject);
        

            if (coll.gameObject.tag != "Background")
            {
                AudioSource Audio = new AudioSource();
        
                //string[] info = Directory.GetDirectories("C:\\Users\\Jacob\\Downloads\\Unity Prototype\\Prototype\\Prototype\\Assets\\Sounds");
                //foreach(string f in info)
                //{
                    //if(f == coll.gameObject.tag)
                    //{
                         //DirectoryInfo dir = new DirectoryInfo("C:\\Users\\Jacob\\Downloads\\Unity Prototype\\Prototype\\Prototype\\Assets\\Sounds\\" + coll.gameObject.tag);
                        //FileInfo[] inf = dir.GetFiles("*.*");
                        //foreach(FileInfo file in inf)
                        //{
                            //Debug.Log("HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHlfkejfjklejflkjaakl");
                    string fileName = null;
                    if((thisRigid.velocity.magnitude > 14f))
                    {
                        fileName = "Loud Bullets";
                       // control.ActivateEmission();
                                
                                    //byte[] fileData = File.ReadAllBytes("C:\\Users\\Jacob\\Downloads\\Unity Prototype\\Prototype\\Prototype\\Assets\\Sounds\\" + coll.gameObject.tag + "\\" + file.Name);
                                    //float[] filess = ConvertByteToFloat(fileData);
                                    //AudioClip audioClip = AudioClip.Create("testSound", filess.Length, 1, 44100, false);
                             //audioClip.SetData(filess, 0);
                                    //Audio.clip = audioClip;
                    }
                         //}
                    else
                    {
                        fileName = "Soft Bullets";
                                
                                    //byte[] fileData = File.ReadAllBytes("C:\\Users\\Jacob\\Downloads\\Unity Prototype\\Prototype\\Prototype\\Assets\\Sounds\\" + coll.gameObject.tag + "\\" + file.Name);
                                    //float[] filess = ConvertByteToFloat(fileData);
                                    //AudioClip audioClip = AudioClip.Create("testSound", f.Length, 1, 44100, false);
                                    //audioClip.SetData(filess, 0);
                                    //Audio.clip = audioClip;
                                
                    }
                    //stick.Start();
                    // these sounds are ***REMOVED***ing ***REMOVED*** so i turned them off
                    if(stick.Audios.ContainsKey(coll.gameObject.tag))
                    {
                        int key_value = stick.Audio_Map[fileName];
                        AudioSource[] audi = stick.Audios[coll.gameObject.tag];
                        //audi[key_value].PlayOneShot(audi[key_value].clip);
                        manager.lastPlayTime = Time.time;
                    }
                    else
                    {
                        int key_value = stick.Audio_Map["defaultBulletHit"];
                        AudioSource[] audi = stick.Audios["Metallic"];
                        //audi[key_value].PlayOneShot(audi[key_value].clip);
                        manager.lastPlayTime = Time.time;
                    }
                            //Debug.Log("***REMOVED***my***REMOVED***");
                            //Debug.Log(file.Name);
                             //byte[] fileDat = File.ReadAllBytes(file.Name);
                            //float[] files = ConvertByteToFloat(fileDat);
                            //AudioClip audioCli = AudioClip.Create("testSound", f.Length, 1, 44100, false);
                           // audioCli.SetData(files, 0);
                            //Audio.clip = audioCli;
                        }
                    }
        else if(coll.gameObject.tag == "World")
        {
            active = false;
        }
                    //Stickman script = coll.transform.root.gameObject.GetComponent<Stickman>();
                    if(hitStick != null)
                    {
                        hitStick.CheckBrokenLimbs();
                    }
              }
              // file:///C:/Users/Jacob/Downloads/Unity Prototype/Prototype/Prototype/Assets/Sounds/Metallic/Soft Bullets.wav

              //C:\Users\Jacob\Downloads\Unity Prototype\Prototype\Prototype\Assets\Sounds\Metallic
            //AudioSource Audio = new AudioSource();
            ////string fileName = null;
            //string soundPath = "file:///" + "C:/Users/Jacob/Downloads/Unity Prototype/Prototype/Prototype/Assets/Sounds" + "/" + coll.gameObject.tag;
            //if((thisRigid.velocity.magnitude > 10f))
            //{
                //fileName = "Loud Bullets.wav";
            //}
           // else
            //{
                //fileName = "Soft Bullets.wav";
            //}
            //string path = soundPath + "/" + fileName;
            //Debug.Log(path);
            //StartCoroutine(loadAudio(path, Audio));
             //byte[] fil = File.ReadAllBytes("C:\\Users\\Jacob\\Downloads\\Unity Prototype\\Prototype\\Prototype\\Assets\\Sounds\\Metallic\\Loud Bullets.wav");
            //float[] fi = ConvertByteToFloat(fil);
            //Debug.Log("black");
            //Debug.Log(fi.Length);
            //AudioClip audioCl = AudioClip.Create("testSound", fi.Length, 1, 44100, false);
            //audioCl.SetData(fi, 0);
            //Audio.clip = audioCl;
            //Audio.PlayOneShot(Audio.clip);
                //if (mag > 5)
               // {
                   //Destroy(coll.gameObject); 
                //}

                //else
               // {
                    //coll.gameObject.transform.rotation = gameObject.transform.rotation;
                    //coll.gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
                //}
     private float[] ConvertByteToFloat(byte[] array) 
            {
                float[] floatArr = new float[array.Length / 4];
                for (int i = 0; i < floatArr.Length; i++) 
                {
                    if (BitConverter.IsLittleEndian) 
                        Array.Reverse(array, i * 4, 4);
                        floatArr[i] = BitConverter.ToSingle(array, i * 4) / 0x80000000;
                }
                return floatArr;
            }
    private IEnumerator loadAudio(string soundPath, AudioSource source) {
       using (var www = UnityWebRequestMultimedia.GetAudioClip("file:/// C:/Users/Jacob/Downloads/Unity Prototype/Prototype/Prototype/Assets/Sounds/Metallic/Loud Bullets.wav", AudioType.WAV))
       {
        //yield return request.SendWebRequest;
        yield return www.SendWebRequest();
        //www.downloadHandler = new DownloadHandlerBuffer();
        if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            yield break;
        }
        
        //AudioClip clip = www.GetAudioClip(soundPath, AudioType.WAV);
            source.clip = DownloadHandlerAudioClip.GetContent(www);
       }
    }
    public void drawBlood(Collision2D col, int id)
    {
        GameObject cloneParticle = Instantiate(BloodParticle) as GameObject;
        GameObject cloneCloud = Instantiate(BloodCloud) as GameObject;
        ContactPoint2D contact = col.GetContact(0);
        cloneParticle.transform.position = contact.point;
        cloneCloud.transform.position = contact.point;
        ParticleSystem cloneSystem = cloneParticle.GetComponent<ParticleSystem>();
        UnityEngine.ParticleSystem.VelocityOverLifetimeModule module = cloneSystem.velocityOverLifetime;
        module.x = contact.normal.x;
        module.y = contact.normal.y;
        module.speedModifier = 0.1f;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        Vector3 rot = new Vector3(0f,0f, angle);
        UnityEngine.ParticleSystem.ShapeModule cloneShape = cloneSystem.shape;
        cloneShape.rotation = rot;
        //cloneSystem.shape = cloneShape;
        //module.yMultiplier = UnityEngine.Random.Range(0, 100);
        //module.xMultiplier = UnityEngine.Random.Range(0, 100);
        // if(playerBody.position.x < col.transform.position.x)
        // {
        //     //module.xMultiplier = -module.xMultiplier;
        // }
        ControlBloodEmission cont1 = cloneParticle.GetComponent<ControlBloodEmission>();
        ControlBloodEmission cont2 = cloneCloud.GetComponent<ControlBloodEmission>();
        cont1.ActivateEmission();
        //cont2.ActivateEmission();
        ids[idIndex] = id;
        idIndex++;
    }
    }
        

