using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class NoCollision : MonoBehaviour
{
    // Start is called before the first frame update
 
 
 
    void OnCollisionEnter2D(Collision2D coll)
    {
        if ((coll.gameObject.tag == "Player" || coll.gameObject.tag == "rArm" || coll.gameObject.tag == "lArm" ||coll.gameObject.tag == "Gun" || coll.gameObject.tag == "lLeg" || coll.gameObject.tag == "rLeg" || coll.gameObject.tag == "LlLeg" || coll.gameObject.tag == "LrLeg") && (coll.gameObject.tag != gameObject.tag))// || coll.gameObject.tag == "rArm"))
        {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), coll.gameObject.GetComponent<Collider2D>());
        }
        
    }
}