using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;

public class Bullet32 : MonoBehaviour {
    public float time = 0;
    public Rigidbody2D thisRigid;

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
            float mag = gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
            //Destroy(gameObject);
        

            if (coll.gameObject.tag != "Background")
            {
                   ///}
                //}AudioSource Audio = new AudioSource();
                //string[] info = Directory.GetDirectories("C:\\Users\\Jacob\\Downloads\\Unity Prototype\\Prototype\\Prototype\\Assets\\Sounds");
                //string soundPath = "file://" + "C:\\Users\\Jacob\\Downloads\\Unity Prototype\\Prototype\\Prototype\\Assets\\Sounds" + "\\" + coll.gameObject.tag;

                //foreach(string f in info)
                //{
                    //if(f == coll.gameObject.tag)
                   // {
                        // DirectoryInfo dir = new DirectoryInfo("C:\\Users\\Jacob\\Downloads\\Unity Prototype\\Prototype\\Prototype\\Assets\\Sounds\\" + coll.gameObject.tag);
                        //FileInfo[] inf = dir.GetFiles("*.*");
                        //foreach(FileInfo file in inf)
                        //{
                //string fileName = null;
                //AudioSource Audio = new AudioSource();
                //if((thisRigid.velocity.magnitude > 10f))
                //{
                    //fileName = "Loud Bullets.wav";
                //}
                //else
               // {
                    //fileName = "Soft Bullets.wav";
                //}
                //string path = soundPath + "\\" + fileName;
                //StartCoroutine(loadAudio(path, Audio));
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
            //}


        }
    }
    //private IEnumerator loadAudio(string soundPath, AudioSource source) {
        //u//sing (UnityWebRequest request =  UnityWebRequestMultimedia.GetAudioClip(soundPath));
        //yield return request.SendWebRequest;
        //while(!request.isDone)
        //{
            //yield return request;
        //}
        //yield return request;
        //source.clip = request.GetAudioClip(false,false);
    }

        

}