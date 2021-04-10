using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float time = 0;

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
            Destroy(gameObject);
        

            if (coll.gameObject.tag != "World")
            {
                if (mag > 5)
                {
                   Destroy(coll.gameObject); 
                }

                else
                {
                    coll.gameObject.transform.rotation = gameObject.transform.rotation;
                    coll.gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
                }
            }


        }
    }
        

}