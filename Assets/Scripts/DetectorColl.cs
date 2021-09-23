using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class DetectorColl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
 
 
 
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag != "Cone" );
        {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), coll.gameObject.GetComponent<Collider2D>());
        }
    }
}