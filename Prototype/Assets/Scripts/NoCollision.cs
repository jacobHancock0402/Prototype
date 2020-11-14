using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class NoCollision : MonoBehaviour
{
    // Start is called before the first frame update
 
 
    // the different feet can't collide as it feels like doesnt trip as legs stay ahead of eachother
    // but shouldn't they move back and forth?
    // but looks too weak and weird with overlap otherwise
    // two options are at bottom
    // if collision sort out directions, as sometimes will collide whe
    // i.e left goes right when changing direction to right, when right shoudl go first
    void OnCollisionEnter2D(Collision2D coll)
    {
<<<<<<< HEAD
       if ((coll.gameObject.tag == "Player" || coll.gameObject.tag == "rArm" || coll.gameObject.tag == "lArm" || (coll.gameObject.tag == "lLeg" && gameObject.tag != "lFoot" && gameObject.tag != "rLeg") || (coll.gameObject.tag == "rLeg" && gameObject.tag != "rFoot" && gameObject.tag != "lLeg") || coll.gameObject.tag == "LlLeg" || coll.gameObject.tag == "LrLeg" || coll.gameObject.tag == "Head" || coll.gameObject.tag == "Body" || (coll.gameObject.tag == "rFoot" && gameObject.tag != "rLeg" && gameObject.tag != "lFoot") || (coll.gameObject.tag == "lFoot" && gameObject.tag != "lLeg" && gameObject.tag != "rFoot" )) && (coll.gameObject.tag != gameObject.tag) )
=======
        if ((coll.gameObject.tag == "Player" || coll.gameObject.tag == "rArm" || coll.gameObject.tag == "lArm" ||coll.gameObject.tag == "Gun" || coll.gameObject.tag == "lLeg" || coll.gameObject.tag == "rLeg" || coll.gameObject.tag == "LlLeg" || coll.gameObject.tag == "LrLeg") && (coll.gameObject.tag != gameObject.tag))// || coll.gameObject.tag == "rArm"))
>>>>>>> 242da17ffddb7cca4e9364343fad6ca7e8037683
        {
            
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), coll.gameObject.GetComponent<Collider2D>());
        }
        
    }
     //if ((coll.gameObject.tag == "Player" || coll.gameObject.tag == "rArm" || coll.gameObject.tag == "lArm" ||coll.gameObject.tag == "Gun" || (coll.gameObject.tag == "lLeg" && gameObject.tag != "lFoot" && gameObject.tag != "rLeg") || (coll.gameObject.tag == "rLeg" && gameObject.tag != "rFoot" && gameObject.tag != "lLeg") || coll.gameObject.tag == "LlLeg" || coll.gameObject.tag == "LrLeg" || coll.gameObject.tag == "Head" || coll.gameObject.tag == "Body" || (coll.gameObject.tag == "rFoot" && gameObject.tag != "rLeg" && gameObject.tag != "lFoot") || (coll.gameObject.tag == "lFoot" && gameObject.tag != "lLeg" && gameObject.tag != "rFoot" )) && (coll.gameObject.tag != gameObject.tag) ) || coll.gameObject.tag == "rArm"))
     //if ((coll.gameObject.tag == "Player" || coll.gameObject.tag == "rArm" || coll.gameObject.tag == "lArm" ||coll.gameObject.tag == "Gun" || (coll.gameObject.tag == "lLeg" && gameObject.tag != "lFoot") || (coll.gameObject.tag == "rLeg" && gameObject.tag != "rFoot" ) || coll.gameObject.tag == "LlLeg" || coll.gameObject.tag == "LrLeg" || coll.gameObject.tag == "Head" || coll.gameObject.tag == "Body" || (coll.gameObject.tag == "rFoot" && gameObject.tag != "rLeg" ) || (coll.gameObject.tag == "lFoot" && gameObject.tag != "lLeg"  )) && (coll.gameObject.tag != gameObject.tag) )// || coll.gameObject.tag == "rArm"))
}