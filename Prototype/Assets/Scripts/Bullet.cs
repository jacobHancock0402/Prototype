using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;
using System.IO;

public class Bullet : MonoBehaviour {
    public float time = 0;
    public Rigidbody2D thisRigid;
    public Stickman stick;

	void Start() {
        time = Time.time;
	}

    void Update()
    {   
        if (gameObject.tag == "Bullet")
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
        if (coll.gameObject.tag != "Gun" && coll.gameObject.tag != "rArm")
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
                    if(stick.Audios.ContainsKey(coll.gameObject.tag))
                    {
                        int key_value = stick.Audio_Map[fileName];
                        AudioSource[] audi = stick.Audios[coll.gameObject.tag];
                        audi[key_value].PlayOneShot(audi[key_value].clip);
                    }
                    else
                    {
                        int key_value = stick.Audio_Map["defaultBulletHit"];
                        AudioSource[] audi = stick.Audios["Metallic"];
                        audi[key_value].PlayOneShot(audi[key_value].clip);
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
    }
        

