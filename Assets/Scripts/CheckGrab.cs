    
using UnityEngine;
public class CheckGrab : MonoBehaviour {
    public Stickman Stick;
    public Gun Gun;
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(gameObject.tag == "rArm")
        {
                if (Stick.grabbingR && (coll.gameObject.tag == "World" || coll.gameObject.tag == "Gun" || coll.gameObject.tag == "Weapon"))
                {
                    Stick.grabbingR = false;
                    gameObject.transform.rotation = Quaternion.Euler(0,0, -720);
                    coll.gameObject.transform.SetParent(gameObject.transform, true);
                    Collider2D collider = coll.gameObject.GetComponent<Collider2D>();
                    // might have to add value on + change wether adding or minus depending on direction the arm is in // add a bit of y and x depending on angle like multiply init
                    coll.gameObject.transform.position = new Vector3((gameObject.transform.position.x + Mathf.Abs(collider.bounds.max[0] - collider.bounds.center[0])), gameObject.transform.position.y, gameObject.transform.position.z);
                    Destroy(coll.gameObject.GetComponent<Rigidbody2D>());
                    Stick.NowGrabbingR = true;
                }

            if ((coll.gameObject.tag == "World" || coll.gameObject.tag == "Gun" || coll.gameObject.tag == "Weapon" || coll.gameObject.tag == "Rope")&& Stick.holdingR)
            {
                Rigidbody2D connectedBody = coll.gameObject.GetComponent<Rigidbody2D>();
                Stick.holdingR = false;
                if (coll.gameObject.tag == "Rope")
                {
                    DistanceJoint2D Hinge = gameObject.AddComponent(typeof(DistanceJoint2D )) as DistanceJoint2D ;
                    Hinge.connectedBody = connectedBody;
                }

                else
                {
                    HingeJoint2D Hinge = gameObject.AddComponent(typeof(HingeJoint2D )) as HingeJoint2D ;
                    Hinge.connectedBody = connectedBody;
                }
                Gun.DontAim = true;
                gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
                Stick.NowHoldingR = true;
                
                }
            }
        else
        {
            if ((coll.gameObject.tag == "World" || coll.gameObject.tag == "Gun" || coll.gameObject.tag == "Weapon" || coll.gameObject.tag == "Rope") && Stick.holdingL)
        {
            Stick.holdingL = false;
            Rigidbody2D connectedBody = coll.gameObject.GetComponent<Rigidbody2D>();
            if (coll.gameObject.tag == "Rope")
            {
                DistanceJoint2D Hinge = gameObject.AddComponent(typeof(DistanceJoint2D )) as DistanceJoint2D ;
                Hinge.connectedBody = connectedBody;
            }

            else
            {
                HingeJoint2D Hinge = gameObject.AddComponent(typeof(HingeJoint2D )) as HingeJoint2D ;
                Hinge.connectedBody = connectedBody;
            }
            Stick.NowHoldingL = true;
            
            
        }

        if ((coll.gameObject.tag == "World" || coll.gameObject.tag == "Gun" || coll.gameObject.tag == "Weapon") && Stick.grabbingL) {
            Stick.grabbingR = true;
            gameObject.transform.rotation = Quaternion.Euler(0,0, -720);
            coll.gameObject.transform.SetParent(gameObject.transform, true);
            Collider2D collider = coll.gameObject.GetComponent<Collider2D>();
            // might have to add value on + change wether adding or minus depending on direction the arm is in // add a bit of y and x depending on angle like multiply init
            coll.gameObject.transform.position = new Vector3((gameObject.transform.position.x + Mathf.Abs(collider.bounds.max[0] - collider.bounds.center[0])), gameObject.transform.position.y, gameObject.transform.position.z);
            Destroy(coll.gameObject.GetComponent<Rigidbody2D>());
            Stick.NowGrabbingR = true;
            //Stick.grabbingL = false;
            //gameObject.transform.rotation = Quaternion.Euler(0,0, 0);
            //coll.gameObject.transform.SetParent(gameObject.transform, true);
            //coll.gameObject.transform.position = new Vector3(gameObject.transform.position.x - 2, gameObject.transform.position.y, gameObject.transform.position.z);
            //Destroy(coll.gameObject.GetComponent<Rigidbody2D>());
            //Stick.NowGrabbingL = true;
        }

        }
    }
}